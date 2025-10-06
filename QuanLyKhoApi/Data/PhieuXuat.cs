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
        public int MaKho { get; set; } 
        [Required]
        public int MaTrangThai { get; set; } 
        public string? GhiChu { get; set; }

        [ForeignKey(nameof(MaNV))]
        public NhanVien NhanVien { get; set; }

        [ForeignKey(nameof (MaKho))]
        public KhoHang KhoHang { get; set; }

        [ForeignKey(nameof(MaTrangThai))]
        public TrangThaiPhieu TrangThaiPhieu { get; set; }
    }
}