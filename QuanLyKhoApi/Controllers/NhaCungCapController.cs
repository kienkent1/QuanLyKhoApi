using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController(INhaCungCapService nhaCungCapService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateNhaCungCap([FromBody] NhaCungCapDto dto)
        {
                var result = await nhaCungCapService.CreateNhaCungCapAsync(dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllNhaCungCap()
        {
                var result = await nhaCungCapService.GetAllNhaCungCapAsync();
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetNhaCungCapById(int id)
        {
                var result = await nhaCungCapService.GetNhaCungCapByIdAsync(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateNhaCungCap(int id, [FromBody] NhaCungCapDto dto)
        {
                var result = await nhaCungCapService.UpdateNhaCungCapAsync(id, dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNhaCungCap(int id)
        {
                var result = await nhaCungCapService.DeleteNhaCungCapAsync(id);
                 return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}