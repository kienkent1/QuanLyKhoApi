using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class HangHoaProfile : Profile
    {
        public HangHoaProfile()
        {
            CreateMap<HangHoaDto, HangHoa>()
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CauHinhs, opt => opt.Ignore())
                .ForMember(dest => dest.loai, opt => opt.Ignore())
                .ForMember(dest => dest.NhaCungCap, opt => opt.Ignore());
            CreateMap<CauHinhDto, CauHinh>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.SoLuongHidden, opt => opt.MapFrom(src => src.SoLuongHidden ?? 0))
                .ForMember(dest => dest.HangHoa, opt => opt.Ignore())
                .ForMember(dest => dest.HinhAnhs, opt => opt.Ignore());
            CreateMap<CauHinh, CauHinhDto>();
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