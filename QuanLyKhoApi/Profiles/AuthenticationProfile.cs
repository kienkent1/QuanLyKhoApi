using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Dto.AuthenDto;

namespace QuanLyKhoApi.Profiles
{
    public class AuthenticationProfile :Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterDto, TaiKhoan>().ReverseMap();
            CreateMap<NhanVienDto, NhanVien>().ReverseMap();
            CreateMap<RegisterGG, TaiKhoan>().ReverseMap();
        }
    }
}
