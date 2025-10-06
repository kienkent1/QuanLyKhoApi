using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
   
        public string VaiTro { get; set; }

        public ICollection<Claims> Claims { get; set; } = new List<Claims>();
        public ICollection<TaiKhoanRole> TaiKhoanRoles { get; set; } = new List<TaiKhoanRole>();

    }
}
