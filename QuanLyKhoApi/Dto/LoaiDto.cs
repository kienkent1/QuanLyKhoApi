using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class LoaiDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Tên loại không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên loại không được vượt quá 100 ký tự")]
        public string TenLoai { get; set; }
        public string? MoTa { get; set; }
        public IFormFile? HinhAnh { get; set; }
        public string? HinhAnhReturn { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateLoaiDto
    {
        [Required(ErrorMessage = "Tên loại không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên loại không được vượt quá 100 ký tự")]
        public string TenLoai { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public IFormFile? NewHinhAnh { get; set; }
    }
}