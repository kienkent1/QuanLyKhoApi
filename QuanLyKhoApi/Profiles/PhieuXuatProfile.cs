using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class PhieuXuatProfile : Profile
    {
        public PhieuXuatProfile()
        {
            CreateMap<CreatePhieuXuatDto, PhieuXuat>();
            CreateMap<ChiTietXuatDto, ChiTietXuat>();
            CreateMap<PhieuXuat, DetailPhieuXuatDto>();
            CreateMap<ChiTietXuat, DetailCTPhieuXuatDto>();
        }
    }
}
