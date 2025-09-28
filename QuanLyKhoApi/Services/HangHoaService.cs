using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoApi.Data;

namespace QuanLyKhoApi.Services
{
    public class HangHoaService(AppDbContext db)
    {
        public List<HangHoa> GetHangHoaSV()
        {
            var hangHoas = db.HangHoa.ToList();
            return hangHoas;
        }
    }
}
