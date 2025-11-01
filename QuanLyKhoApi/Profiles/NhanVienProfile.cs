using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;


namespace QuanLyKhoApi.Profiles
{
    public class NhanVienProfile : Profile
    {
        public NhanVienProfile()
        {
            CreateMap<NhanVien, NhanVienDto>().ReverseMap();
            CreateMap<ChangePassModel, NhanVien>().ReverseMap();
            CreateMap<DetailNhanVienDto, NhanVien>().ReverseMap();
        }
    }
}
