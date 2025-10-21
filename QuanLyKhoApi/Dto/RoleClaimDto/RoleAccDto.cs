using QuanLyKhoApi.Data;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto.RoleClaimDto
{
    public class RoleAccDto
    {
        [Required(ErrorMessage = "Mã tài khoản là bắt buộc")]
        public Guid TaiKhoanId { get; set; }
        [Required(ErrorMessage = "Mã vai trò là bắt buộc")]
        public string RoleId { get; set; }
    }

    public class RoleAccResponseDto
    {
        public Guid TaiKhoanId { get; set; }
        public string? TenDangNhap { get; set; }
        public string RoleId { get; set; }
        public string? VaiTro { get; set; }
    }
}
