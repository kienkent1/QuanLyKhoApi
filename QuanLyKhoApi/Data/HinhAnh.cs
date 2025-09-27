using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class HinhAnh
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public int ma_sp { get; set; }
        [Required]
        public string url { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
