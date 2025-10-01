using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;
using System.Reflection.Metadata.Ecma335;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoHangController(IKhoHangService service) : ControllerBase
    {
        private class ValidateKhoHangDto
        {
            public bool IsValid { get; set; } = true;
            public string Message { get; set; }
        }

        private ValidateKhoHangDto ValidateKhoHang(KhoHangDto dto)
        {
            var vali = new ValidateKhoHangDto();

            if (dto == null)
            {
                vali.IsValid = false;
                vali.Message = "Vui lòng điền đầy đủ thông tin bắt buộc";
                return vali;
            }

            if (string.IsNullOrWhiteSpace(dto.TenKho))
            {
                vali.IsValid = false;
                vali.Message = "Tên kho không được để trống";
                return vali;
            }

            if (string.IsNullOrWhiteSpace(dto.DiaChi))
            {
                vali.IsValid = false;
                vali.Message = "Địa chỉ không được để trống";
                return vali;
            }

            return vali;
        }

        [HttpPost("ThemKhoHang")]
        public async Task<IActionResult> ThemKhoHang([FromBody] KhoHangDto dto)
        {
            ValidateKhoHangDto validate = ValidateKhoHang(dto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Message);
            }

            try
            {
                var newKhoHang = await service.ThemKhoHangAsync(dto);
                return Ok(newKhoHang);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetKhoHang")]
        public async Task<IActionResult> GetKhoHang([FromQuery] string? query)
        {
            try
            {
                var khoHang = await service.GetKhoHang(query);
                return Ok(khoHang);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetKhoHangById")]
        public async Task<IActionResult> GetKhoHangById([FromQuery] int maKho)
        {
            try
            {
                var dto = await service.GetKhoHangById(maKho);
                if (dto == null)
                {
                    return NotFound($"Không tìm thấy kho với mã: {maKho}");
                }
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("SuaKhoHang")]
        public async Task<IActionResult> SuaKhoHang([FromQuery] int maKho, [FromBody] KhoHangDto dto)
        {
            var validate = ValidateKhoHang(dto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Message);
            }

            try
            {
                var updated = await service.SuaKhoHang(maKho, dto);
                if (updated == null)
                {
                    return NotFound($"Không tìm thấy kho với mã: {maKho}");
                }
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("XoaKhoHangTam")]
        public async Task<IActionResult> XoaKhoHangTam([FromQuery] int maKho)
        {
            try
            {
                var isDeleted = await service.XoaKhoHangTamAsync(maKho);
                if (!isDeleted)
                {
                    return NotFound($"Không tìm thấy kho với mã: {maKho}");
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
