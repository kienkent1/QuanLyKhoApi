using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class ComfirmAccount
    {
        [Key]
        [ForeignKey(nameof(TaiKhoan))]
        public Guid IdTaiKhoan { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public TaiKhoan TaiKhoan { get; set; }
    }
}
