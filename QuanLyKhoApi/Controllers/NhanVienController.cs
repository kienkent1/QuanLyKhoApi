using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLyKhoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController(INhanVienService service, AppDbContext db) : ControllerBase
    {
        private class ValidateNhanVienDto
        {
            public bool IsValid { get; set; } = true;
            public string Message { get; set; }
        }
        private async Task<ValidateNhanVienDto> ValitdateNhanVien(NhanVienDto dto)
        {
            var vali = new ValidateNhanVienDto();
            if (dto == null)
            {
                vali.IsValid = false;
                vali.Message = "Vui lòng điền đầy đủ thông tin bắt buộc";
                return vali;
            }
            if (dto.ngaySinh.AddYears(15) > DateTime.Today)
            {
                vali.IsValid = false;
                vali.Message = "Bạn Không đủ tuổi";
                return vali;
            }
            var isEmailExit = await db.NhanVien.AnyAsync(u => u.email == dto.email);
            if (isEmailExit)
            {
                vali.IsValid = false;
                vali.Message = "Email đã tồn tại";
                return vali;
            }
            return vali;
        }
        [HttpGet]
        public async Task<ActionResult<List<NhanVien>>> GetNhanVien(
            [FromQuery]string? query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12)
        {
                var nhanVien = await service.GetNhanVienAsync(query, page, pageSize);
            return MyStatusCodeBase.MyStatusCode(this, nhanVien);

        }
        [HttpPost("ThemNhanVien")]
        public async Task<IActionResult> ThemNhanVien([FromBody] NhanVienDto dto)
        {
            ValidateNhanVienDto Validate = await ValitdateNhanVien(dto);
            if (Validate.IsValid == false)
            {
                return BadRequest(Validate.Message);
            }

            try
            {
                var result = await service.ThemNhanVienAsync(dto);
                return StatusCode(result.StatusCode, new
                {
                    success = result.Success,
                    message = result.Message,
                    data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("UpdateNhanVien/{id}")]
        public async Task<IActionResult> updateNhanVien([FromQuery] Guid id, [FromForm] UpdateNhanVienDto dto)
        {
            try
            {
                var result = await service.UpdateNhanVienAsync(id, dto);
                return MyStatusCodeBase.MyStatusCode(this, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPatch("UpdateAvatar/{id}")]
        public async Task<IActionResult> UpdateAvatarNV([FromRoute] string id, IFormFile file)
        {
            try
            {
                var result = await service.UpdateAvatarNV(id, file);
                return MyStatusCodeBase.MyStatusCode(this, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpGet("ProfileUser")]
        public async Task<IActionResult> ProfileUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            try
            {
                var result = await service.ProfileUser(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassworDto Pass, [FromRoute] Guid id)
        {
            try
            {
                var result = await service.ChangePassword(Pass, id);
                return StatusCode(result.StatusCode, new
                {
                    success = result.Success,
                    message = result.Message,
                    data = result.Data
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpGet("GetClaimUser")]
        public async Task<IActionResult> GetClaimUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            try
            {
                var result = await service.GetClaimUser(id);
                return MyStatusCodeBase.MyStatusCode(this, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
