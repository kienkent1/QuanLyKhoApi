using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class NhaCungCap
    {
        [Key]
        [Required]
        public string MaNCC { get; set; }
        [Required, MaxLength(200)]
        public string TenNCC { get; set; }
        public string? DiaChi { get; set; }
        [Required, MaxLength(15)]
        public string DienThoai { get; set; }
        public string? Email { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public DateTime? Deleted_at { get; set; }
    }
}
