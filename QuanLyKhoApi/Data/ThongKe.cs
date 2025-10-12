using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class ThongKe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaXuat { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
