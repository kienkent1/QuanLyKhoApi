using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class NhaCungCapDto
    {
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [MaxLength(200, ErrorMessage = "Tên nhà cung cấp không được vượt quá 200 ký tự")]
        public string TenNCC { get; set; } = string.Empty;

        public string? DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        public string DienThoai { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        public string? HinhAnh { get; set; }
    }
}