using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class ChangePassworDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
