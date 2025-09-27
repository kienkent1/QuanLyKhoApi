using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class PhieuNhap
    {
        [Key]
        [Required]
        public int MaPhieuNhap { get; set; }
        [Required]
        public DateTime NgayNhap { get; set; } = DateTime.Now;
        [Required]
        public Guid MaNV { get; set; }
        [Required]
        public string MaNCC { get; set; }
        
        public int MaTrangThai { get; set; }
        public string? GhiChu { get; set; }

        [ForeignKey(nameof(MaTrangThai))]
        public TrangThaiPhieu TrangThaiPhieu { get; set; }

        [ForeignKey(nameof(MaNV))]
        public NhanVien NhanVien { get; set; }

        

    }
}
