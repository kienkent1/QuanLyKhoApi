using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class PhieuNhapDto
    {
    }
    public class CreatePhieuNhapDto
    {
        public DateTime? NgayNhap { get; set; } = DateTime.UtcNow;
        public Guid? MaNV { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn bằng 0")]
        public decimal? GiaNhap { get; set; }
        [Required(ErrorMessage = "Hàng hóa là bắt buộc")]
        public Guid MaHH { get; set; }

        public int? MaTrangThai { get; set; } = 1;
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
}
