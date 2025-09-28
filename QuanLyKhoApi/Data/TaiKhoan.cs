using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class TaiKhoan
    {
        [Key]
        [ForeignKey(nameof(NhanVien))]
        public Guid IdNhanVien { get; set; }
        [Required, MaxLength(200)]
        public string TenDangNhap { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
