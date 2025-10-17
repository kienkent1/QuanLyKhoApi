using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController(IHangHoaService hangHoaService, IMapper mapper) : ControllerBase
    {
        private readonly IHangHoaService _hangHoaService = hangHoaService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("create-hang-hoa")]
        public async Task<IActionResult> CreateHangHoa([FromBody] HangHoaDto dto)
        {

                var result = await _hangHoaService.CreateHangHoaAsync(dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("create-cau-hinh")]
        public async Task<IActionResult> CreateCauHinh([FromBody] CauHinhDto dto)
        {
                var result = await _hangHoaService.CreateCauHinhAsync(dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllHangHoa()
        {
                var result = await _hangHoaService.GetAllHangHoaAsync();
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetHangHoaById(Guid id)
        {
                var result = await _hangHoaService.GetHangHoaByIdAsync(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("update-hang-hoa/{id}")]
        public async Task<IActionResult> UpdateHangHoa(Guid id, [FromBody] HangHoaDto dto)
        {
                var result = await _hangHoaService.UpdateHangHoaAsync(id, dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("delete-hang-hoa/{id}")]
        public async Task<IActionResult> DeleteHangHoa(Guid id)
        {
                var result = await _hangHoaService.DeleteHangHoaAsync(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("update-cau-hinh/{id}")]
        public async Task<IActionResult> UpdateCauHinh(Guid id, [FromBody] CauHinhDto dto)
        {
                var result = await _hangHoaService.UpdateCauHinhAsync(id, dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("delete-cau-hinh/{id}")]
        public async Task<IActionResult> DeleteCauHinh(Guid id)
        {
                var result = await _hangHoaService.DeleteCauHinhAsync(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}