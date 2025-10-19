using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuanLyKhoApi.Data
{
    public class CauHinh
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MaHH { get; set; }

        [ForeignKey(nameof(MaHH))]

        public HangHoa HangHoa { get; set; }

        [Required]
        public decimal GiaBan { get; set; }

        [Required]
        public int SoLuongTon { get; set; }

        public string? MoTa { get; set; }

        [MaxLength(50)]
        public string? MauSac { get; set; }

        [MaxLength(20)]
        public string? ColorCode { get; set; }

        public string? Ram { get; set; }

        public string? Rom { get; set; }

        public int? SoLuongHidden { get; set; } = 0;

        [JsonIgnore]
        public ICollection<HinhAnhHH> HinhAnhs { get; set; } = new List<HinhAnhHH>();

        public bool Deleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }
    }
}