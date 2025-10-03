using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class KhoHangProfile : Profile
    {
        public KhoHangProfile()
        {
            CreateMap<KhoHang, KhoHangDto>().ReverseMap();
        }
    }
}