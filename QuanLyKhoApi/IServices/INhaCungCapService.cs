using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface INhaCungCapService
    {
        Task<ServiceResult<NhaCungCap?>> CreateNhaCungCapAsync(NhaCungCapDto dto);
        Task<ServiceResult<PaginatedResult<List<NhaCungCap>>>> GetAllNhaCungCapAsync(string? query, int page, int pageSize, SortOBJ? sort);
        Task<ServiceResult<NhaCungCap?>> GetNhaCungCapByIdAsync(int id);
        Task<ServiceResult<NhaCungCapUpdateDto>> UpdateNhaCungCapAsync(int id, NhaCungCapUpdateDto dto);
        Task<ServiceResult<bool>> DeleteNhaCungCapAsync(int id);
    }
}