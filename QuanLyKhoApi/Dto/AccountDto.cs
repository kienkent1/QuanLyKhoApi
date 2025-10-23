using QuanLyKhoApi.Data;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class AccountDto
    {
        public Guid IdNhanVien { get; set; }
        public string TenDangNhap { get; set; }
        public string TenNhanVien { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public bool TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
