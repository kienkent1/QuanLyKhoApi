using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class HangHoa
    {
        [Key]
        public string MaHH { get; set; }
        [Required, MaxLength(200) ]
        public string TenHH { get; set; }
        public string? MoTa { get; set; }
        [Required]
        public string DonViTinh { get; set; }
        [Required]
        public decimal GiaBan { get; set; }
        [Required]
        public int SoLuongTon { get; set; }
        [Required]
        public string MaKho {  get; set; }
        [Required]
        public int IdLoai { get; set; }

        [ForeignKey(nameof(IdLoai))]
        public Loai loai { get; set; }
        public ICollection<HinhAnhHH> HinhAnhs { get; set; } = new List<HinhAnhHH>();

    }
}
