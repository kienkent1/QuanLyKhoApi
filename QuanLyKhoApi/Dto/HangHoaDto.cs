using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class HangHoaDto
    {
        public Guid MaHH { get; set; }

        [MaxLength(200, ErrorMessage = "Model không được quá 200 ký tự")]
        public string Model { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? MoTa { get; set; }

        [MaxLength(30, ErrorMessage = "Đơn vị tính không được quá 30 ký tự")]
        public string DonViTinh { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID nhà cung cấp là bắt buộc")]
        public int NhaCungCapId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; }

        [Required(ErrorMessage = "ID loại là bắt buộc")]
        public int IdLoai { get; set; }
        public bool CanhBaoSoLuong { get; set; } = false;
        public string? ThongBaoSoLuong { get; set; }
    }
}