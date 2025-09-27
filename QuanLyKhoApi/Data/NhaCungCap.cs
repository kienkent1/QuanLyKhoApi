using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class NhaCungCap
    {
        [Key]
        [Required]
        public string ma_ncc { get; set; }
        [Required]
        public string ten_ncc { get; set; }
        public string dia_chi { get; set; }
        [Required]
        public string sdt { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public DateTime? Deleted_at { get; set; }
    }
}
