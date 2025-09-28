using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class Role
    {
        [Key]
        [ForeignKey(nameof(TaiKhoan))]
        public Guid IdNhanVien { get; set; }
        [Required]
        public string VaiTro { get; set; }
        public string? Quyen { get; set; }

    }
}
