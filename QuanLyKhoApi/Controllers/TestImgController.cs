using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using System.Threading.Tasks;
using QuanLyKhoApi.Helper;
using static QuanLyKhoApi.Helper.GitHubImageService;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestImgController(GitHubImageService _github, AppDbContext db) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Testimg([FromForm ] IFormFile[] files,[FromForm] string folder)
        {
            List<GitHubRes> patch = await _github.UpdateimgList(files, folder);
            return Ok(patch);
        }

        [HttpGet]
        public IActionResult testTrangThaiPhieu()
        {
            var status = db.PhieuNhap.AsQueryable();

           var result = status.Select(t => t.TrangThaiPhieu.TenTrangThai);
            return Ok(result);
        }


    }
}
