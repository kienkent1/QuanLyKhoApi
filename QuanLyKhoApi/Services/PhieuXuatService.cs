using QuanLyKhoApi.Data;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class PhieuXuatService(AppDbContext db) : IPhieuXuat
    {
        public Task<ServiceResult<bool>> ThemPhieuXuat()
        {
            throw new NotImplementedException();
        }
    }
}
