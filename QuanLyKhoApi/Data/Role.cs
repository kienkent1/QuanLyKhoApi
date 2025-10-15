using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
   
        public string VaiTro { get; set; }
        public bool Deleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
        public ICollection<TaiKhoanRole> TaiKhoanRoles { get; set; } = new List<TaiKhoanRole>();

    }
}
