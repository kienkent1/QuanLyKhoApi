using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto.ThongKeDto
{
    public class UpdateThongKeDto
    {
        [Required]
        public int Id { get; set; }

        public int? SoPhieuluongNhap { get; set; }
        public int? SoPhieuluongXuat { get; set; }
        public decimal? TongGiaNhap { get; set; }
        public decimal? TongGiaXuat { get; set; }
        public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
