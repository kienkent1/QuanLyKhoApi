using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;
using System.Reflection.Metadata.Ecma335;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController(ILoaiService service) : ControllerBase
    {
        private class ValidateLoaiDto
        {
            public bool IsValid { get; set; } = true;
            public string Message { get; set; }
        }
        private ValidateLoaiDto ValidateLoai(LoaiDto dto)
        {
            var vali = new ValidateLoaiDto();
            if (dto == null)
            {
                vali.IsValid = false;
                vali.Message = "Dữ liệu không được để trống";
                return vali;
            }
            if (string.IsNullOrEmpty(dto.TenLoai))
            {
                vali.IsValid = false;
                vali.Message = "Tên loại không được để trống";
                return vali;
            }
            if (dto.TenLoai.Length > 200)
            {
                vali.IsValid = false;
                vali.Message = "Tên loại không được vượt quá 100 ký tự";
                return vali;
            }
            return vali;
        }
        [HttpPost("ThemLoai")]
        public async Task<IActionResult> ThemLoai([FromForm] LoaiDto dto )
        {
            ValidateLoaiDto validate = ValidateLoai(dto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Message);
            }
            try
            {
                var newLoai = await service.ThemLoaiAsync(dto);
                return Ok(newLoai);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("SuaLoai/{id}")]
        public async Task<IActionResult> SuaLoai(int id, [FromForm] LoaiDto dto)
        {
            var validate = ValidateLoai(dto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Message);
            }
            try
            {
                var updatedLoai = await service.SuaLoai(id, dto);
                if (updatedLoai == null)
                {
                    return NotFound($"Không tìm thấy loại với mã: {id} hoặc tên loại đã tồn tại");
                }
                return Ok(updatedLoai);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetLoai")]
        public async Task<IActionResult> GetLoai([FromQuery] string? query)
        {
            try
            {
                var loaiList = await service.GetLoai(query);
                return Ok(loaiList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetLoaiById/{id}")]
        public async Task<IActionResult> GetLoaiById([FromQuery] int id)
        {
            try
            {
                var loai = await service.GetLoaiById(id);
                if (loai == null)
                {
                    return NotFound($"Không tìm thấy loại với mã: {id}");
                }
                return Ok(loai);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("XoaLoaiTam/{id}")]
        public async Task<IActionResult> XoaLoaiTam(int id)
        {
            try
            {
                var isDeleted = await service.XoaLoaiTamAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Không tìm thấy kho với mã: {id}");
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
