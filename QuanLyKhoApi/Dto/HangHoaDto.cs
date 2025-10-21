using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class HangHoaDto
    {
        [Required(ErrorMessage = "Tên hàng hóa là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Model không được quá 200 ký tự")]
        public string Model { get; set; }

        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? MoTa { get; set; }

        [MaxLength(30, ErrorMessage = "Đơn vị tính không được quá 30 ký tự")]
        public string? DonViTinh { get; set; }

        [Required(ErrorMessage = "ID nhà cung cấp là bắt buộc")]
        public int NhaCungCapId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; } = 0;

        [Required(ErrorMessage = "ID loại là bắt buộc")]
        public int IdLoai { get; set; }

        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }

    public class DetailHangHoaDto
    {
        public Guid Id { get; set; }
        [MaxLength(200, ErrorMessage = "Model không được quá 200 ký tự")]
        public string Model { get; set; }

        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? MoTa { get; set; }

        [MaxLength(30, ErrorMessage = "Đơn vị tính không được quá 30 ký tự")]
        public string? DonViTinh { get; set; }

        [Required(ErrorMessage = "ID nhà cung cấp là bắt buộc")]
        public int NhaCungCapId { get; set; }
        public string TenNhaCungCap { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; } = 0;

        [Required(ErrorMessage = "ID loại là bắt buộc")]
        public int IdLoai { get; set; }
        public string TenLoai { get; set; }
        public bool CanhBaoSoLuong { get; set; } = false;
        public string? ThongBaoSoLuong { get; set; }
        public List<CauHinhDto>? CauHinhs { get; set; }
    }

    public class ListHangHoaDto
    {
        public Guid Id { get; set; }
        [MaxLength(200, ErrorMessage = "Model không được quá 200 ký tự")]
        public string Model { get; set; }

        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? MoTa { get; set; }

        [MaxLength(30, ErrorMessage = "Đơn vị tính không được quá 30 ký tự")]
        public string? DonViTinh { get; set; }

        [Required(ErrorMessage = "ID nhà cung cấp là bắt buộc")]
        public int NhaCungCapId { get; set; }
        public string TenNhaCungCap { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; }

        [Required(ErrorMessage = "ID loại là bắt buộc")]
        public int IdLoai { get; set; }
        public string TenLoai { get; set; }
        public string? ThongBaoSoLuong { get; set; }
    }

    public class Getfile
    {
        public IFormFile file { get; set; }
    }
}