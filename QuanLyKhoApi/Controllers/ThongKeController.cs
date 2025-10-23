using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController(IThongKeServices service, AuthorizationService auth) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetThongKe([FromQuery] int year)
        {
            var result = await service.GetThongKe(year);
            return this.MyStatusCode(result);
        }

        [HttpGet("confirmAcc")]
        public async Task<IActionResult> GetConfirmAccCount(
            [FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ["ThemNhanVien", "SuaNhanVien", "XemNhanVien", "XoaNhanVien"]);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.ListConfirm(query, page, pageSize, sort);
            return this.MyStatusCode(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ConfirmAcc([FromRoute] Guid id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ["ThemNhanVien", "SuaNhanVien"]);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.ComfirmAcc(id);
            return this.MyStatusCode(result);
        }
    }
}
