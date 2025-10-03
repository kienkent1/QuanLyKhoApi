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
        public string? Password { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? GoogleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Role> roles { get; set; } = new List<Role>();
    }
}
