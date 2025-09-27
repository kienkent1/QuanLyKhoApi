using System.ComponentModel.DataAnnotations;
namespace QuanLyKhoApi.Data
{
    public class TaiKhoanAuth
    {
        [Key]
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
