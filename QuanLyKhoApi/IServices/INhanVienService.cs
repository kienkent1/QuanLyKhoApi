using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.IServices
{
    public interface INhanVienService
    {
        Task <NhanVienDto?> ThemNhanVienAsync(NhanVienDto nhanVien);
        Task <List<NhanVien>> GetNhanVien();

    }
}
