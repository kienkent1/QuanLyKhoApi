using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using System.Text.Json;

namespace QuanLyKhoApi.Profiles
{
    public class NhaCungCapProfile : Profile
    {
        public NhaCungCapProfile()
        {
            CreateMap<NhaCungCapDto, NhaCungCap>()
                .ForMember(dest => dest.DiaChi, opt => opt.MapFrom(src => DeserializeDiaChi(src.DiaChi)))
                .ReverseMap();


            CreateMap<NhaCungCapUpdateDto, NhaCungCap>()
                 .ForMember(dest => dest.DiaChi, opt => opt.MapFrom(src => DeserializeDiaChi(src.DiaChi)))
                 .ReverseMap();
        }
        private static Dictionary<string, object>? DeserializeDiaChi(string? diaChi)
        {
            return diaChi == null ? null : JsonSerializer.Deserialize<Dictionary<string, object>>(diaChi);
        }
    }
}