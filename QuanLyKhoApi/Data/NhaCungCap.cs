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
        public string? HinhAnh { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
