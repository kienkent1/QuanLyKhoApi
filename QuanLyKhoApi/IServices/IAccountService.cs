using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IAccountService
    {
        Task<ServiceResult<PaginatedResult<List<AccountDto>>>> GetAllAccount(string? query, int page, int pageSize, SortOBJ? sort);
        Task<ServiceResult<RegisterDto>> CreateAccount(RegisterDto dto);
    }
}
