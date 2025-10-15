using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.ThongKeDto;

namespace QuanLyKhoApi.Profiles
{
    public class ThongKeProfile : Profile
    {
        public ThongKeProfile()
        {
            CreateMap<ThongKe, CreateThongKeDto>().ReverseMap();
        }
    }
}
