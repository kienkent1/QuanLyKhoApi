using QuanLyKhoApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Dto
{
    public class PhieuXuatDto
    {
    }

    public class CreatePhieuXuatDto
    {
        public DateTime? NgayXuat { get; set; } = DateTime.UtcNow;
        public Guid? MaNV { get; set; }

        [Required]
        public int? MaTrangThai { get; set; } = (int)TRANGTHAI.Pending;
        public string? GhiChu { get; set; }
        public decimal? GiaXuat { get; set; }

        public Guid? MaHH { get; set; }
        public List<ChiTietXuatDto>? ChiTietXuats { get; set; }
    }

    public class ChiTietXuatDto
    {
        [Required]
        public Guid MaCauHinh { get; set; }

        public int? SoLuong { get; set; }

        public int? MaPhieuXuat { get; set; }


        [Required]
        public decimal DonGia { get; set; }
    }
}
