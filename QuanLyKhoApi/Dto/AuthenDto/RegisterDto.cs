using QuanLyKhoApi.Data;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto.AuthenDto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Id nhân viên là bắt buộc.")]
        public Guid IdNhanVien { get; set; }


        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [MaxLength(200, ErrorMessage = "Tên đăng nhập tối đa 200 ký tự.")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 ký tự.")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        [MaxLength(100, ErrorMessage = "Mật khẩu tối đa 100 ký tự.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
