using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface INhaCungCapService
    {
        Task<ServiceResult<NhaCungCap?>> CreateNhaCungCapAsync(NhaCungCapDto dto);
        Task<ServiceResult<List<NhaCungCap>>> GetAllNhaCungCapAsync();
        Task<ServiceResult<NhaCungCap?>> GetNhaCungCapByIdAsync(int id);
        Task<ServiceResult<bool>> UpdateNhaCungCapAsync(int id, NhaCungCapDto dto);
        Task<ServiceResult<bool>> DeleteNhaCungCapAsync(int id);
    }
}