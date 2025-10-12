using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class HinhAnhHH
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid CauHinhId { get; set; }
        [Required]
        public string Url { get; set; }
        public int? Stt { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(CauHinhId))]
        public CauHinh CauHinh { get; set; }
    }
}
