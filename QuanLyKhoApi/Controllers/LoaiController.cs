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
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiService service;
        private readonly AuthorizationService auth;

        public LoaiController(ILoaiService service, AuthorizationService authorization)
        {
            this.service = service;
            auth = authorization;
        }

        private class ClaimLoai
        {
            public const string XemLoai = "XemLoai";
            public const string ThemLoai = "ThemLoai";
            public const string SuaLoai = "SuaLoai";
            public const string XoaLoai = "XoaLoai";
            public static readonly string[] Loaiclaim = { XemLoai, ThemLoai, SuaLoai, XoaLoai };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimLoai.Loaiclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetLoai(query, page, pageSize, sort);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasListClaimAsync(idUser, ClaimLoai.Loaiclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetLoaiById(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LoaiDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimLoai.ThemLoai);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.ThemLoaiAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateLoaiDto dto)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimLoai.SuaLoai);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.SuaLoai(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var idUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isHasClaim = await auth.RoleHasClaimAsync(idUser, ClaimLoai.XoaLoai);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.XoaLoaiTamAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}