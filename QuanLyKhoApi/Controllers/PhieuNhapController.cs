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
    public class PhieuNhapController(IPhieuNhap service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPhieuNhap([FromQuery] string? query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] SortOBJ? sort = null)
        {
            var result = await service.GetAllPhieuNhap(query, page, pageSize, sort);
            return this.MyStatusCode(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhieuNhap([FromBody] CreatePhieuNhapDto dto)
        {
            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.MaNV = Guid.Parse(idUser!);
            var result = await service.CreatePhieuNhapAsync(dto);
            return this.MyStatusCode(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhieuNhapById(int id)
        {
            var result = await service.GetPhieuNhapById(id);
            return this.MyStatusCode(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] TRANGTHAI trangThai)
        {
            var result = await service.ChangeStatus(id, trangThai);
            return this.MyStatusCode(result);
        }
    }
}
