using QuanLyKhoApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Dto
{
    public class PhieuXuatDto
    {
        public int Id { get; set; }
        public DateTime NgayXuat { get; set; }
        public int MaTrangThai { get; set; }
        public string? TenHang { get; set; }
        public string? TenTrangThai { get; set; }
        public decimal? GiaXuat { get; set; }
        public string? GhiChu { get; set; }
        public string? NhanVienTen { get; set; }
    }

    public class CreatePhieuXuatDto
    {
        public DateTime? NgayXuat { get; set; } = DateTime.UtcNow;
        public Guid? MaNV { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Giá xuất phải lớn hơn bằng 0")]
        public decimal? GiaXuat { get; set; }

        [Required(ErrorMessage = "Hàng hóa là bắt buộc")]
        public Guid? MaHH { get; set; }

        public int? MaTrangThai { get; set; } = (int)TRANGTHAI.Pending;
        public string? GhiChu { get; set; }
        public List<ChiTietXuatDto>? ChiTietXuats { get; set; }
    }

    public class ChiTietXuatDto
    {
        [Required(ErrorMessage = "Chi tiết hàng hóa là bắt buộc")]
        public Guid MaCauHinh { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 1")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn bằng 0")]
        public decimal DonGia { get; set; }
    }

    public class DetailPhieuXuatDto
    {
        public int MaPhieuXuat { get; set; }
        public DateTime NgayXuat { get; set; }
        public Guid MaHH { get; set; }
        public string? TenHH { get; set; }
        public int MaTrangThai { get; set; }
        public string? TenTrangThai { get; set; }
        public string? GhiChu { get; set; }
        public decimal? GiaXuat { get; set; }
        public int? SoLuong { get; set; }
        public string? NhanVienTen { get; set; }
        public List<DetailCTPhieuXuatDto>? ChiTietXuats { get; set; }
    }

    public class DetailCTPhieuXuatDto
    {
        public int MaChiTietXuat { get; set; }
        public Guid MaCauHinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string? MauSac { get; set; }
        public string? Ram { get; set; }
        public string? Rom { get; set; }
        public string? ColorCode { get; set; }
        public string? TenPhienBan { get; set; }
    }
}
