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
                return MyStatusCodeBase.MyStatusCode(this,await autsv.RegisterAsync(req) );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto req)
        {
                return MyStatusCodeBase.MyStatusCode(this, await autsv.LoginAsync(req));
        }

        [HttpPost("refreshtoken")]

        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto tokenRequest)
        {
            
            return MyStatusCodeBase.MyStatusCode(this, await autsv.RefreshTokenAsync(tokenRequest));
        }

        [HttpPost("google-Login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthDto dto)
        {   
                return MyStatusCodeBase.MyStatusCode(this,await autsv.GoogleLoginAsync(dto) );
        }
        public class GoogleRegisterRequest
        {
            public RegisterGG GG { get; set; }
            public GoogleAuthDto Dto { get; set; }
        }

        [HttpPost("google-register")]
        public async Task<IActionResult> GoogleRegister([FromBody] GoogleRegisterRequest req)
        { 
                return MyStatusCodeBase.MyStatusCode(this, await autsv.RegisterGoogle(req.GG, req.Dto));  
        }
    }
}
