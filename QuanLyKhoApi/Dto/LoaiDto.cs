using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class LoaiDto
    {
        [Required(ErrorMessage = "Tên loại không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên loại không được vượt quá 100 ký tự")]
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public IFormFile? HinhAnh { get; set; }
        public DateTime? CreatAt { get; set; } = DateTime.Now;
    }
}
