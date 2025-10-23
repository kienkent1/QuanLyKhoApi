using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyKhoApi.Data
{
    public class TrangThaiPhieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTrangThai { get; set; }
        [Required, MaxLength(200)]
        public string TenTrangThai { get; set; }
        public string? MoTa { get; set; }
    }
}
