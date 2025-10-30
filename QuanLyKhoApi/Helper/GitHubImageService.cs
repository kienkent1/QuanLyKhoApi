using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;
using static QuanLyKhoApi.Helper.GitHubImageService;

namespace QuanLyKhoApi.Helper
{
    public sealed class GitHubImageService(IOptions<GitHubOptions> git)
    {
        //link hiển thị ảnh : https://raw.githubusercontent.com/kienkent1/QuanLyKhoImg/main/(folder)/name //download_url

        private readonly HttpClient http;
        private const long MaxFileSize = 10L * 1024 * 1024;
        private string Owner = git.Value.Owner;
        private string Repo = git.Value.Repo;
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<GitHubRes>> UpdateimgList(IFormFile[] files, string folder)
        {

            if (files is null) return null;
            List<GitHubRes> result = new List<GitHubRes>();
            for (var i = 0; i < files.Length; i++)
            {
                IFormFile FileChecked = await CheckNameImg(files[i], folder);
                var item = await UpdateImgAsync(FileChecked, folder);
                result.Add(item);
            }
            return result;
        }
        public async Task<GitHubRes> UpdateOneImg(IFormFile file, string folder)
        {
            if (file is null) return null;
            IFormFile FileChecked = await CheckNameImg(file, folder);
            var res = await UpdateImgAsync(FileChecked, folder);
            return res;
        }
        #region code lỏ
        //private async Task<string> UpdateImgAsync(IFormFile file, string folder)
        //{
        //    string BaseUri = $"https://api.github.com/repos/{Owner}/{Repo}/contents/{folder}/";
        //    var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    using var memoryStream = new MemoryStream();
        //    await file.CopyToAsync(memoryStream);
        //    var fileBytes = memoryStream.ToArray();
        //    var base64Content = Convert.ToBase64String(fileBytes);
        //    string convertFileName = ToSlug(file.FileName);
        //    var req = new GitHubUploadRequest() {Message= convertFileName, Content=base64Content };
        //    string body = JsonSerializer.Serialize(req);
        //    var content = new StringContent(body, Encoding.UTF8, "application/json");
        //    try
        //    {
        //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage res = await client.PutAsync(BaseUri + folder+ "/" + convertFileName, content);
        //        res.EnsureSuccessStatusCode();
        //        return await res.Content.ReadAsStringAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }

        //}
        #endregion

        private async Task<IFormFile> CheckNameImg(IFormFile file, string folder)
        {
            if (file.Length > MaxFileSize)
                throw new InvalidOperationException($"File {file.FileName} vượt quá dung lượng cho phép (10 MB).");

            string convertFileName = ToSlug(file.FileName);
            string path = $"{folder}/{convertFileName}";
            string BaseUri = $"https://api.github.com/repos/{Owner}/{Repo}/contents/{path}";
            var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("QuanLyKhoImg/1.0");

            try
            {
                //  Kiểm tra file có tồn tại hay không
                HttpResponseMessage res = await client.GetAsync(BaseUri);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // file đã tồn tại -> đổi tên
                    string ext = Path.GetExtension(file.FileName);
                    string newName = $"{Path.GetFileNameWithoutExtension(convertFileName)}_{DateTime.UtcNow:yyyyMMddHHmmss}{ext}";

                    var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    IFormFile newFile = new FormFile(memoryStream, 0, memoryStream.Length, file.Name, newName)
                    {
                        Headers = file.Headers,
                        ContentType = file.ContentType
                    };
                    return newFile;
                }
                // nếu 404 => file chưa tồn tại, giữ nguyên
                else if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return file;
                }

                res.EnsureSuccessStatusCode();
                return file;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // file chưa tồn tại
                return file;
            }
            catch
            {
                return file;
            }
        }

        private async Task<GitHubRes> UpdateImgAsync(IFormFile file, string folder)
        {
            if (file.Length > MaxFileSize)
                throw new InvalidOperationException($"File {file.FileName} vượt quá dung lượng cho phép (10 MB).");
            string BaseUri = $"https://api.github.com/repos/{Owner}/{Repo}/contents/{folder}/";
            var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            client.DefaultRequestHeaders.UserAgent.ParseAdd("QuanLyKhoImg/1.0");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var base64Content = Convert.ToBase64String(fileBytes);

            string convertFileName = ToSlug(file.FileName);

            var req = new GitHubUploadRequest
            {
                Message = $"upload {convertFileName}",
                Content = base64Content
            };

            string body = JsonSerializer.Serialize(req, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage res = await client.PutAsync(BaseUri + convertFileName, content);
                res.EnsureSuccessStatusCode();

                var json = await res.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                GitHubRes url = new GitHubRes();
                url.Path = doc.RootElement.GetProperty("content").GetProperty("path").GetString();
                url.Url = doc.RootElement.GetProperty("content").GetProperty("download_url").GetString();
                return url;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string ToSlug(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string normalized = input.Normalize(NormalizationForm.FormD);

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string noDiacritics = regex.Replace(normalized, "")
                                       .Replace("đ", "d")
                                       .Replace("Đ", "D");


            string slug = Regex.Replace(noDiacritics, @"\s+", "-");

            slug = Regex.Replace(slug, @"[^a-zA-Z0-9\.\-]", "").ToLower();

            slug = slug.Trim('-');

            return slug;
        }

        private class GitHubUploadRequest
        {

            public string Message { get; set; }

            public string Content { get; set; }
            public string Sha { get; set; }
        }
        public class GitHubOptions
        {
            public const string GitHub = "GitHub";
            public string Owner { get; set; }
            public string Repo { get; set; }
            public string Branch { get; set; }
        }
        public class GitHubRes
        {
            public string Path { get; set; }
            public string Url { get; set; }
        }

    }
}
