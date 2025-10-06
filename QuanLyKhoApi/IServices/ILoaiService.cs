using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.IServices
{
    public interface ILoaiService
    {
        Task<LoaiDto?> ThemLoaiAsync(LoaiDto loai);
        Task<LoaiDto?> SuaLoai(int id, LoaiDto loai);
        Task<List<LoaiDto>> GetLoai(string? query);
        Task<LoaiDto?> GetLoaiById(int id);
        Task<bool> XoaLoaiTamAsync(int id);
    }
}
