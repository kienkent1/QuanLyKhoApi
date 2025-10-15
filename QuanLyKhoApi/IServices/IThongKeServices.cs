using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.ThongKeDto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IThongKeServices
    {
        Task<ServiceResult<List<ThongKe>?>> GetThongKe( int year);
        Task<bool> CreateThongKe(int Year, int Month);
        Task<bool> UpdateThongKe(UpdateThongKeDto  dto);
    }
}
