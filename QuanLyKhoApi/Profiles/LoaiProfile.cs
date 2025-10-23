using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class LoaiProfile : Profile
    {
        public LoaiProfile()
        {
            CreateMap<LoaiDto, Loai>().ReverseMap();
            CreateMap<UpdateLoaiDto, Loai>().ReverseMap();
        }
    }
}