namespace QuanLyKhoApi.Dto.ThongKeDto
{
    public class CreateThongKeDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int SoPhieuluongNhap { get; set; }
        public int SoPhieuluongXuat { get; set; }
        public decimal TongGiaNhap { get; set; }
        public decimal TongGiaXuat { get; set; }
        public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
