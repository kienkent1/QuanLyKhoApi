using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
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
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _hangHoaService.CreateHangHoaAsync(dto);
                if (result is null) return BadRequest("Không thể tạo hàng hóa hoặc loại/nhà cung cấp không tồn tại");
                var hangHoaDto = _mapper.Map<HangHoaDto>(result);
                return Ok(hangHoaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpPost("create-cau-hinh")]
        public async Task<IActionResult> CreateCauHinh([FromBody] CauHinhDto dto)
        {
            try
            {
                var result = await _hangHoaService.CreateCauHinhAsync(dto);
                if (result is null) return BadRequest("Không thể tạo cấu hình hoặc hàng hóa không tồn tại");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpGet("/")]
        public async Task<IActionResult> GetAllHangHoa()
        {
            try
            {
                var hangHoas = await _hangHoaService.GetAllHangHoaAsync();
                return Ok(hangHoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetHangHoaById(Guid id)
        {
            try
            {
                var hangHoa = await _hangHoaService.GetHangHoaByIdAsync(id);
                if (hangHoa is null) return NotFound("Không tìm thấy hàng hóa");
                return Ok(hangHoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpPut("update-hang-hoa/{id}")]
        public async Task<IActionResult> UpdateHangHoa(Guid id, [FromBody] HangHoaDto dto)
        {
            try
            {
                var result = await _hangHoaService.UpdateHangHoaAsync(id, dto);
                if (!result) return BadRequest("Không thể cập nhật hàng hóa");
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpDelete("delete-hang-hoa/{id}")]
        public async Task<IActionResult> DeleteHangHoa(Guid id)
        {
            try
            {
                var result = await _hangHoaService.DeleteHangHoaAsync(id);
                if (!result) return BadRequest("Không thể xóa hàng hóa");
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpPut("update-cau-hinh/{id}")]
        public async Task<IActionResult> UpdateCauHinh(Guid id, [FromBody] CauHinhDto dto)
        {
            try
            {
                var result = await _hangHoaService.UpdateCauHinhAsync(id, dto);
                if (!result) return BadRequest("Không thể cập nhật cấu hình");
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpDelete("delete-cau-hinh/{id}")]
        public async Task<IActionResult> DeleteCauHinh(Guid id)
        {
            try
            {
                var result = await _hangHoaService.DeleteCauHinhAsync(id);
                if (!result) return BadRequest("Không thể xóa cấu hình");
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
    }
}