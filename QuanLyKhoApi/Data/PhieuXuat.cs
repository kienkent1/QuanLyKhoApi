using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class PhieuXuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuXuat { get; set; }
        [Required]
        public DateTime NgayXuat { get; set; } = DateTime.UtcNow;
        [Required]
        public Guid MaNV{ get; set; } 

        [Required]
        public int MaTrangThai { get; set; } 
        public string? GhiChu { get; set; }
        public decimal? GiaXuat { get; set; }
        [Required]
        public Guid MaHH { get; set; }
        [ForeignKey(nameof(MaHH))]
        public HangHoa HangHoa { get; set; }

        [ForeignKey(nameof(MaNV))]
        public NhanVien NhanVien { get; set; }


        [ForeignKey(nameof(MaTrangThai))]
        public TrangThaiPhieu TrangThaiPhieu { get; set; }
        public ICollection<ChiTietXuat> ChiTietXuats { get; set; } = new List<ChiTietXuat>();
    }
}