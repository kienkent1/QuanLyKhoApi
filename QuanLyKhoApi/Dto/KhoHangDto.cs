using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class KhoHangDto
    {

        [Required(ErrorMessage = "Tên kho là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên kho không được vượt quá 200 ký tự")]
        public string TenKho { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string DiaChi { get; set; }

        public string? MoTa { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
    }
}