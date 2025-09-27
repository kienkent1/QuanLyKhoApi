using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class ChiTietXuat
    {
        [Key]
        [Required]
        public int id_ctx { get; set; }
        [Required]
        public int ma_px { get; set; }
        [Required]
        public int ma_sp { get; set; }
        [Required]
        public int so_luong { get; set; }
        public decimal don_gia { get; set; }
        public decimal thanh_tien { get; set; }
    }
}
