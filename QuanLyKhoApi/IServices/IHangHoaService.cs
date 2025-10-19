using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IHangHoaService
    {
        Task<ServiceResult<HangHoa>> CreateHangHoaAsync(HangHoaDto dto);
        Task<ServiceResult<IEnumerable<HangHoaDto>>> GetAllHangHoaAsync();
        Task<ServiceResult<HangHoaDto>> GetHangHoaByIdAsync(Guid id);
        Task<ServiceResult<HangHoa>> UpdateHangHoaAsync(Guid id, HangHoaDto dto);
        Task<ServiceResult<bool>> DeleteHangHoaAsync(Guid id);
        Task<ServiceResult<IEnumerable<CauHinhDto>>> GetCauHinhByHangHoaIdAsync(Guid hangHoaId);
        Task<ServiceResult<List<CauHinh>>> CreateMultipleConfigsAsync(CreateMultipleConfigsDto dto);
        Task<ServiceResult<IEnumerable<HangHoaDto>>> GetHangHoaCanhBaoAsync();
    }
}