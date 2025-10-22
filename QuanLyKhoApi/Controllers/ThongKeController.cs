using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController(IThongKeServices service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetThongKe([FromQuery] int year)
        {
            var result = await service.GetThongKe(year);
            return this.MyStatusCode(result);
        }
    }
}
