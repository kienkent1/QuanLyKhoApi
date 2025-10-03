using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using test.MyHelper;
using static test.MyHelper.GitHubImageService;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestImgController(GitHubImageService _github) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Testimg([FromForm ] IFormFile[] files,[FromForm] string folder)
        {
            List<GitHubRes> patch = await _github.Updateimg(files, folder);
            return Ok(patch);
        }


    }
}
