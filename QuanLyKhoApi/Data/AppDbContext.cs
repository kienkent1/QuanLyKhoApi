using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoApi.Data
{
    public class AppDbContext : DbContext
    {
      public  AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }
        public DbSet<ChiTietNhap> ChiTietNhap { get; set; }
        public DbSet<ChiTietXuat> ChiTietXuat { get; set; }
        public DbSet<HangHoa> HangHoa { get; set; }
        public DbSet<HinhAnhHH> HinhAnhHH { get; set; }
        public DbSet<Loai> Loai { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<PhieuNhap> PhieuNhap { get; set; }
        public DbSet<PhieuXuat> PhieuXuat { get;set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get;set; }
        public DbSet<TrangThaiPhieu> TrangThaiPhieu { get; set; }
        public DbSet<TaiKhoanToken> TaiKhoanToken { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<TaiKhoanRole> TaiKhoanRoles { get; set; }
        public DbSet<ComfirmAccount> ComfirmAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Khai báo khóa chính cho bảng liên kết khóa chính sẽ là 1 cặp ví dụ roleid = admin, claimid = 1 (all claim),
            //sẽ không tồn tại 1 cặp như vạy trong db nx
            modelBuilder.Entity<RoleClaim>()
                .HasKey(rc => new { rc.RoleId, rc.ClaimId });
            modelBuilder.Entity<TaiKhoanRole>()
                .HasKey(tr => new { tr.TaiKhoanId, tr.RoleId });
        }
    }
}
