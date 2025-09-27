using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class PhieuNhap
    {
        [Key]
        [Required]
        public int ma_pn { get; set; }
        [Required]
        public DateTime ngay_nhap { get; set; }
        [Required]
        public int ma_nv { get; set; }
        [Required]
        public string ma_ncc { get; set; }
        [Required]
        public int ma_kho { get; set; }
        public decimal tong_tien { get; set; }
        public int ma_trang_thai { get; set; }
        public string ghi_chu { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
