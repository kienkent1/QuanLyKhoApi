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
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService service;
        private readonly AppDbContext db;
        private readonly AuthorizationService authorization;

        public NhanVienController(INhanVienService service, AppDbContext db, AuthorizationService authorization)
        {
            this.service = service;
            this.db = db;
            this.authorization = authorization;
        }

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
        public async Task<ActionResult<List<NhanVien>>> GetAll(
            [FromQuery] string? query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12)
        {
            var nhanVien = await service.GetNhanVienAsync(query, page, pageSize);
            return MyStatusCodeBase.MyStatusCode(this, nhanVien);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await service.GetNhanVienByIdAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NhanVienDto dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var iduserGuid = Guid.Parse(iduser);
            var isHasClaim = await authorization.RoleHasClaimAsync(iduserGuid, "ThemNhanVien");
            if (isHasClaim == false)
            {
                return StatusCode(403, "Bạn không có quyền truy cập chức năng này");
            }
            ValidateNhanVienDto Validate = await ValitdateNhanVien(dto);
            if (Validate.IsValid == false)
            {
                return BadRequest(Validate.Message);
            }
            var result = await service.ThemNhanVienAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateNhanVienDto dto)
        {
            var result = await service.UpdateNhanVienAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPatch("{id}/avatar")]
        public async Task<IActionResult> UpdateAvatar(Guid id, IFormFile file)
        {
            var result = await service.UpdateAvatarNV(id.ToString(), file);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> ProfileUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var result = await service.ProfileUser(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("{id}/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassworDto Pass, [FromRoute] Guid id)
        {
            var result = await service.ChangePassword(Pass, id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpGet("permissions")]
        public async Task<IActionResult> GetClaimUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var result = await service.GetClaimUser(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await service.DeleteNhanVienAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}