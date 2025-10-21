using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto.RoleClaimDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService service, AuthorizationService auth) : ControllerBase
    {
        private class ClaimRole
        {
            public const string XemRole = "XemRole";
            public const string ThemRole = "ThemRole";
            public const string XoaRole = "XoaRole";
            public const string SuaRole = "SuaRole";
            public static readonly string[] Roleclaims = { XemRole, ThemRole, SuaRole, XoaRole };
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesAsync([FromQuery] string? query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimRole.Roleclaims);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetRoleAsync(query, page, pageSize, sort);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneRoleAsync([FromRoute] string id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimRole.Roleclaims);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetOneRoleAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimRole.ThemRole);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CreateRoleAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute] string id, [FromBody] CreateRoleDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimRole.SuaRole);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.UpdateRoleAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimRole.XoaRole);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.DeleteRoleAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetAllClaimsAsync()
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimRole.Roleclaims);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetAllClaimsAsync();
            return MyStatusCodeBase.MyStatusCode(this, result);

        }

        [HttpPost("capquyen")]
        public async Task<IActionResult> CapQuyen(RoleAccDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, "Admin");
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CapQuyen(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}
