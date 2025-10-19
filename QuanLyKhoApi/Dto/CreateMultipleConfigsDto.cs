using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoApi.Dto
{
    public class CreateMultipleConfigsDto
    {
        [Required]
        public Guid MaHH { get; set; }

        [Required]
        public List<CauHinhDto> Configs { get; set; } = new List<CauHinhDto>();
    }
}