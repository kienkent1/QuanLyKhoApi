using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {
        private readonly INhaCungCapService nhaCungCapService;

        public NhaCungCapController(INhaCungCapService nhaCungCapService)
        {
            this.nhaCungCapService = nhaCungCapService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await nhaCungCapService.GetAllNhaCungCapAsync();
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await nhaCungCapService.GetNhaCungCapByIdAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NhaCungCapDto dto)
        {
            var result = await nhaCungCapService.CreateNhaCungCapAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NhaCungCapDto dto)
        {
            var result = await nhaCungCapService.UpdateNhaCungCapAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await nhaCungCapService.DeleteNhaCungCapAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}