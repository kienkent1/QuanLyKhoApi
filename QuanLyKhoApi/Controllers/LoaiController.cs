using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiService service;

        public LoaiController(ILoaiService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var result = await service.GetLoai(query);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetLoaiById(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoaiDto dto)
        {
            var result = await service.ThemLoaiAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoaiDto dto)
        {
            var result = await service.SuaLoai(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.XoaLoaiTamAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}