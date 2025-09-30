using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class ChangePassModel
    {
        [Required, MaxLength(200)]
        public string TenNhanVien { get; set; }
        [Required]
        public string email { get; set; }
        [Required, MaxLength(15)]
        public string sdt { get; set; }
        public string? diaChi { get; set; }
        public DateTime ngaySinh { get; set; }
        [Required]
        public string gioiTinh { get; set; }
        [Required]
        public string chucVu { get; set; }
        public bool trangthai { get; set; } = true;
        public string? UrlHinh { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
