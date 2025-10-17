using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface ILoaiService
    {
        Task<ServiceResult<LoaiDto>> ThemLoaiAsync(LoaiDto loai);
        Task<ServiceResult<LoaiDto?>> SuaLoai(int id, LoaiDto loai);
        Task<ServiceResult<List<LoaiDto>>> GetLoai(string? query);
        Task<ServiceResult<LoaiDto?>> GetLoaiById(int id);
        Task<ServiceResult<bool>> XoaLoaiTamAsync(int id);
    }
}
