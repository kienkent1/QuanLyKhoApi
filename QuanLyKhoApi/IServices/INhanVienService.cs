using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface INhanVienService
    {
        Task <ServiceResult< NhanVienDto>> ThemNhanVienAsync(NhanVienDto nhanVien);
        Task <IQueryable<NhanVien>> GetNhanVienAsync();
        Task<ServiceResult<UpdateNhanVienDto>> UpdateNhanVienAsync(Guid id, UpdateNhanVienDto dto);
        Task<ServiceResult<ProfileUserDto>> ProfileUser(string id);
        Task<ServiceResult<string>> ChangePassword(ChangePassworDto Pass, Guid id);
        Task<ServiceResult<string>> UpdateAvatarNV(string id, IFormFile file);
        Task<ServiceResult<HashSet<string>>> GetClaimUser(string id);
    }
}

