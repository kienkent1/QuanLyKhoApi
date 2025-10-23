using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Net;
using System.Security.Claims;

namespace QuanLyKhoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangHoaController(IHangHoaService service, AuthorizationService auth) : ControllerBase
    {
        private class ClaimHangHoa
        {
            public const string XemHangHoa = "XemHangHoa";
            public const string ThemHangHoa = "ThemHangHoa";
            public const string SuaHangHoa = "SuaHangHoa";
            public const string XoaHangHoa = "XoaHangHoa";
            public static readonly string[] HangHoaclaim = { XemHangHoa, ThemHangHoa, SuaHangHoa, XoaHangHoa };
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HangHoaDto dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasClaimAsync(iduser, ClaimHangHoa.ThemHangHoa);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CreateHangHoaAsync(dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? query = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] SortOBJ? sort = null)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasListClaimAsync(iduser, ClaimHangHoa.HangHoaclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetAllHangHoaAsync(query, page, pageSize, sort);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasListClaimAsync(iduser, ClaimHangHoa.HangHoaclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetHangHoaByIdAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("scan")]
        public async Task<IActionResult> GetByScanCode([FromForm] Getfile dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasListClaimAsync(iduser, ClaimHangHoa.HangHoaclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.FindByBarCode(dto.file);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{id}/gen-barcode")]
        public async Task<IActionResult> GenBarCode(string id)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasListClaimAsync(iduser, ClaimHangHoa.HangHoaclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GenBarCode(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HangHoaDto dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasClaimAsync(iduser, ClaimHangHoa.SuaHangHoa);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.UpdateHangHoaAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasClaimAsync(iduser, ClaimHangHoa.XoaHangHoa);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.DeleteHangHoaAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpGet("{cauHinhId}/configs")]
        public async Task<IActionResult> GetConfigs(Guid hangHoaId)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasListClaimAsync(iduser, ClaimHangHoa.HangHoaclaim);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.GetCauHinhById(hangHoaId);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("configs/{id}")]
        public async Task<IActionResult> CreateMultipleConfigs([FromRoute] Guid id, [FromBody] List<CreateCauHinhDto> dto)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasClaimAsync(iduser, ClaimHangHoa.ThemHangHoa);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.CreateMultipleConfigsAsync(id, dto);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpPost("configs/{id}/images")]
        public async Task<IActionResult> AddHinhAnhCauHing([FromRoute] Guid id, [FromForm] IFormFile[] files)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasClaimAsync(iduser, ClaimHangHoa.SuaHangHoa);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.AddHinhAnhCauHing(id, files);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

        [HttpDelete("configs/images")]
        public async Task<IActionResult> DeleteHinhAnhCauHinh([FromBody] Guid[] id)
        {
            var iduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isHasClaim = await auth.RoleHasClaimAsync(iduser, ClaimHangHoa.SuaHangHoa);
            if (isHasClaim.Success == false)
            {
                return MyStatusCodeBase.MyStatusCode(this, isHasClaim);
            }
            var result = await service.DeleteHinhAnhCauHinhAsync(id);
            return MyStatusCodeBase.MyStatusCode(this, result);
        }

    }
}