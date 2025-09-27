using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class HinhAnhHH
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string MaHH { get; set; }
        [Required]
        public string Url { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(MaHH))]
        public HangHoa HangHoa { get; set; }
    }
}
