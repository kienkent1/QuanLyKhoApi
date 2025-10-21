using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class PhieuNhapService(AppDbContext db) : IPhieuNhap
    {
        public async Task<ServiceResult<CreatePhieuNhapDto>> CreatePhieuNhapAsync(CreatePhieuNhapDto dto)
        {
            try
            {
                var isHHExist = await db.HangHoa.AnyAsync(h => h.MaHH == dto.MaHH);
                if (isHHExist == false)
                {
                    return ServiceResult<CreatePhieuNhapDto>.Fail("Hàng hóa không tồn tại", 400);
                }
                if (dto.ChiTietNhaps is not null && dto.ChiTietNhaps.Count > 0)
                {
                    foreach (var ct in dto.ChiTietNhaps!)
                    {
                        var isCauHinhExist = await db.CauHinh.AnyAsync(c => c.Id == ct.MaCauHinh && c.MaHH == dto.MaHH);
                        if (isCauHinhExist == false)
                        {
                            dto.Message += $"Chi tiết cấu hình hàng hóa với mã cấu hình {ct.MaCauHinh} không tồn tại cho hàng hóa {dto.MaHH}\n";
                            dto.ChiTietNhaps.Remove(ct);
                        }
                    }
                }

                return ServiceResult<CreatePhieuNhapDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<CreatePhieuNhapDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }
    }
}
