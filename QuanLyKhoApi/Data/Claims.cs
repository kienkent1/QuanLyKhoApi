using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class Claims
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Quyen { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
    }
}
