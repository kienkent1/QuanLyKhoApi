using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class ChiTietNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTietNhap { get; set; }

        [Required]
        public Guid MaCauHinh { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        public decimal DonGia { get; set; }


        [ForeignKey(nameof(MaCauHinh))]
        public CauHinh CauHinh { get; set; }


    }
}
