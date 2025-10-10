using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class NhanVienDto
    {
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string TenNhanVien { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
  
        [RegularExpression(@"^\d{9,13}$", ErrorMessage = "Số điện thoại phải có từ 9 đến 13 chữ số")]

        public string sdt { get; set; }

        public string? diaChi { get; set; }
        public IFormFile? Hinh { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime ngaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public string gioiTinh { get; set; }

        [Required(ErrorMessage = "Chức vụ là bắt buộc")]
        public string chucVu { get; set; }

        public bool trangthai { get; set; } = true;

    }
}
