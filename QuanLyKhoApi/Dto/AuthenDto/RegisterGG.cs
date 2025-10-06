using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto.AuthenDto
{
    public class RegisterGG
    {
        [Required(ErrorMessage = "Id nhân viên là bắt buộc.")]
        public Guid IdNhanVien { get; set; }


        [MaxLength(200, ErrorMessage = "Tên đăng nhập tối đa 200 ký tự.")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 ký tự.")]
        public string? TenDangNhap { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
