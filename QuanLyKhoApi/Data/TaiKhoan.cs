using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class TaiKhoan
    {
        [Key]
        [ForeignKey(nameof(NhanVien))]
        public Guid IdNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }
        [Required, MaxLength(200)]
        public string TenDangNhap { get; set; }
        public string? Password { get; set; }


        public string? GoogleId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<TaiKhoanRole> TaiKhoanRoles { get; set; } = new List<TaiKhoanRole>();
        public ICollection<TaiKhoanToken> TaiKhoanTokens { get; set; } = new List<TaiKhoanToken>();
        public ComfirmAccount? ComfirmAccount { get; set; }
    }
}
