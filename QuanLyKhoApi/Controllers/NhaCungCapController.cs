using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController(INhaCungCapService nhaCungCapService, AuthorizationService auth) : ControllerBase
    {

        private class ClaimNhaCungCap
        {
            public const string XemNhaCungCap = "XemNhaCungCap";
            public const string ThemNhaCungCap = "ThemNhaCungCap";
            public const string SuaNhaCungCap = "SuaNhaCungCap";
            public const string XoaNhaCungCap = "XoaNhaCungCap";
            public static readonly string[] NhaCungCapclaim = { XemNhaCungCap, ThemNhaCungCap, SuaNhaCungCap, XoaNhaCungCap };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimNhaCungCap.NhaCungCapclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await nhaCungCapService.GetAllNhaCungCapAsync(query, page, pageSize, sort);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimNhaCungCap.NhaCungCapclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await nhaCungCapService.GetNhaCungCapByIdAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NhaCungCapDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimNhaCungCap.ThemNhaCungCap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await nhaCungCapService.CreateNhaCungCapAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] NhaCungCapUpdateDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimNhaCungCap.SuaNhaCungCap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await nhaCungCapService.UpdateNhaCungCapAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimNhaCungCap.XoaNhaCungCap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await nhaCungCapService.DeleteNhaCungCapAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}