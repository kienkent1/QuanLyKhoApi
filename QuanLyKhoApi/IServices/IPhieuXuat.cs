using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IPhieuXuat
    {
        Task<ServiceResult<bool>> ThemPhieuXuat();
    }
}
