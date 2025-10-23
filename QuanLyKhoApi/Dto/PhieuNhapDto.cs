using System.ComponentModel.DataAnnotations;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Dto
{
    public class PhieuNhapDto
    {
        public int Id { get; set; }
        public DateTime NgayNhap { get; set; }
        public int MaTrangThai { get; set; }
        public string? TenHang { get; set; }
        public string TenTrangThai { get; set; }
        public decimal GiaNhap { get; set; }
        public string? GhiChu { get; set; }
        public string? Message { get; set; }
        public string? NhanVienTen { get; set; }

    }
    public class CreatePhieuNhapDto
    {
        public DateTime? NgayNhap { get; set; } = DateTime.UtcNow;
        public Guid? MaNV { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn bằng 0")]
        public decimal? GiaNhap { get; set; }
        [Required(ErrorMessage = "Hàng hóa là bắt buộc")]
        public Guid MaHH { get; set; }

        public int? MaTrangThai { get; set; } = (int)TRANGTHAI.Pending;
        public string? GhiChu { get; set; }
        public string? Message { get; set; }
        public List<ChiTietNhapDto>? ChiTietNhaps { get; set; }
    }

    public class ChiTietNhapDto
    {
        [Required(ErrorMessage = "Chi tiết hàng hóa là bắt buộc")]
        public Guid MaCauHinh { get; set; }


        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 1")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn bằng 0")]
        public decimal DonGia { get; set; }
    }

    public class DetailPhieuNhapDto
    {
        public int MaPhieuNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public Guid MaNV { get; set; }
        public decimal? GiaNhap { get; set; }
        public Guid MaHH { get; set; }
        public string? TenHH { get; set; }
        public int MaTrangThai { get; set; }
        public string? TenTrangThai { get; set; }
        public string? GhiChu { get; set; }
        public int? SoLuong { get; set; }
        public string? NhanVienTen { get; set; }
        public string? TrangThaiTen { get; set; }
        public List<DetailCTPhieuNhapDto>? ChiTietNhaps { get; set; }
    }

    public class DetailCTPhieuNhapDto
    {
        public int MaChiTietNhap { get; set; }
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
