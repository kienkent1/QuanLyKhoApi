using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService autsv) : ControllerBase
    {
        public static TaiKhoan user = new();

        [HttpPost("register")]
        public async Task<ActionResult<TaiKhoan>> Register(TaiKhoan req)
        {
            try
            {


                var user = await autsv.RegisterAsync(req);
                if (user is null) return BadRequest("Tên đăng nhập đã tồn tại");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }

        }
    }
}
