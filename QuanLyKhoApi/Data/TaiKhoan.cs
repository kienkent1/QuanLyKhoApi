namespace QuanLyKhoApi.Data
{
    public class TaiKhoan
    {
       
        public Guid IdNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }
        public string TenDangNhap { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
