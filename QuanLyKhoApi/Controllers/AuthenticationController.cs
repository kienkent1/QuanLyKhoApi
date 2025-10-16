using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Helper;
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
               var result = await autsv.RegisterAsync(req);

                return MyStatusCodeBase.MyStatusCode(this, result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto req)
        {
            try
            {
                var result = await autsv.LoginAsync(req);
                
                return MyStatusCodeBase.MyStatusCode(this, result);
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
            
            return MyStatusCodeBase.MyStatusCode(this, token);
        }

        [HttpPost("google-Login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthDto dto)
        {
            try
            {
                if (dto is null) return BadRequest("Không có id token được gửi");
                var token = await autsv.GoogleLoginAsync(dto);
               
                return MyStatusCodeBase.MyStatusCode(this, token);
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
                var newAcc = await autsv.RegisterGoogle(req.GG, req.Dto);
                return MyStatusCodeBase.MyStatusCode(this, newAcc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
