using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class HangHoa
    {
        [Key]
        public Guid MaHH { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200) ]
        public string Model { get; set; }

        public string? MoTa { get; set; }

        [Required, MaxLength(30)]
        public string DonViTinh { get; set; }

         [Required]
        public int NhaCungCapId { get; set; }
        [ForeignKey(nameof(NhaCungCapId))]
        public NhaCungCap NhaCungCap { get; set; }

        [Required]
        public int SoLuongTon { get; set; } 

        public bool Deleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
       
        [Required]
        public int IdLoai { get; set; }

        [ForeignKey(nameof(IdLoai))]
        public Loai loai { get; set; }

        public ICollection<CauHinh> CauHinhs { get; set; } = new List<CauHinh>();

    }
}
