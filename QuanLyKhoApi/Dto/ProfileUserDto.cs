using System.ComponentModel.DataAnnotations;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Dto
{
    public class ProfileUserDto
    {

        [Required, MaxLength(200)]
        public string TenNhanVien { get; set; }

        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string UserName { get; set; }

        [Required]
        public string email { get; set; }

        [Required, MaxLength(15)]
        [RegularExpression(@"^\d{9,13}$", ErrorMessage = "Số điện thoại phải có từ 9 đến 13 chữ số")]
        public string sdt { get; set; }

        public Dictionary<string, object>? diaChi { get; set; }

        public string? UrlHinh { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime ngaySinh { get; set; }
        public string? ChucVu { get; set; }

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public GIOITINH gioiTinh { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
