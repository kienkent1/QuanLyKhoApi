using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Data
{
    public class NhanVien
    {
        [Key]
        public Guid IdNhanVien { get; set; }


        [Required]
        public string MaNV { get; set; }
        [Required, MaxLength(200)]
        public string TenNhanVien { get; set; }
        [Required]
        public string email { get; set; }
        [Required, MaxLength(15)]
        public string sdt { get; set; }
        public Dictionary<string, object>? diaChi { get; set; }
        public DateTime ngaySinh { get; set; }
        [Required]
        public string gioiTinh { get; set; }
        [Required]
        public string chucVu { get; set; }
        public bool trangthai { get; set; } = false;
        public string? UrlHinh { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

        public TaiKhoan? TaiKhoan { get; set; }
    }
}
