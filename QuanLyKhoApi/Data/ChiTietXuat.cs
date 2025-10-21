using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class ChiTietXuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuXuat { get; set; }

        [Required]
        public Guid MaCauHinh { get; set; }
        [Required]
        public int SoLuong { get; set; }

        public int MaChiTietXuat { get; set; }

        [ForeignKey(nameof(MaChiTietXuat))]
        public PhieuXuat PhieuXuat { get; set; }

        [Required]
        public decimal DonGia { get; set; }

        [ForeignKey(nameof(MaCauHinh))]
        public CauHinh CauHinh { get; set; }
    }
}
