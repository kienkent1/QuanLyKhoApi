using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.RoleClaimDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Linq.Dynamic.Core;

namespace QuanLyKhoApi.Services
{
    public class RoleService(AppDbContext db, IMapper mapper) : IRoleService
    {

        public async Task<ServiceResult<CreateRoleDto>> CreateRoleAsync(CreateRoleDto dto)
        {
            try
            {
                var idRole = ConverToSlug.GetSlug(dto.VaiTro);
                var existingRole = await db.Role.FirstOrDefaultAsync(r => r.Id == idRole);
                if (existingRole is not null) return ServiceResult<CreateRoleDto>.Fail("Vai trò đã tồn tại hoặc tên bị trùng", 400);
                var role = await db.Role.AddAsync(new Role()
                {
                    Id = idRole,
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

        public async Task<ServiceResult<PaginatedResult<List<ListRoleDto>>>> GetRoleAsync(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var roles = db.Role
            .Select(r => new ListRoleDto
            {
                Id = r.Id,
                VaiTro = r.VaiTro,

                Claims = db.RoleClaims
                    .Where(rc => rc.RoleId == r.Id)
                    .OrderBy(o => o.Claim.Id)
                    .Select(rc => new ClaimDto
                    {
                        Id = rc.ClaimId,
                        TenQuyen = rc.Claim.Quyen
                    }).ToList()

            })
            .AsQueryable();
                if (query is not null) roles = roles.Where(r => r.VaiTro.Contains(query));
                var TotalItems = await roles.CountAsync();
                var paginatedRoles = await Pagination<ListRoleDto>.PaginationAsync(roles, page, pageSize, sort);

                return ServiceResult<PaginatedResult<List<ListRoleDto>>>.Ok(paginatedRoles);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<ListRoleDto>>>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<ListRoleDto>> GetOneRoleAsync(string id)
        {
            try
            {
                var role = await db.Role.FindAsync(id);
                if (role is null) return ServiceResult<ListRoleDto>.Fail("Không tìm thấy vai trò", 404);
                var dto = new ListRoleDto
                {
                    VaiTro = role.VaiTro,
                    Claims = await db.RoleClaims
                        .Where(rc => rc.RoleId == role.Id)
                        .OrderBy(o => o.Claim.Id)
                        .Select(rc => new ClaimDto
                        {
                            Id = rc.ClaimId,
                            TenQuyen = rc.Claim.Quyen
                        }).ToListAsync()
                };
                return ServiceResult<ListRoleDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<ListRoleDto>.Fail($"Lỗi: {ex.Message}", 500);
            }

        }

        public async Task<ServiceResult<bool>> DeleteRoleAsync(string id)
        {
            try
            {
                var role = await db.Role.FirstOrDefaultAsync(r => r.Id == id && r.Id != "admin");
                if (role is null) return ServiceResult<bool>.Fail("Không tìm thấy vai trò", 404);
                role.Deleted = true;
                role.DeletedAt = DateTime.UtcNow;
                db.Role.Update(role);
                await db.SaveChangesAsync();
                return ServiceResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<CreateRoleDto>> UpdateRoleAsync(string id, CreateRoleDto dto)
        {
            try
            {
                var role = await db.Role.FirstOrDefaultAsync(r => r.Id == id && r.Id != "admin");
                if (role is null) return ServiceResult<CreateRoleDto>.Fail("Không tìm thấy vai trò", 404);
                role.VaiTro = dto.VaiTro;
                db.Role.Update(role);
                var existingClaims = db.RoleClaims.Where(rc => rc.RoleId == id);
                db.RoleClaims.RemoveRange(existingClaims);
                if (dto.ClaimIds is not null && dto.ClaimIds.Count > 0)
                {
                    foreach (var claimId in dto.ClaimIds)
                    {
                        await db.RoleClaims.AddAsync(new RoleClaim()
                        {
                            RoleId = role.Id,
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

        public async Task<ServiceResult<List<ClaimDto>>> GetAllClaimsAsync()
        {
            try
            {
                var claims = await db.Claims
                    .Where(c => c.Id != 2)
                    .OrderBy(c => c.Id)
                    .Select(c => new ClaimDto
                    {
                        Id = c.Id,
                        TenQuyen = c.Quyen,
                        Category = c.Category
                    }).ToListAsync();
                return ServiceResult<List<ClaimDto>>.Ok(claims);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<ClaimDto>>.Fail($"Lỗi: {ex.Message}", 500);
            }

        }

        public async Task<ServiceResult<RoleAccResponseDto>> CapQuyen(RoleAccDto dto)
        {
            try
            {
                var user = await db.TaiKhoan.FindAsync(dto.TaiKhoanId);
                var role = await db.Role.FirstOrDefaultAsync(r => r.Id == dto.RoleId && r.Id != "admin");
                if (user is null || role is null) return ServiceResult<RoleAccResponseDto>.Fail("Role hoặc tài khoản không tìm thấy", 404);
                await db.TaiKhoanRoles.AddAsync(mapper.Map<TaiKhoanRole>(dto));
                await db.SaveChangesAsync();
                return ServiceResult<RoleAccResponseDto>.Ok(new RoleAccResponseDto()
                {
                    RoleId = dto.RoleId,
                    TaiKhoanId = dto.TaiKhoanId,
                    TenDangNhap = user.TenDangNhap,
                    VaiTro = role.VaiTro
                });
            }
            catch (Exception ex)
            {
                return ServiceResult<RoleAccResponseDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }
    }
}
