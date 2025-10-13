using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
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
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await nhaCungCapService.CreateNhaCungCapAsync(dto);
                if (result is null)
                    return BadRequest("Không thể tạo nhà cung cấp");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllNhaCungCap()
        {
            try
            {
                var nhaCungCaps = await nhaCungCapService.GetAllNhaCungCapAsync();
                return Ok(nhaCungCaps);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetNhaCungCapById(int id)
        {
            try
            {
                var nhaCungCap = await nhaCungCapService.GetNhaCungCapByIdAsync(id);
                if (nhaCungCap is null)
                    return NotFound("Không tìm thấy nhà cung cấp");

                return Ok(nhaCungCap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateNhaCungCap(int id, [FromBody] NhaCungCapDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await nhaCungCapService.UpdateNhaCungCapAsync(id, dto);
                if (!result)
                    return BadRequest("Không thể cập nhật nhà cung cấp");

                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNhaCungCap(int id)
        {
            try
            {
                var result = await nhaCungCapService.DeleteNhaCungCapAsync(id);
                if (!result)
                    return BadRequest("Không thể xóa nhà cung cấp");

                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ: {ex.Message}");
            }
        }
    }
}