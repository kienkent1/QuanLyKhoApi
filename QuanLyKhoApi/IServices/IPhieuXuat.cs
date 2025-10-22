using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.IServices
{
    public interface IPhieuXuat
    {
        Task<ServiceResult<CreatePhieuXuatDto>> CreatePhieuXuatAsync(CreatePhieuXuatDto dto);
        Task<ServiceResult<PaginatedResult<List<PhieuXuatDto>>>> GetAllPhieuXuat(string? query, int page, int pageSize, SortOBJ? sort);
        Task<ServiceResult<DetailPhieuXuatDto>> GetPhieuXuatById(int maPhieuXuat);
        Task<ServiceResult<DetailPhieuXuatDto>> ChangeStatus(int id, TRANGTHAI trangThai);
    }
}
