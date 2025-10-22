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
    public class PhieuNhapController(IPhieuNhap service, AuthorizationService auth) : ControllerBase
    {
        private class ClaimPhieuNhap
        {
            public const string XemPhieuNhap = "XemPhieuNhap";
            public const string ThemPhieuNhap = "ThemPhieuNhap";
            public static readonly string[] PhieuNhap = { XemPhieuNhap, ThemPhieuNhap };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhieuNhap(
            [FromQuery] string? query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimPhieuNhap.PhieuNhap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetAllPhieuNhap(query, page, pageSize, sort);
            return this.MyStatusCode(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhieuNhap([FromBody] CreatePhieuNhapDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimPhieuNhap.ThemPhieuNhap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CreatePhieuNhapAsync(dto);
            return this.MyStatusCode(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhieuNhapById(int id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimPhieuNhap.PhieuNhap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetPhieuNhapById(id);
            return this.MyStatusCode(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] TRANGTHAI trangThai)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimPhieuNhap.ThemPhieuNhap);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.ChangeStatus(id, trangThai);
            return this.MyStatusCode(result);
        }
    }
}
