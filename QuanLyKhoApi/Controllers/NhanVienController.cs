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
    public class NhanVienController(INhanVienService service, AppDbContext db, AuthorizationService authorization) : ControllerBase
    {
        private class ClaimUser
        {
            public const string XemNhanVien = "XemNhanVien";
            public const string ThemNhanVien = "ThemNhanVien";
            public const string SuaNhanVien = "SuaNhanVien";
            public const string XoaNhanVien = "XoaNhanVien";
            public static readonly string[] Nhanvienclaim = { XemNhanVien, ThemNhanVien, SuaNhanVien, XoaNhanVien };
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<NhanVien>>> GetNhanVien(
            [FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await authorization.RoleHasListClaimAsync(idUser, ClaimUser.Nhanvienclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var nhanVien = await service.GetNhanVienAsync(query, page, pageSize, sort);
            return MyStatusCodeBase.MyStatusCode(this, nhanVien);

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await authorization.RoleHasListClaimAsync(idUser, ClaimUser.Nhanvienclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetNhanVienByIdAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ThemNhanVien([FromBody] NhanVienDto dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await authorization.RoleHasClaimAsync(iduser, ClaimUser.ThemNhanVien);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            ValidateNhanVienDto Validate = await ValitdateNhanVien(dto);
            if (Validate.IsValid == false)
            {
                return BadRequest(Validate.Message);
            }
            var result = await service.ThemNhanVienAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> updateNhanVien([FromQuery] Guid id, [FromForm] UpdateNhanVienDto dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await authorization.RoleHasClaimAsync(iduser, ClaimUser.SuaNhanVien);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.UpdateNhanVienAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpPatch("{id}/avatar")]
        public async Task<IActionResult> UpdateAvatar(Guid id, IFormFile file)
        {
            var result = await service.UpdateAvatarNV(id.ToString(), file);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> ProfileUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var result = await service.ProfileUser(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpPost("{id}/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassworDto Pass, [FromRoute] Guid id)
        {
            var result = await service.ChangePassword(Pass, id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpGet("/claims")]
        public async Task<IActionResult> GetClaimUser()
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await authorization.RoleHasListClaimAsync(iduser, ClaimUser.Nhanvienclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            var result = await service.GetClaimUser(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await authorization.RoleHasClaimAsync(iduser, ClaimUser.XoaNhanVien);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.DeleteNhanVienAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }
    }
}