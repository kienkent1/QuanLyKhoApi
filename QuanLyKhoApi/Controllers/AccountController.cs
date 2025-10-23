using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountService service, AuthorizationService auth) : ControllerBase
    {
        private class ClaimUser
        {
            public const string XemNhanVien = "XemNhanVien";
            public const string ThemNhanVien = "ThemNhanVien";
            public const string SuaNhanVien = "SuaNhanVien";
            public const string XoaNhanVien = "XoaNhanVien";
            public static readonly string[] Nhanvienclaim = { XemNhanVien, ThemNhanVien, SuaNhanVien, XoaNhanVien };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccount(
            [FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimUser.Nhanvienclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetAllAccount(query, page, pageSize, sort);
            return this.MyStatusCode(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(RegisterDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, [ClaimUser.ThemNhanVien, ClaimUser.SuaNhanVien]);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CreateAccount(dto);
            return this.MyStatusCode(result);
        }
    }
}
