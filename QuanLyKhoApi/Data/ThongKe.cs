using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class ThongKe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int SoPhieuluongNhap { get; set; }
        public int SoPhieuluongXuat { get; set; }
        public decimal TongGiaNhap { get; set; }
        public decimal TongGiaXuat { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
