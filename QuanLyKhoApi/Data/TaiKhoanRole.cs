using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoApi.Data
{
    public class TaiKhoanRole
    {
        public Guid TaiKhoanId { get; set; }
        public TaiKhoan TaiKhoan { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
