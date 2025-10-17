using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController(ILoaiService service) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateLoai([FromBody] LoaiDto dto)
        {

                var result = await service.ThemLoaiAsync(dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLoai(int id, [FromBody] LoaiDto dto)
        {

                var result = await service.SuaLoai(id, dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLoai([FromQuery] string? query)
        {
                var result = await service.GetLoai(query);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetLoaiById(int id)
        {
                var result = await service.GetLoaiById(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLoai(int id)
        {
                var result = await service.XoaLoaiTamAsync(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}