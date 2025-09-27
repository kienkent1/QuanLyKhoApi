using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class KhoHang
    {
        [Key]
        [Required]
       public int ma_sp { get; set; }
       public string ten_sp { get; set; }
       public string mo_ta { get; set; }
       public decimal gia_ban { get; set; }
       public string don_vi_tinh { get; set; }
       public int so_luong_ton { get; set; }
        [Required]
        public int ma_kho { get; set; }
        public string Loai { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
