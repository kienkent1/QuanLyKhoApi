using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class Role
    {
        [Key]
        public string Id { get; set; }
   
        public Guid IdTaiKhoan { get; set; }
        [Required]
        public string VaiTro { get; set; }
        public string? Quyen { get; set; }

        [ForeignKey(nameof(IdTaiKhoan))]
        public TaiKhoan TaiKhoan { get; set; }

    }
}
