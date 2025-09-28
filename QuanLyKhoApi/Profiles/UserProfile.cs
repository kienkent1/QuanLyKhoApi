using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Models;

namespace QuanLyKhoApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<TaiKhoan, LoginModel>();
        }
    }
}
