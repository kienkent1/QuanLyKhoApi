using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class HangHoaDto
    {
        public Guid MaHH { get; set; }

        [Required(ErrorMessage = "Model là bắt buộc")]
        [MaxLength(200, ErrorMessage = "Model không được quá 200 ký tự")]
        public string Model { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? MoTa { get; set; }

        [Required(ErrorMessage = "Đơn vị tính là bắt buộc")]
        [MaxLength(30, ErrorMessage = "Đơn vị tính không được quá 30 ký tự")]
        public string DonViTinh { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID nhà cung cấp là bắt buộc")]
        public int NhaCungCapId { get; set; }

        [Required(ErrorMessage = "Số lượng tồn là bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; }

        [Required(ErrorMessage = "ID loại là bắt buộc")]
        public int IdLoai { get; set; }
        public LoaiDto? Loai { get; set; }
        public ICollection<CauHinhDto>? CauHinhs { get; set; }
    }
}