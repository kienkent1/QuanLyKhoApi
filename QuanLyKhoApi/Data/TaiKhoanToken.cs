using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class TaiKhoanToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(500)]
        public string RefreshToken { get; set; }
        public DateTime ExpiryTime { get; set; }
     
        public Guid IdTaiKhoan { get; set; }

        [ForeignKey(nameof(IdTaiKhoan))]
        public TaiKhoan TaiKhoan { get; set; }
    }
}
