using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.RoleClaimDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Linq;

namespace QuanLyKhoApi.Services
{
    public class RoleService(AppDbContext db) : IRoleService
    {
        public async Task<ServiceResult<CreateRoleDto>> CreateRoleAsync(CreateRoleDto dto)
        {
            try
            {
                var role = await db.Role.AddAsync(new Role()
                {
                    VaiTro = dto.VaiTro,
                });
                if (dto.ClaimIds is not null && dto.ClaimIds.Count > 0)
                {
                    foreach (var claimId in dto.ClaimIds)
                    {
                        await db.RoleClaims.AddAsync(new RoleClaim()
                        {
                            RoleId = role.Entity.Id,
                            ClaimId = claimId
                        });
                    }
                }
                await db.SaveChangesAsync();
                return ServiceResult<CreateRoleDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<CreateRoleDto>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<CreateRoleDto>>>> GetRoleAsync(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var roles = db.Role.Select(r => new CreateRoleDto
                {
                    VaiTro = r.VaiTro,
                    ClaimIds = db.RoleClaims.Where(rc => rc.RoleId == r.Id).OrderBy(o => o.Role.VaiTro).Select(rc => rc.ClaimId).ToList()
                }).AsQueryable();
                if (query is not null) roles = roles.Where(r => r.VaiTro.Contains(query));
                var TotalItems = await roles.CountAsync();
                var paginatedRoles = await Pagination<CreateRoleDto>.PaginationAsync(roles, page, pageSize, sort);

                return ServiceResult<PaginatedResult<List<CreateRoleDto>>>.Ok(paginatedRoles);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<CreateRoleDto>>>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }
    }
}
