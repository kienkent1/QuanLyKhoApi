using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.ThongKeDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class ThongKeService(AppDbContext db, IMapper mapper) : IThongKeServices
    {


        public async Task<ServiceResult<List<ThongKe>?>> GetThongKe(int year)
        {
            try
            {
                if (year == null || year < 1)
                {
                    return ServiceResult<List<ThongKe>?>.Fail("Năm không hợp lệ", 400);
                }
                var thongKe = await db.ThongKe.Where(tk => tk.Year == year).ToListAsync();
                if (thongKe is null || thongKe.Count == 0)
                {
                    return ServiceResult<List<ThongKe>?>.Fail("Không có dữ liệu thống kê cho năm này", 404);
                }
                return ServiceResult<List<ThongKe>?>.Ok(thongKe);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<ThongKe>?>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }
        public async Task<bool> CreateThongKe(int Year, int Month)
        {
            try
            {
                if (Year < 1 || Month < 1 || Month > 12)
                {
                    return false;
                }
                var isExist = await db.ThongKe.FirstOrDefaultAsync(tk => tk.Year == Year && tk.Month == Month);
                if (isExist is not null)
                {
                    return false;
                }
                isExist.Year = Year;
                isExist.Month = Month;
                isExist.SoPhieuluongNhap = 0;
                isExist.SoPhieuluongXuat = 0;
                isExist.TongGiaNhap = 0;
                isExist.TongGiaXuat = 0;
                isExist.UpdateAt = DateTime.UtcNow;
                db.ThongKe.Add(isExist);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateThongKe(UpdateThongKeDto dto)
        {
            try
            {
                var thongKe = await db.ThongKe.FirstOrDefaultAsync(tk => tk.Id == dto.Id);
                if (thongKe is null)
                {
                    return false;
                }
                if(dto.SoPhieuluongNhap > 0 && dto.SoPhieuluongNhap is not null)
                {
                    var temp = thongKe.SoPhieuluongNhap + dto.SoPhieuluongNhap.Value;
                    thongKe.SoPhieuluongNhap = temp;
                }
                if (dto.SoPhieuluongXuat > 0 && dto.SoPhieuluongXuat is not null)
                {
                    var temp = thongKe.SoPhieuluongXuat + dto.SoPhieuluongXuat.Value;
                    thongKe.SoPhieuluongXuat = temp;
                }
                if (dto.TongGiaNhap > 0 && dto.TongGiaNhap is not null)
                {
                    var temp = thongKe.TongGiaNhap + dto.TongGiaNhap.Value;
                    thongKe.TongGiaNhap = temp;
                }
                if (dto.TongGiaXuat > 0 && dto.TongGiaXuat is not null)
                {
                    var temp = thongKe.TongGiaXuat + dto.TongGiaXuat.Value;
                    thongKe.TongGiaXuat = temp;
                }

                thongKe.UpdateAt = DateTime.UtcNow;
                db.ThongKe.Update(thongKe);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
