using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.IServices
{
    public interface IKhoHangService
    {
        Task<KhoHangDto?> ThemKhoHangAsync(KhoHangDto khoHang);
        Task<List<KhoHangDto>> GetKhoHang(string? query);
        Task<bool> XoaKhoHangTamAsync(int makho);
        Task<KhoHangDto?> SuaKhoHang(int makho, KhoHangDto khohang);
        Task<KhoHangDto?> GetKhoHangById(int maKho);
    }
}