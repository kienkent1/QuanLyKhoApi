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
        public  async Task<bool> RoleHasClaimAsync(Guid IdNhanVien, string claimType)
        {
            return await context.TaiKhoanRoles
            .Include(tr => tr.Role)
            .ThenInclude(r => r.RoleClaims)
            .Where(tr => tr.TaiKhoan.IdNhanVien == IdNhanVien)
            .SelectMany(tr => tr.Role.RoleClaims)
            .AnyAsync(rc => rc.Claim.Quyen == claimType);
        }
    }
}
