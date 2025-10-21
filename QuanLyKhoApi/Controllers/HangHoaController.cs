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
    public class HangHoaController(IHangHoaService service) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HangHoaDto dto)
        {
            var result = await service.CreateHangHoaAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var result = await service.GetAllHangHoaAsync(query, page, pageSize, sort);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetHangHoaByIdAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("scan")]
        public async Task<IActionResult> GetByScanCode([FromForm] Getfile dto)
        {
            var result = await service.FindByBarCode(dto.file);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}/gen-barcode")]
        public async Task<IActionResult> GenBarCode(string id)
        {
            var result = await service.GenBarCode(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
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

        [HttpGet("{cauHinhId}/configs")]
        public async Task<IActionResult> GetConfigs(Guid hangHoaId)
        {
            var result = await service.GetCauHinhById(hangHoaId);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("configs/{id}")]
        public async Task<IActionResult> CreateMultipleConfigs([FromRoute] Guid id, [FromBody] List<CreateCauHinhDto> dto)
        {
            var result = await service.CreateMultipleConfigsAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("configs/{id}/images")]
        public async Task<IActionResult> AddHinhAnhCauHing([FromRoute] Guid id, [FromForm] IFormFile[] files)
        {
            var result = await service.AddHinhAnhCauHing(id, files);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("configs/images")]
        public async Task<IActionResult> DeleteHinhAnhCauHinh([FromBody] Guid[] id)
        {
            var result = await service.DeleteHinhAnhCauHinhAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

    }
}