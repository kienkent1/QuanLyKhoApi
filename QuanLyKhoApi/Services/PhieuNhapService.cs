using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Collections.Generic;
using static QuanLyKhoApi.Helper.BaseEnum;

namespace QuanLyKhoApi.Services
{
    public class PhieuNhapService(AppDbContext db, IMapper mapper, IThongKeServices thongKe) : IPhieuNhap
    {
        public async Task<ServiceResult<CreatePhieuNhapDto>> CreatePhieuNhapAsync(CreatePhieuNhapDto dto)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                if (dto.ChiTietNhaps == null || dto.ChiTietNhaps.Count == 0)
                {
                    return ServiceResult<CreatePhieuNhapDto>.Fail("Chi tiết nhập không được để trống", 400);
                }

                // Kiểm tra hàng hóa
                var isHHExist = await db.HangHoa.AnyAsync(h => h.MaHH == dto.MaHH);
                if (!isHHExist)
                {
                    return ServiceResult<CreatePhieuNhapDto>.Fail("Hàng hóa không tồn tại", 400);
                }



                // Tạo phiếu nhập
                var newPhieuNhap = await db.PhieuNhap.AddAsync(new PhieuNhap
                {
                    NgayNhap = DateTime.UtcNow,
                    MaHH = dto.MaHH,
                    MaNV = dto.MaNV ?? Guid.Empty,
                    MaTrangThai = dto.MaTrangThai ?? 1,
                    GhiChu = dto.GhiChu,
                });
                await db.SaveChangesAsync();

                // Lấy danh sách cấu hình hợp lệ
                var validCauHinhIds = await db.CauHinh
                    .Where(c => c.MaHH == dto.MaHH)
                    .Select(c => c.Id)
                    .ToListAsync();

                var invalids = dto.ChiTietNhaps
                    .Where(ct => !validCauHinhIds.Contains(ct.MaCauHinh))
                    .ToList();

                if (invalids.Any())
                {
                    dto.Message = string.Join("\n", invalids.Select(i =>
                        $"Chi tiết hàng {i.MaCauHinh} không tồn tại cho hàng hóa {dto.MaHH}"));

                    dto.ChiTietNhaps = dto.ChiTietNhaps
                        .Where(ct => validCauHinhIds.Contains(ct.MaCauHinh))
                        .ToList();
                }

                // Nếu không còn chi tiết hợp lệ
                if (dto.ChiTietNhaps.Count == 0)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<CreatePhieuNhapDto>.Fail("Không có chi tiết nhập hợp lệ để lưu.", 400);
                }


                newPhieuNhap.Entity.SoLuong = dto.ChiTietNhaps.Sum(ct => ct.SoLuong);


                var ChiTietPhieuNhaps = mapper.Map<List<ChiTietNhap>>(dto.ChiTietNhaps);

                decimal TongGiaNhap = 0;
                //Gán mã phiếu nhập cho chi tiết
                foreach (var ct in ChiTietPhieuNhaps)
                {
                    ct.MaPhieuNhap = newPhieuNhap.Entity.MaPhieuNhap;
                    TongGiaNhap += ct.DonGia * ct.SoLuong;
                }
                newPhieuNhap.Entity.GiaNhap = TongGiaNhap;
                db.PhieuNhap.Update(newPhieuNhap.Entity);
                await db.ChiTietNhap.AddRangeAsync(ChiTietPhieuNhaps);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return ServiceResult<CreatePhieuNhapDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<CreatePhieuNhapDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<PhieuNhapDto>>>> GetAllPhieuNhap(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var phieuNhaps = db.PhieuNhap
                    .Include(pn => pn.ChiTietNhaps)
                    .Include(pn => pn.HangHoa)
                    .Include(pn => pn.NhanVien)
                    .Include(pn => pn.TrangThaiPhieu)
                    .Select(p => new PhieuNhapDto
                    {
                        Id = p.MaPhieuNhap,
                        NgayNhap = p.NgayNhap,
                        MaTrangThai = p.MaTrangThai,
                        TenTrangThai = p.TrangThaiPhieu.TenTrangThai,
                        NhanVienTen = p.NhanVien.TenNhanVien,
                        GhiChu = p.GhiChu,
                    })
                    .AsQueryable();


                var result = await Pagination<PhieuNhapDto>.PaginationAsync(phieuNhaps, page, pageSize, sort);
                return ServiceResult<PaginatedResult<List<PhieuNhapDto>>>.Ok(result);
            }
            catch (Exception ex)
            {

                return ServiceResult<PaginatedResult<List<PhieuNhapDto>>>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<DetailPhieuNhapDto>> GetPhieuNhapById(int maPhieuNhap)
        {
            try
            {
                var phieuNhap = db.PhieuNhap
                    .Include(p => p.HangHoa)
                    .Include(p => p.ChiTietNhaps)
                    .ThenInclude(ct => ct.CauHinh)
                    .AsQueryable();

                var result = await phieuNhap.Select(p => new DetailPhieuNhapDto
                {
                    MaPhieuNhap = p.MaPhieuNhap,
                    NgayNhap = p.NgayNhap,
                    MaHH = p.MaHH,
                    TenHH = p.HangHoa.Model,
                    SoLuong = p.SoLuong,
                    GiaNhap = p.GiaNhap,
                    MaTrangThai = p.MaTrangThai,
                    TenTrangThai = p.TrangThaiPhieu.TenTrangThai,
                    GhiChu = p.GhiChu,
                    ChiTietNhaps = p.ChiTietNhaps.Select(ct => new DetailCTPhieuNhapDto
                    {
                        MaChiTietNhap = ct.MaChiTietNhap,
                        MaCauHinh = ct.MaCauHinh,
                        SoLuong = ct.SoLuong,
                        DonGia = ct.DonGia,
                        MauSac = ct.CauHinh.MauSac,
                        Ram = ct.CauHinh.Ram,
                        Rom = ct.CauHinh.Rom,
                        ColorCode = ct.CauHinh.ColorCode,
                        TenPhienBan = ct.CauHinh.TenPhienBan

                    }).ToList()
                }).FirstOrDefaultAsync(p => p.MaPhieuNhap == maPhieuNhap);
                if (result == null)
                {
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Không tìm thấy phiếu nhập", 404);
                }
                return ServiceResult<DetailPhieuNhapDto>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<DetailPhieuNhapDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<DetailPhieuNhapDto>> ChangeStatus(int id, TRANGTHAI trangThai)
        {
            try
            {
                var phieuNhap = await db.PhieuNhap
                    .Include(p => p.HangHoa)
                    .Include(p => p.ChiTietNhaps)
                    .ThenInclude(ct => ct.CauHinh)
                    .FirstOrDefaultAsync(p => p.MaPhieuNhap == id);

                if (phieuNhap == null)
                {
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Không tìm thấy phiếu nhập", 404);
                }

                if (trangThai == TRANGTHAI.Done)
                {
                    return await ChangeStatusTofinish(phieuNhap);
                }
                else if (trangThai == TRANGTHAI.Cancel)
                {
                    return await ChangeStatusToCancel(phieuNhap);
                }
                else
                {
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Trạng thái không hợp lệ", 400);
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<DetailPhieuNhapDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        private async Task<ServiceResult<DetailPhieuNhapDto>> ChangeStatusTofinish(PhieuNhap phieuNhap)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var hangHoa = await db.HangHoa.FirstOrDefaultAsync(hh => hh.MaHH == phieuNhap.MaHH);
                if (hangHoa == null)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Không tìm thấy hàng hóa", 404);
                }

                // Lấy danh sách cấu hình liên quan trong 1 query
                var cauHinhIds = phieuNhap.ChiTietNhaps.Select(ct => ct.MaCauHinh).ToList();
                var cauHinhs = await db.CauHinh.Where(ch => cauHinhIds.Contains(ch.Id)).ToListAsync();

                // Kiểm tra đầy đủ cấu hình
                if (cauHinhs.Count != cauHinhIds.Count)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Một hoặc nhiều cấu hình không tồn tại", 404);
                }

                // Cộng tổng số lượng hàng hóa
                hangHoa.SoLuongTon += (int)phieuNhap.SoLuong;

                // Cộng từng cấu hình tồn kho
                foreach (var ct in phieuNhap.ChiTietNhaps)
                {
                    var cauHinh = cauHinhs.First(ch => ch.Id == ct.MaCauHinh);
                    cauHinh.SoLuongTon += ct.SoLuong;
                }

                phieuNhap.MaTrangThai = (int)TRANGTHAI.Done;

                await db.SaveChangesAsync();

                // Gọi thống kê — không rollback nếu lỗi
                var resultDto = mapper.Map<DetailPhieuNhapDto>(phieuNhap);
                var thongKeOk = await thongKe.CreateOrUpdateThongKePhieuNhap(resultDto, TRANGTHAI.Done);
                if (!thongKeOk)
                {
                    Console.WriteLine("[Cảnh báo] Cập nhật thống kê thất bại cho phiếu nhập #" + phieuNhap.MaPhieuNhap);
                }

                await transaction.CommitAsync();
                return ServiceResult<DetailPhieuNhapDto>.Ok(resultDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<DetailPhieuNhapDto>.Fail($"Lỗi khi hoàn tất phiếu nhập: {ex.Message}", 500);
            }
        }

        private async Task<ServiceResult<DetailPhieuNhapDto>> ChangeStatusToCancel(PhieuNhap phieuNhap)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var hangHoa = await db.HangHoa.FirstOrDefaultAsync(hh => hh.MaHH == phieuNhap.MaHH);
                if (hangHoa == null)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Không tìm thấy hàng hóa", 404);
                }

                var cauHinhIds = phieuNhap.ChiTietNhaps.Select(ct => ct.MaCauHinh).ToList();
                var cauHinhs = await db.CauHinh.Where(ch => cauHinhIds.Contains(ch.Id)).ToListAsync();

                if (cauHinhs.Count != cauHinhIds.Count)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Một hoặc nhiều cấu hình không tồn tại", 404);
                }

                // Kiểm tra đủ hàng để hủy
                if (hangHoa.SoLuongTon < phieuNhap.SoLuong)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuNhapDto>.Fail("Kho không đủ hàng để hủy phiếu nhập", 400);
                }

                foreach (var ct in phieuNhap.ChiTietNhaps)
                {
                    var cauHinh = cauHinhs.First(ch => ch.Id == ct.MaCauHinh);
                    if (cauHinh.SoLuongTon < ct.SoLuong)
                    {
                        await transaction.RollbackAsync();
                        return ServiceResult<DetailPhieuNhapDto>.Fail(
                            $"Cấu hình {cauHinh.Id} không đủ số lượng để hủy (hiện có {cauHinh.SoLuongTon}, cần {ct.SoLuong})", 400);
                    }
                }

                // Trừ tồn kho
                hangHoa.SoLuongTon -= (int)phieuNhap.SoLuong;
                foreach (var ct in phieuNhap.ChiTietNhaps)
                {
                    var cauHinh = cauHinhs.First(ch => ch.Id == ct.MaCauHinh);
                    cauHinh.SoLuongTon -= ct.SoLuong;
                }

                phieuNhap.MaTrangThai = (int)TRANGTHAI.Cancel;
                await db.SaveChangesAsync();

                var resultDto = mapper.Map<DetailPhieuNhapDto>(phieuNhap);
                var thongKeOk = await thongKe.CreateOrUpdateThongKePhieuNhap(resultDto, TRANGTHAI.Cancel);
                if (!thongKeOk)
                {
                    Console.WriteLine("[Cảnh báo] Cập nhật thống kê thất bại cho phiếu nhập #" + phieuNhap.MaPhieuNhap);
                }

                await transaction.CommitAsync();
                return ServiceResult<DetailPhieuNhapDto>.Ok(resultDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<DetailPhieuNhapDto>.Fail($"Lỗi khi hủy phiếu nhập: {ex.Message}", 500);
            }
        }
    }
}
