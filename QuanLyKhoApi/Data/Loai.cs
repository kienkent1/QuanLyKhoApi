using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Data
{
    public class Loai
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string TenLoai { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
    }
}
