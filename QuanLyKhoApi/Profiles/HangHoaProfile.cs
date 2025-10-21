using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class HangHoaProfile : Profile
    {
        public HangHoaProfile()
        {
            CreateMap<HangHoaDto, HangHoa>().ReverseMap();
            CreateMap<CauHinhDto, CauHinh>().ReverseMap();
            CreateMap<Loai, LoaiDto>();
            CreateMap<LoaiDto, Loai>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
            CreateMap<NhaCungCap, NhaCungCapDto>();
            CreateMap<NhaCungCapDto, NhaCungCap>()
                .ForMember(dest => dest.MaNCC, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
        }
    }
}