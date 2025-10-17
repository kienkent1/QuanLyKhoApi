using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IHangHoaService
    {
        Task<ServiceResult<HangHoa>> CreateHangHoaAsync(HangHoaDto dto);
        Task<ServiceResult<CauHinh?>> CreateCauHinhAsync(CauHinhDto dto);
        Task<ServiceResult<IEnumerable<HangHoaDto>>> GetAllHangHoaAsync();
        Task<ServiceResult<HangHoaDto>> GetHangHoaByIdAsync(Guid id);
        Task<ServiceResult<bool>> UpdateHangHoaAsync(Guid id, HangHoaDto dto);
        Task<ServiceResult<bool>> DeleteHangHoaAsync(Guid id);
        Task<ServiceResult<bool>> UpdateCauHinhAsync(Guid id, CauHinhDto dto);
        Task<ServiceResult<bool>> DeleteCauHinhAsync(Guid id);
    }
}