using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService autsv) : ControllerBase
    {
       // public static TaiKhoan user = new();

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto req)
        {
            try
            {
                bool IsExitUser = await autsv.ExitsUser(req.IdNhanVien);

                if (IsExitUser == true)
                {
                    if (await autsv.ValidateAccount(req.IdNhanVien, req.TenDangNhap))
                    {


                        var user = await autsv.RegisterAsync(req);
                        if (user is null) return BadRequest("Tên đăng nhập đã tồn tại");

                        return Ok(req.TenDangNhap);
                    }
                    else return BadRequest("Bạn chưa có thông tin trong hệ thống hoặc Tên đăng nhập bị trùng");
                }
                else return BadRequest("Bạn chưa có thông tin trong hệ thống");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginDto req)
        {
            try
            {
                var token = await autsv.LoginAsync(req);
                if (token is null) return Unauthorized("Tên đăng nhập hoặc mật khẩu không đúng");
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpPost("refreshtoken")]

        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto tokenRequest)
        {
            var token = await autsv.RefreshTokenAsync(tokenRequest);
            if (token is null || token.AccessToken is null || token.RefreshToken is null) return Unauthorized("Token không hợp lệ");
            return Ok(token);
        }

        [HttpPost("google-Login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthDto dto)
        {
            try
            {
                if (dto is null) return BadRequest("Không có id token được gửi");
                var token = await autsv.GoogleLoginAsync(dto);
                if (token is null) return BadRequest("Lỗi xác thực với id token");
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public class GoogleRegisterRequest
        {
            public RegisterGG GG { get; set; }
            public GoogleAuthDto Dto { get; set; }
        }

        [HttpPost("google-register")]
        public async Task<IActionResult> GoogleRegister([FromBody] GoogleRegisterRequest req)
        {
            try
            {
                if (req.Dto is null || req.GG is null) return BadRequest("Vui lòng điền đầy đủ thông tin");
                var newAcc = await autsv.RegisterGoogle(req.GG, req.Dto);
                if(newAcc is null) return BadRequest("lỗi: không thể tạo tài khoản");
                return Ok(newAcc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
