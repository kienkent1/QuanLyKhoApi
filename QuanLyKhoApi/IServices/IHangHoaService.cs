using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.IServices
{
    public interface IHangHoaService
    {
        Task<HangHoa?> CreateHangHoaAsync(HangHoaDto dto);
        Task<CauHinh?> CreateCauHinhAsync(CauHinhDto dto);
        Task<IEnumerable<HangHoaDto>> GetAllHangHoaAsync();
        Task<HangHoaDto?> GetHangHoaByIdAsync(Guid id);
        Task<bool> UpdateHangHoaAsync(Guid id, HangHoaDto dto);
        Task<bool> DeleteHangHoaAsync(Guid id);
        Task<bool> UpdateCauHinhAsync(Guid id, CauHinhDto dto);
        Task<bool> DeleteCauHinhAsync(Guid id);
    }
}