using AutoMapper;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;

namespace QuanLyKhoApi.Profiles
{
    public class PhieuNhapProfile : Profile
    {
        public PhieuNhapProfile()
        {
            CreateMap<ChiTietNhap, ChiTietNhapDto>().ReverseMap();
            CreateMap<PhieuNhap, DetailPhieuNhapDto>()
            .ForMember(dest => dest.TenHH,
                opt => opt.MapFrom(src => src.HangHoa != null ? src.HangHoa.Model : null))
            .ForMember(dest => dest.NhanVienTen,
                opt => opt.MapFrom(src => src.NhanVien != null ? src.NhanVien.TenNhanVien : null))
            .ForMember(dest => dest.TrangThaiTen,
                opt => opt.MapFrom(src => src.TrangThaiPhieu != null ? src.TrangThaiPhieu.TenTrangThai : null))
            .ForMember(dest => dest.TenTrangThai,
                opt => opt.MapFrom(src => src.TrangThaiPhieu != null ? src.TrangThaiPhieu.TenTrangThai : null))
            .ForMember(dest => dest.ChiTietNhaps,
                opt => opt.MapFrom(src => src.ChiTietNhaps));

            // Map danh sách chi tiết
            CreateMap<ChiTietNhap, DetailCTPhieuNhapDto>()
                .ForMember(dest => dest.MaCauHinh,
                    opt => opt.MapFrom(src => src.CauHinh != null
                        ? $"{src.CauHinh.MauSac} {src.CauHinh.Ram} {src.CauHinh.Rom}"
                        : null))
                .ForMember(dest => dest.MaCauHinh, opt => opt.MapFrom(src => src.MaCauHinh))
                .ForMember(dest => dest.SoLuong, opt => opt.MapFrom(src => src.SoLuong))
                .ForMember(dest => dest.DonGia, opt => opt.MapFrom(src => src.DonGia))
                .ReverseMap();
        }
    }
}
