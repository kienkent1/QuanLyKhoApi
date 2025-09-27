using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class TrangThaiPhieu
    {
        [Key]
        [Required]
        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public string MoTa { get; set; }
    }
}
