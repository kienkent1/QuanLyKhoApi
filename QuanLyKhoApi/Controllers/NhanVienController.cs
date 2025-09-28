using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController(INhanVienService service) : ControllerBase
    {
        private class ValidateNhanVienDto
        {
            public bool IsValid { get; set; } = true;
            public string Message { get; set; }
        }
        private ValidateNhanVienDto ValitdateNhanVien(NhanVienDto dto)
        {
            var vali = new ValidateNhanVienDto();
            if (dto == null)
            {
                vali.IsValid = false;
                vali.Message = "Vui lòng điền đầy đủ thông tin bắt buộc";
                
            }
            if(dto.ngaySinh.AddYears(15) > DateTime.Today )
            {
                vali.IsValid = false;
                vali.Message = "Bạn Không đủ tuổi";
            }
            if(dto.gioiTinh != "Nam" || dto.gioiTinh != "Nữ")
            {
                vali.IsValid = false;
                vali.Message = "Vui lòng chọn đúng định dạng giới tính";
            }
        
            return vali;
        }
        [HttpPost("ThemNhanVien")]
        public async Task<IActionResult> ThemNhanVien([FromBody]NhanVienDto dto)
        {
            ValidateNhanVienDto Validate = ValitdateNhanVien(dto);
            if (Validate.IsValid)
            {
                return BadRequest(Validate.Message);
            } 
                
            try
            {
                var NewNhanVien = await service.ThemNhanVienAsync(dto);
                return Ok(NewNhanVien);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }
    }
}
