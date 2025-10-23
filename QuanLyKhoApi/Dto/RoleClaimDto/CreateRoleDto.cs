using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto.RoleClaimDto
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Tên role là bắt buộc")]
        [MaxLength(30, ErrorMessage = "Tên role chỉ từ 1 đến 30 ký tự")]
        [RegularExpression(@"^[\p{L}0-9 ]+$",
        ErrorMessage = "Không được chứa ký tự đặc biệt.")]
        public string VaiTro { get; set; }

        public List<int>? ClaimIds { get; set; } = new List<int>();
    }

    public class ListRoleDto
    {
        public string Id { get; set; }
        public string VaiTro { get; set; }

        public List<ClaimDto>? Claims { get; set; } = new List<ClaimDto>();
    }
    public class ClaimDto
    {
        public int Id { get; set; }
        public string TenQuyen { get; set; }
        public string? Category { get; set; }
    }
}
