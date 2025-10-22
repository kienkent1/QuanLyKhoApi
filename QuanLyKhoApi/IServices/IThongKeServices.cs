using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Dto.ThongKeDto;
using QuanLyKhoApi.Helper;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.IServices
{
    public interface IThongKeServices
    {
        Task<ServiceResult<List<ThongKe>?>> GetThongKe(int year);
        Task<bool> CreateOrUpdateThongKePhieuNhap(DetailPhieuNhapDto phieuNhap, TRANGTHAI trangThai);
        Task<bool> CreateOrUpdateThongKePhieuXuat(DetailPhieuXuatDto phieuNhap, TRANGTHAI trangThai);
    }
}
