using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Models;

namespace QuanLyKhoApi.Profiles
{
    public class NhanVienProfile : Profile
    {
        public NhanVienProfile() {
            CreateMap<NhanVien, NhanVienDto>().ReverseMap();
        }
    }
}
