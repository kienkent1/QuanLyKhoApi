using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Security.Claims;

namespace QuanLyKhoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaService service;

        public HangHoaController(IHangHoaService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HangHoaDto dto)
        {
            var result = await service.CreateHangHoaAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllHangHoaAsync();
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Console.WriteLine($"Controller nhận ID: {id}");
                var result = await service.GetHangHoaByIdAsync(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong Controller: {ex.Message}");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HangHoaDto dto)
        {
            var result = await service.UpdateHangHoaAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await service.DeleteHangHoaAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{hangHoaId}/configs")]
        public async Task<IActionResult> GetConfigs(Guid hangHoaId)
        {
            var result = await service.GetCauHinhByHangHoaIdAsync(hangHoaId);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("configs")]
        public async Task<IActionResult> CreateMultipleConfigs([FromBody] CreateMultipleConfigsDto dto)
        {
            var result = await service.CreateMultipleConfigsAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("canh-bao")]
        public async Task<IActionResult> GetHangHoaCanhBao()
        {
            var result = await service.GetHangHoaCanhBaoAsync();
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}