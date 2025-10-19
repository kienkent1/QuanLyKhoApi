using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }
        public DbSet<ChiTietNhap> ChiTietNhap { get; set; }
        public DbSet<ChiTietXuat> ChiTietXuat { get; set; }
        public DbSet<HangHoa> HangHoa { get; set; }
        public DbSet<HinhAnhHH> HinhAnhHH { get; set; }
        public DbSet<Loai> Loai { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<PhieuNhap> PhieuNhap { get; set; }
        public DbSet<PhieuXuat> PhieuXuat { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<TrangThaiPhieu> TrangThaiPhieu { get; set; }
        public DbSet<TaiKhoanToken> TaiKhoanToken { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<TaiKhoanRole> TaiKhoanRoles { get; set; }
        public DbSet<ComfirmAccount> ComfirmAccounts { get; set; }
        public DbSet<CauHinh> CauHinh { get; set; }
        public DbSet<ThongKe> ThongKe { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Khai báo khóa chính cho bảng liên kết khóa chính sẽ là 1 cặp ví dụ roleid = admin, claimid = 1 (all claim),
            //sẽ không tồn tại 1 cặp như vạy trong db nx
            modelBuilder.Entity<RoleClaim>()
                .HasKey(rc => new { rc.RoleId, rc.ClaimId });
            modelBuilder.Entity<TaiKhoanRole>()
                .HasKey(tr => new { tr.TaiKhoanId, tr.RoleId });
            modelBuilder.Entity<NhanVien>()
                .Property(nv => nv.diaChi)
                .HasColumnType("jsonb")
                .HasDefaultValueSql("'{}'::jsonb");
            modelBuilder.Entity<NhaCungCap>()
                .Property(ncc => ncc.DiaChi)
                .HasColumnType("jsonb")
                .HasDefaultValueSql("'{}'::jsonb");

            //seeds data
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = "user", VaiTro = "User" },
                new Role { Id = "admin", VaiTro = "Admin" }
            );
            modelBuilder.Entity<Claims>().HasData(
                new Claims { Id = 1, Quyen = "User" },
                new Claims { Id = 2, Quyen = "Admin" },
                new Claims { Id = 3, Quyen = "ThemNhanVien", Category = "NhanVien" },
                new Claims { Id = 4, Quyen = "XoaNhanVien", Category = "NhanVien" },
                new Claims { Id = 5, Quyen = "SuaNhanVien", Category = "NhanVien" },
                new Claims { Id = 6, Quyen = "XemNhanVien", Category = "NhanVien" },
                new Claims { Id = 7, Quyen = "ThemNhaCungCap", Category = "NhaCungCap" },
                new Claims { Id = 8, Quyen = "XoaNhaCungCap", Category = "NhaCungCap" },
                new Claims { Id = 9, Quyen = "SuaNhaCungCap", Category = "NhaCungCap" },
                new Claims { Id = 10, Quyen = "XemNhaCungCap", Category = "NhaCungCap" },
                new Claims { Id = 11, Quyen = "ThemHangHoa", Category = "HangHoa" },
                new Claims { Id = 12, Quyen = "XoaHangHoa", Category = "HangHoa" },
                new Claims { Id = 13, Quyen = "SuaHangHoa", Category = "HangHoa" },
                new Claims { Id = 14, Quyen = "XemHangHoa", Category = "HangHoa" },
                new Claims { Id = 15, Quyen = "ThemLoai", Category = "Loai" },
                new Claims { Id = 16, Quyen = "XoaLoai", Category = "Loai" },
                new Claims { Id = 17, Quyen = "SuaLoai", Category = "Loai" },
                new Claims { Id = 18, Quyen = "XemLoai", Category = "Loai" },
                new Claims { Id = 19, Quyen = "XemThongKe", Category = "ThongKe" },
                new Claims { Id = 20, Quyen = "ThemRole", Category = "Role" },
                new Claims { Id = 21, Quyen = "XoaRole", Category = "Role" },
                new Claims { Id = 22, Quyen = "SuaRole", Category = "Role" },
                new Claims { Id = 23, Quyen = "XemRole", Category = "Role" },
                new Claims { Id = 24, Quyen = "ThemPhieuNhap", Category = "PhieuNhap" },
                new Claims { Id = 25, Quyen = "XemPhieuNhap", Category = "PhieuNhap" },
                new Claims { Id = 26, Quyen = "ThemPhieuXuat", Category = "PhieuXuat" },
                new Claims { Id = 27, Quyen = "XemPhieuXuat", Category = "PhieuXuat" },
                new Claims { Id = 28, Quyen = "ThemTaiKhoan", Category = "TaiKhoan" },
                new Claims { Id = 29, Quyen = "XoaTaiKhoan", Category = "TaiKhoan" },
                new Claims { Id = 30, Quyen = "SuaTaiKhoan", Category = "TaiKhoan" },
                new Claims { Id = 31, Quyen = "XemTaiKhoan", Category = "TaiKhoan" }
                );
            modelBuilder.Entity<RoleClaim>().HasData(
                new RoleClaim { RoleId = "user", ClaimId = 1 },
                new RoleClaim { RoleId = "admin", ClaimId = 2 }
                );
            modelBuilder.Entity<TrangThaiPhieu>().HasData(
                new TrangThaiPhieu { MaTrangThai = 1, TenTrangThai = "Đang xử lý" },
                new TrangThaiPhieu { MaTrangThai = 2, TenTrangThai = "Hoàn thành" },
                new TrangThaiPhieu { MaTrangThai = 3, TenTrangThai = "Hủy" }
                );

            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien
                {
                    IdNhanVien = Guid.Parse("49a522ed-edb3-44b6-abf7-e6b1962003cf"),
                    TenNhanVien = "Nguyễn Văn A",
                    email = "nguyenvana@gmail.com",
                    sdt = "0123456789",
                    gioiTinh = "Nam",
                    ngaySinh = new DateTime(1990, 1, 1),
                    trangthai = true,
                    CreatedAt = DateTime.UtcNow,
                    chucVu = "Admin"
                });
            modelBuilder.Entity<TaiKhoan>().HasData(
                new TaiKhoan
                {
                    IdNhanVien = Guid.Parse("49a522ed-edb3-44b6-abf7-e6b1962003cf"),
                    TenDangNhap = "adminA",
                    Password = "AQAAAAIAAYagAAAAEEms0ysPRm2n5vnXRawAsarpqN71JIBmAsB6o/LwNQElvYkETT9sR3eCUBaE9SpJtA==",//admin1234
                    CreatedAt = DateTime.UtcNow
                });

            modelBuilder.Entity<TaiKhoanRole>().HasData(
                new TaiKhoanRole
                {
                    TaiKhoanId = Guid.Parse("49a522ed-edb3-44b6-abf7-e6b1962003cf"),
                    RoleId = "admin"
                });
        }
    }
}
