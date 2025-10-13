using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class UpdateAvatarNV
    {
        [Required(ErrorMessage = "Không có hình được tải lên")]
        public IFormFile avatar { get; set; }
    }
}
