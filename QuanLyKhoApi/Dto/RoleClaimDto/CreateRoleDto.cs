using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto.RoleClaimDto
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Tên role là bắt buộc")]
        [MaxLength(30, ErrorMessage = "Tên role chỉ từ 1 đến 30 ký tự")]
        public string VaiTro { get; set; }

        public List<int>? ClaimIds { get; set; } = new List<int>();
    }
}
