using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Data
{
    public class Phieu_xuat
    {
        [Key]
        [Required]
        public int ma_px { get; set; }
        [Required]
        public DateTime ngay_xuat { get; set; }
        [Required]
        public int ma_nv { get; set; } 
        public decimal tong_tien { get; set; }
        [Required]
        public int ma_kho { get; set; } 
        [Required]
        public int MaTrangThai { get; set; } 
        public string ghi_chu { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}