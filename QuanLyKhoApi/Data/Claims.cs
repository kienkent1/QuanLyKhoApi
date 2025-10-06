using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class Claims
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Quyen { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
