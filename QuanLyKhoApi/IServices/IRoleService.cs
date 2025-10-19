using QuanLyKhoApi.Dto.RoleClaimDto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IRoleService
    {
        Task<ServiceResult<CreateRoleDto>> CreateRoleAsync(CreateRoleDto dto);
        Task<ServiceResult<PaginatedResult<List<CreateRoleDto>>>> GetRoleAsync(string? query, int page, int pageSize, SortOBJ? sort);
    }
}
