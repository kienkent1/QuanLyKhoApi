using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Dto.ThongKeDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Services
{
    public class ThongKeService(AppDbContext db, IMapper mapper) : IThongKeServices
    {


        public async Task<ServiceResult<List<ThongKe>?>> GetThongKe(int year)
        {
            try
            {
                if (year < 1)
                    return ServiceResult<List<ThongKe>?>.Fail("Năm không hợp lệ", 400);

                var thongKe = await db.ThongKe
                    .Where(tk => tk.Year == year)
                    .OrderBy(tk => tk.Month)
                    .ThenBy(tk => tk.Day)
                    .ToListAsync();

                if (thongKe == null || thongKe.Count == 0)
                    return ServiceResult<List<ThongKe>?>.Fail("Không có dữ liệu thống kê cho năm này", 404);

                return ServiceResult<List<ThongKe>?>.Ok(thongKe);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<ThongKe>?>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        private async Task<bool> UpdateThongKe(DateTime date, DTO dto, PHIEU phieu, TRANGTHAI trangThai)
        {
            try
            {
                var thongKe = await db.ThongKe.FirstOrDefaultAsync(tk =>
                    tk.Year == date.Year &&
                    tk.Month == date.Month &&
                    tk.Day == date.Day);

                if (thongKe == null)
                {

                    thongKe = new ThongKe
                    {
                        Year = date.Year,
                        Month = date.Month,
                        Day = date.Day,
                        SoPhieuluongNhap = 0,
                        SoPhieuluongXuat = 0,
                        TongGiaNhap = 0,
                        TongGiaXuat = 0,
                        UpdateAt = DateTime.UtcNow
                    };
                    db.ThongKe.Add(thongKe);
                }

                // Phiếu nhập
                if (phieu == PHIEU.Nhap && dto.nhap is not null)
                {
                    if (trangThai == TRANGTHAI.Done)
                    {
                        thongKe.SoPhieuluongNhap += 1;
                        thongKe.TongGiaNhap += dto.nhap.GiaNhap ?? 0;
                    }
                    else if (trangThai == TRANGTHAI.Cancel)
                    {
                        thongKe.SoPhieuluongNhap = Math.Max(0, thongKe.SoPhieuluongNhap - 1);
                        thongKe.TongGiaNhap = Math.Max(0, thongKe.TongGiaNhap - (dto.nhap.GiaNhap ?? 0));
                    }
                }

                // Phiếu xuất
                else if (phieu == PHIEU.Xuat && dto.xuat is not null)
                {
                    if (trangThai == TRANGTHAI.Done)
                    {
                        thongKe.SoPhieuluongXuat += 1;
                        thongKe.TongGiaXuat += dto.xuat.GiaXuat ?? 0;
                    }
                    else if (trangThai == TRANGTHAI.Cancel)
                    {
                        thongKe.SoPhieuluongXuat = Math.Max(0, thongKe.SoPhieuluongXuat - 1);
                        thongKe.TongGiaXuat = Math.Max(0, thongKe.TongGiaXuat - (dto.xuat.GiaXuat ?? 0));
                    }
                }

                thongKe.UpdateAt = DateTime.UtcNow;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi UpdateThongKe: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateOrUpdateThongKePhieuNhap(DetailPhieuNhapDto phieuNhap, TRANGTHAI trangThai)
        {
            try
            {
                if (phieuNhap == null) return false;
                var dto = new DTO { nhap = phieuNhap };
                return await UpdateThongKe(phieuNhap.NgayNhap, dto, PHIEU.Nhap, trangThai);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi thống kê phiếu nhập: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateOrUpdateThongKePhieuXuat(DetailPhieuXuatDto phieuXuat, TRANGTHAI trangThai)
        {
            try
            {
                if (phieuXuat == null) return false;
                var dto = new DTO { xuat = phieuXuat };
                return await UpdateThongKe(phieuXuat.NgayXuat, dto, PHIEU.Xuat, trangThai);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi thống kê phiếu xuất: {ex.Message}");
                return false;
            }
        }
    }
}
