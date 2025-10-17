using System.ComponentModel.DataAnnotations;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Dto
{
    public class UpdateNhanVienDto
    {

        [MaxLength(200, ErrorMessage = "Tên nhân viên không được vượt quá 200 ký tự")]
        public string? TenNhanVien { get; set; }

 
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? email { get; set; }



        [RegularExpression(@"^\d{9,13}$", ErrorMessage = "Số điện thoại phải có từ 9 đến 13 chữ số")]

        public string? sdt { get; set; }

        public string? diaChi { get; set; }

        public DateTime? ngaySinh { get; set; }

        public IFormFile? avatar { get; set; }
        public GIOITINH gioiTinh { get; set; }

        public string? chucVu { get; set; }

        public bool trangthai { get; set; } = true;
    }
}
