using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class PhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuNhap { get; set; }
        [Required]
        public DateTime NgayNhap { get; set; } = DateTime.UtcNow;
        [Required]
        public Guid MaNV { get; set; }
        public decimal? GiaNhap { get; set; }
        [Required]
        public Guid MaHH { get; set; }
        [ForeignKey(nameof(MaHH))]
        public HangHoa HangHoa { get; set; }
        [Required]
        public int MaTrangThai { get; set; }
        public string? GhiChu { get; set; }
        public int? SoLuong { get; set; }

        [ForeignKey(nameof(MaTrangThai))]
        public TrangThaiPhieu TrangThaiPhieu { get; set; }

        [ForeignKey(nameof(MaNV))]
        public NhanVien NhanVien { get; set; }

        public ICollection<ChiTietNhap> ChiTietNhaps { get; set; } = new List<ChiTietNhap>();

    }
}
