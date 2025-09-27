using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Data
{
    public class NhanVien
    {
        [Key]
        public Guid IdNhanVien   { get; set; }
        [Required]
        public string TenNhanVien { get; set; }
        [Required]
        public string email { get; set; }
        public string sdt { get; set; }
        public string diaChi { get; set; }
        public DateTime ngaySinh { get; set; }
        public bool gioiTinh { get; set; }
        public string chucVu { get; set; }
        public bool trangthai { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;



    }
}
