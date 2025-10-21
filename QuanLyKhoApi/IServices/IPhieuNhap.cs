using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IPhieuNhap
    {
        Task<ServiceResult<CreatePhieuNhapDto>> CreatePhieuNhapAsync(CreatePhieuNhapDto dto);
    }
}
