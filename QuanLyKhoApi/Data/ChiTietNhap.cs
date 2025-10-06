using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class ChiTietNhap
    {
        [Key, ForeignKey(nameof(PhieuNhap))]
        public int MaPhieuNhap { get; set; }

        [Required]
        public string MaHH { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        public decimal DonGia { get; set; }


        [ForeignKey(nameof(MaHH))]
        public HangHoa HangHoa { get; set; }


    }
}
