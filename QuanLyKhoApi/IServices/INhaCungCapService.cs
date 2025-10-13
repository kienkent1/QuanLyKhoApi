using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.IServices
{
    public interface INhaCungCapService
    {
        Task<NhaCungCap?> CreateNhaCungCapAsync(NhaCungCapDto dto);
        Task<IEnumerable<NhaCungCap>> GetAllNhaCungCapAsync();
        Task<NhaCungCap?> GetNhaCungCapByIdAsync(int id);
        Task<bool> UpdateNhaCungCapAsync(int id, NhaCungCapDto dto);
        Task<bool> DeleteNhaCungCapAsync(int id);
    }
}