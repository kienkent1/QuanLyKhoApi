using QuanLyKhoApi.Dto.RoleClaimDto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IRoleService
    {
        Task<ServiceResult<CreateRoleDto>> CreateRoleAsync(CreateRoleDto dto);
        Task<ServiceResult<PaginatedResult<List<ListRoleDto>>>> GetRoleAsync(string? query, int page, int pageSize, SortOBJ? sort);
        Task<ServiceResult<ListRoleDto>> GetOneRoleAsync(string id);
        Task<ServiceResult<bool>> DeleteRoleAsync(string id);
        Task<ServiceResult<CreateRoleDto>> UpdateRoleAsync(string id, CreateRoleDto dto);
        Task<ServiceResult<List<ClaimDto>>> GetAllClaimsAsync();
        Task<ServiceResult<RoleAccResponseDto>> CapQuyen(RoleAccDto dto);
    }
}
