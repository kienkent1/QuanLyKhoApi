using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IHangHoaService
    {
        Task<ServiceResult<HangHoaDto>> CreateHangHoaAsync(HangHoaDto dto);
        Task<ServiceResult<PaginatedResult<List<ListHangHoaDto>>>> GetAllHangHoaAsync(string? query, int page, int pageSize, SortOBJ? sort);
        Task<ServiceResult<DetailHangHoaDto>> GetHangHoaByIdAsync(string id);
        Task<ServiceResult<HangHoaDto>> UpdateHangHoaAsync(Guid id, HangHoaDto dto);
        Task<ServiceResult<bool>> DeleteHangHoaAsync(Guid id);
        Task<ServiceResult<CauHinhDto>> GetCauHinhById(Guid Id);
        Task<ServiceResult<List<CreateCauHinhDto>>> CreateMultipleConfigsAsync(Guid maHH, List<CreateCauHinhDto> dto);
        Task<ServiceResult<List<HinhAnhDto>>> AddHinhAnhCauHing(Guid id, IFormFile[] files);
        Task<ServiceResult<bool>> DeleteHinhAnhCauHinhAsync(Guid[] id);
        Task<ServiceResult<DetailHangHoaDto>> FindByBarCode(IFormFile file);
        Task<ServiceResult<string>> GenBarCode(string id);
    }
}