using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyKhoApi.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration, IMapper mapper) : IAuthService
    {
        //tạo token
        private string CreateToken(TaiKhoan user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.NameIdentifier, user.IdNhanVien.ToString()),

            };

            //vì dùng user id để tìm role của user đó nên không cần lặp qua roles nữa
            //foreach (var role in user.TaiKhoanRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.RoleId));
            //}
            //system.identity.tokens.jwt tai thu vien ve
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
                );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDes = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDes);
        }

        //tạo chuỗi ngãu nhiên
        private string CreateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        //kiểm tra token có còn hợp lệ
        private async Task<TaiKhoan?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var token = await context.TaiKhoanToken
        .FirstOrDefaultAsync(t => t.IdTaiKhoan == userId && t.RefreshToken == refreshToken);

            if (token is null || token.ExpiryTime < DateTime.UtcNow)
                return null;

            // load user kèm role 
            return await context.TaiKhoan
                .Include(u => u.TaiKhoanRoles)
                .ThenInclude(tr => tr.Role)
                .FirstOrDefaultAsync(u => u.IdNhanVien == userId);
        }

        //tạo vào lưu token mới
        private async Task<string> GenerateAndSaveRefreshToken(Guid IdTaiKhoan)
        {

            var refreshToken = CreateRefreshToken();

            var token = new TaiKhoanToken
            {
                IdTaiKhoan = IdTaiKhoan,
                RefreshToken = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(2)
            };
            context.TaiKhoanToken.Add(token);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        //tạo token trả về
        private async Task<TokenResponseDto> CreateTokenResponseAsync(TaiKhoan user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user.IdNhanVien)
            };
        }

        //check user tồn tại
        public async Task<bool> ExitsUser(Guid id)
        {
            return await context.NhanVien.AnyAsync(u => u.IdNhanVien == id);
        }
        //kiểm tra tài khoản có hợp lệ để register
        public async Task<bool> ValidateAccount(Guid id, string userName)
        {
            if (await context.TaiKhoan.AnyAsync(u => u.IdNhanVien == id))
                return false;
            if (await context.TaiKhoan.AnyAsync(u => u.TenDangNhap == userName))
                return false;
            var TrangThai = await context.NhanVien.AnyAsync(u => u.IdNhanVien == id && u.trangthai == false);
            if (TrangThai) return false;
            return true;
        }

        public async Task<bool> IsEmailExit(string email)
        {
            return await context.NhanVien.AnyAsync(u => u.email == email);
        }
        public async Task<TaiKhoan> RegisterAsync(RegisterDto req)
        {
            try
            {
                var hashedPass = new PasswordHasher<RegisterDto>()
                    .HashPassword(req, req.Password);

                req.Password = hashedPass;
                req.CreatedAt = DateTime.UtcNow;
                var account = mapper.Map<TaiKhoan>(req);
                context.TaiKhoan.Add(account);
                var userRole = new TaiKhoanRole
                {
                    TaiKhoanId = req.IdNhanVien,
                    RoleId = "user"
                };
                context.TaiKhoanRoles.Add(userRole);
                await context.SaveChangesAsync();
                return account;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<TokenResponseDto?> LoginAsync(LoginDto req)
        {
            var user = await context.TaiKhoan
                 .Include(t => t.NhanVien)
                 .Include(t => t.TaiKhoanRoles)
         .ThenInclude(tr => tr.Role)
        .FirstOrDefaultAsync(t =>
            t.TenDangNhap == req.UserNameOrEmail ||
            t.NhanVien.email == req.UserNameOrEmail);

            if (user is null) return null;

            if (new PasswordHasher<TaiKhoan>().VerifyHashedPassword(user, user.Password, req.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }


            return await CreateTokenResponseAsync(user);
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto req)
        {
            var user = await ValidateRefreshTokenAsync(req.UserId, req.RefreshToken);
            if (user is null) return null;
            var refToken = await context.TaiKhoanToken
                .FirstOrDefaultAsync(t => t.IdTaiKhoan == req.UserId && t.RefreshToken == req.RefreshToken);
            if (refToken is not null)
            {
                context.TaiKhoanToken.Remove(refToken);
                await context.SaveChangesAsync();
            }
            return await CreateTokenResponseAsync(user);
        }


        #region Authentication GG
        public  async Task<GoogleResponse> GetGoogleResponse(GoogleAuthDto dto)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(
                    dto.IdToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[]
                        {
                            configuration.GetValue<string>("Authentication:Google:ClientId")!
                        }
                    }
                    );

                if (!payload.EmailVerified) return null;
                var res = new GoogleResponse();
                res.GoogleSub = payload.Subject;
                res.Email = payload.Email.Trim().ToLowerInvariant();
                res.EmailVerified  = payload.EmailVerified;
                res.Picture = payload.Picture;
                return res;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RegisterGG?> RegisterGoogle(RegisterGG req, GoogleAuthDto gg)
        {
            try
            {
                var res = await GetGoogleResponse(gg);
                if (res is  null) return null;
                var exitsUser = await ExitsUser(req.IdNhanVien);
                if (exitsUser is false) return null;

                var validateAccount = await ValidateAccount(req.IdNhanVien, req.TenDangNhap);
                if (validateAccount is false) return null;
                if (String.IsNullOrEmpty(req.TenDangNhap)) req.TenDangNhap = res.Email;
         
                req.CreatedAt = DateTime.UtcNow;
                var account = mapper.Map<TaiKhoan>(req);
                account.GoogleId = res.GoogleSub;
                context.TaiKhoan.Add(account);
                var userRole = new TaiKhoanRole
                {
                    TaiKhoanId = req.IdNhanVien,
                    RoleId = "user"
                };
                context.TaiKhoanRoles.Add(userRole);

                var user = await context.NhanVien.Include(u => u.TaiKhoan)
                    .FirstOrDefaultAsync(nv =>nv.IdNhanVien == req.IdNhanVien && nv.UrlHinh == null);
                if (user is not null)
                {
                    user.UrlHinh = res.Picture;
                    context.NhanVien.Update(user);
                }
                await context.SaveChangesAsync();
                return req;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<TokenResponseDto?> GoogleLoginAsync(GoogleAuthDto dto)
        {
            try
            {
                var GGResponse = await GetGoogleResponse(dto);

                var Account = await context.TaiKhoan.Include(u => u.NhanVien)
                    .FirstOrDefaultAsync(u => u.GoogleId == GGResponse.GoogleSub  || u.NhanVien.email == GGResponse.Email);



                if (Account is not null)
                {
                    if (Account.GoogleId == GGResponse.GoogleSub)
                    {
                        return await CreateTokenResponseAsync(Account);
                    }
                    else
                    {
                        var isAccExit = await context.TaiKhoan.FirstOrDefaultAsync(t => t.GoogleId == GGResponse.GoogleSub);
                        if (isAccExit is not null) return await CreateTokenResponseAsync(isAccExit);
                        else return null;
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion
    }
}
