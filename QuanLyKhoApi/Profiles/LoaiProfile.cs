using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class LoaiProfile : Profile
    {
        public LoaiProfile()
        {
            CreateMap<LoaiDto, Loai>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

            CreateMap<Loai, LoaiDto>();
        }
    }
}