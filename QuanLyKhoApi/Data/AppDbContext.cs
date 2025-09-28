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
        public DbSet<KhoHang> KhoHang { get; set; }
        public DbSet<Loai> Loai { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<PhieuNhap> PhieuNhap { get; set; }
        public DbSet<PhieuXuat> PhieuXuat { get;set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get;set; }
        public DbSet<TrangThaiPhieu> TrangThaiPhieu { get; set; }
    }
}
