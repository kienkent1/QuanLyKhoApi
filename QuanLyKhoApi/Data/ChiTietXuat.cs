using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class ChiTietXuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTietXuat { get; set; }


        [Required]
        public Guid MaCauHinh { get; set; }

        public int? SoLuong { get; set; }

        [Required]
        public int MaPhieuXuat { get; set; }


        [ForeignKey(nameof(MaPhieuXuat))]
        public PhieuXuat PhieuXuat { get; set; }

        [Required]
        public decimal DonGia { get; set; }

        [ForeignKey(nameof(MaCauHinh))]
        public CauHinh CauHinh { get; set; }
    }
}
