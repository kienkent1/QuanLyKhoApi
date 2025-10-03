using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class KhoHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKho { get; set; }
        [Required, MaxLength(200)]
        public string TenKho { get; set; }
        [Required]
        public string DiaChi { get; set; }
       public string? MoTa { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    }
}
