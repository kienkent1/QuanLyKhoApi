using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Security.Claims;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuXuatController(IPhieuXuat service, AuthorizationService auth) : ControllerBase
    {
        private class ClaimPhieuXuat
        {
            public const string XemPhieuXuat = "XemPhieuXuat";
            public const string ThemPhieuXuat = "ThemPhieuXuat";
            public static readonly string[] PhieuXuat = { XemPhieuXuat, ThemPhieuXuat };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhieuXuat(
            [FromQuery] string? query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimPhieuXuat.PhieuXuat);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetAllPhieuXuat(query, page, pageSize, sort);
            return this.MyStatusCode(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePhieuXuat([FromBody] CreatePhieuXuatDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimPhieuXuat.ThemPhieuXuat);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CreatePhieuXuatAsync(dto);
            return this.MyStatusCode(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhieuXuatById(int id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimPhieuXuat.PhieuXuat);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetPhieuXuatById(id);
            return this.MyStatusCode(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] TRANGTHAI trangThai)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimPhieuXuat.ThemPhieuXuat);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.ChangeStatus(id, trangThai);
            return this.MyStatusCode(result);
        }
    }
}
