using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class TaiKhoanAuth
    {
        [Key]
        [ForeignKey(nameof(TaiKhoan))]
        public Guid MaNhanVien { get; set; }

        public string? Password { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? GoogleId { get; set; }

    }
}
