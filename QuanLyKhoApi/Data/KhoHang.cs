using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class KhoHang
    {
        [Key]
        [Required]
       public int MaKho { get; set; }
        [Required, MaxLength(200)]
        public string TenKho { get; set; }
        [Required]
        public string DiaChi { get; set; }
       public string? MoTa { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
