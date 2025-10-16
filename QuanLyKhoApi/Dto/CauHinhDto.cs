using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class CauHinhDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Mã hàng hóa là bắt buộc")]
        public Guid MaHH { get; set; }

        [Required(ErrorMessage = "Giá bán là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
        public decimal GiaBan { get; set; }

        [Required(ErrorMessage = "Số lượng tồn là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; }

        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? MoTa { get; set; }

        [MaxLength(50, ErrorMessage = "Màu sắc không được quá 50 ký tự")]
        public string? MauSac { get; set; }

        [MaxLength(20, ErrorMessage = "Mã màu không được quá 20 ký tự")]
        public string? ColorCode { get; set; }

        [MaxLength(20, ErrorMessage = "RAM không được quá 20 ký tự")]
        public string? Ram { get; set; }

        [MaxLength(20, ErrorMessage = "ROM không được quá 20 ký tự")]
        public string? Rom { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng ẩn phải lớn hơn hoặc bằng 0")]
        public int? SoLuongHidden { get; set; } = 0;
    }
}