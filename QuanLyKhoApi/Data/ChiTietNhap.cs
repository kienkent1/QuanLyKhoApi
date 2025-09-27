using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class ChiTietNhap
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int ma_pn { get; set; }
        [Required]
        public int ma_sp { get; set; }
        [Required]
        public int so_luong { get; set; }
        public decimal don_gia { get; set; }
        public decimal thanh_tien { get; set; }
    }
}
