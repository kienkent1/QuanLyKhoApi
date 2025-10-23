using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.RoleClaimDto;

namespace QuanLyKhoApi.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<TaiKhoanRole, RoleAccDto>().ReverseMap();
        }
    }
}
