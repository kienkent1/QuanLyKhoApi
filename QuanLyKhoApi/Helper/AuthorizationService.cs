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
         .Where(tr => tr.TaiKhoanId == IdNhanVien)
         .AnyAsync(tr => tr.Role.Claims.Any(rc => rc.Quyen == claimType));
        }
    }
}
