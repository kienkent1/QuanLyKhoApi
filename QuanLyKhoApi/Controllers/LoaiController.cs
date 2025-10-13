using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
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
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await service.ThemLoaiAsync(dto);
                if (result is null)
                    return BadRequest("Không thể tạo loại hoặc tên loại đã tồn tại");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLoai(int id, [FromBody] LoaiDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await service.SuaLoai(id, dto);
                if (result is null)
                    return NotFound("Không tìm thấy loại hoặc tên loại đã tồn tại");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLoai([FromQuery] string? query)
        {
            try
            {
                var loaiList = await service.GetLoai(query);
                return Ok(loaiList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetLoaiById(int id)
        {
            try
            {
                var loai = await service.GetLoaiById(id);
                if (loai is null)
                    return NotFound("Không tìm thấy loại");

                return Ok(loai);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLoai(int id)
        {
            try
            {
                var result = await service.XoaLoaiTamAsync(id);
                if (!result)
                    return NotFound("Không tìm thấy loại");

                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
    }
}