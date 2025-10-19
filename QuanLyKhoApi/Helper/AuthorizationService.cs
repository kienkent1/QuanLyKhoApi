using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;

namespace QuanLyKhoApi.Helper
{
    public class AuthorizationService
    {
        private readonly AppDbContext context;

        public AuthorizationService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ServiceResult<bool>> RoleHasClaimAsync(string IdNhanVien, string claimType)
        {
            var isActive = await context.NhanVien.AnyAsync(nv => nv.IdNhanVien.ToString() == IdNhanVien && nv.trangthai == true);

            if (!isActive) return ServiceResult<bool>.Fail("Bạn không thể đăng nhập hãy liên hệ với admin", 401);

            var result = await context.TaiKhoanRoles
            .Include(tr => tr.Role)
            .ThenInclude(r => r.RoleClaims)
            .Where(tr => tr.TaiKhoan.IdNhanVien.ToString() == IdNhanVien && tr.Role.Deleted != true)
            .SelectMany(tr => tr.Role.RoleClaims)
            .AnyAsync(rc => claimType.Contains(rc.Claim.Quyen) || rc.Claim.Quyen == "Admin");
            if (result)
                return ServiceResult<bool>.Ok(result);
            else
                return ServiceResult<bool>.Fail("Bạn không có quyền truy cập chức năng này", 403);
        }
    }
}
