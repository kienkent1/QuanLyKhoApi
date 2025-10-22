using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.IServices
{
    public interface IPhieuNhap
    {
        Task<ServiceResult<CreatePhieuNhapDto>> CreatePhieuNhapAsync(CreatePhieuNhapDto dto);
        Task<ServiceResult<PaginatedResult<List<PhieuNhapDto>>>> GetAllPhieuNhap(string? query, int page, int pageSize, SortOBJ? sort);
        Task<ServiceResult<DetailPhieuNhapDto>> GetPhieuNhapById(int maPhieuNhap);
        Task<ServiceResult<DetailPhieuNhapDto>> ChangeStatus(int id, TRANGTHAI trangThai);
    }
}
