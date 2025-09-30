using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.IServices
{
    public interface INhanVienService
    {
        Task <NhanVienDto?> ThemNhanVienAsync(NhanVienDto nhanVien);
        Task <IQueryable<NhanVien>> GetNhanVienAsync();
        Task<NhanVienDto> UpdateNhanVienAsync(Guid id, NhanVienDto dto);
        Task<ProfileUserDto> ProfileUser(string id);
        Task<string> ChangePassword(ChangePassworDto Pass, Guid id);
    }
}
