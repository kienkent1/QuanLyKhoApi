using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class NhaCungCapProfile : Profile
    {
        public NhaCungCapProfile()
        {
            CreateMap<NhaCungCapDto, NhaCungCap>()
                .ForMember(dest => dest.MaNCC, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreateAt, opt => opt.Ignore());
            CreateMap<NhaCungCap, NhaCungCapDto>();
        }
    }
}