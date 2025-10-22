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
    public class PhieuXuatService(AppDbContext db, IMapper mapper, IThongKeServices thongKe) : IPhieuXuat
    {
        public async Task<ServiceResult<CreatePhieuXuatDto>> CreatePhieuXuatAsync(CreatePhieuXuatDto dto)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                if (dto.ChiTietXuats == null || dto.ChiTietXuats.Count == 0)
                {
                    return ServiceResult<CreatePhieuXuatDto>.Fail("Chi tiết xuất không được để trống", 400);
                }

                // Kiểm tra hàng hóa tồn tại
                var isHHExist = await db.HangHoa.AnyAsync(h => h.MaHH == dto.MaHH);
                if (!isHHExist)
                {
                    return ServiceResult<CreatePhieuXuatDto>.Fail("Hàng hóa không tồn tại", 400);
                }

                // Tạo phiếu xuất
                var newPhieuXuat = await db.PhieuXuat.AddAsync(new PhieuXuat
                {
                    NgayXuat = DateTime.UtcNow,
                    MaHH = dto.MaHH ?? Guid.Empty,
                    MaNV = dto.MaNV ?? Guid.Empty,
                    MaTrangThai = dto.MaTrangThai ?? (int)TRANGTHAI.Pending,
                    GhiChu = dto.GhiChu,
                });
                await db.SaveChangesAsync();

                // Lấy danh sách cấu hình hợp lệ
                var validCauHinhIds = await db.CauHinh
                    .Where(c => c.MaHH == dto.MaHH)
                    .Select(c => c.Id)
                    .ToListAsync();

                var invalids = dto.ChiTietXuats
                    .Where(ct => !validCauHinhIds.Contains(ct.MaCauHinh))
                    .ToList();

                if (invalids.Any())
                {
                    dto.GhiChu += "\n" + string.Join("\n", invalids.Select(i =>
                        $"Chi tiết cấu hình {i.MaCauHinh} không tồn tại cho hàng hóa {dto.MaHH}"));

                    dto.ChiTietXuats = dto.ChiTietXuats
                        .Where(ct => validCauHinhIds.Contains(ct.MaCauHinh))
                        .ToList();
                }

                if (dto.ChiTietXuats.Count == 0)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<CreatePhieuXuatDto>.Fail("Không có chi tiết xuất hợp lệ để lưu.", 400);
                }

                newPhieuXuat.Entity.SoLuong = dto.ChiTietXuats.Sum(ct => ct.SoLuong);

                var chiTietXuats = mapper.Map<List<ChiTietXuat>>(dto.ChiTietXuats);

                decimal tongGiaXuat = 0;
                foreach (var ct in chiTietXuats)
                {
                    ct.MaPhieuXuat = newPhieuXuat.Entity.MaPhieuXuat;
                    tongGiaXuat += ct.DonGia * (ct.SoLuong ?? 0);
                }

                newPhieuXuat.Entity.GiaXuat = tongGiaXuat;
                db.PhieuXuat.Update(newPhieuXuat.Entity);
                await db.ChiTietXuat.AddRangeAsync(chiTietXuats);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return ServiceResult<CreatePhieuXuatDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<CreatePhieuXuatDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<PhieuXuatDto>>>> GetAllPhieuXuat(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var phieuXuats = db.PhieuXuat
                    .Include(px => px.ChiTietXuats)
                    .Include(px => px.HangHoa)
                    .Include(px => px.NhanVien)
                    .Include(px => px.TrangThaiPhieu)
                    .Select(px => new PhieuXuatDto
                    {
                        Id = px.MaPhieuXuat,
                        NgayXuat = px.NgayXuat,
                        MaTrangThai = px.MaTrangThai,
                        TenTrangThai = px.TrangThaiPhieu.TenTrangThai,
                        NhanVienTen = px.NhanVien.TenNhanVien,
                        GhiChu = px.GhiChu,
                        GiaXuat = px.GiaXuat,
                    })
                    .AsQueryable();

                var result = await Pagination<PhieuXuatDto>.PaginationAsync(phieuXuats, page, pageSize, sort);
                return ServiceResult<PaginatedResult<List<PhieuXuatDto>>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<PhieuXuatDto>>>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<DetailPhieuXuatDto>> GetPhieuXuatById(int maPhieuXuat)
        {
            try
            {
                var phieuXuat = db.PhieuXuat
                    .Include(px => px.HangHoa)
                    .Include(px => px.ChiTietXuats)
                    .ThenInclude(ct => ct.CauHinh)
                    .AsQueryable();

                var result = await phieuXuat.Select(p => new DetailPhieuXuatDto
                {
                    MaPhieuXuat = p.MaPhieuXuat,
                    NgayXuat = p.NgayXuat,
                    MaHH = p.MaHH,
                    TenHH = p.HangHoa.Model,
                    SoLuong = p.SoLuong,
                    GiaXuat = p.GiaXuat,
                    MaTrangThai = p.MaTrangThai,
                    TenTrangThai = p.TrangThaiPhieu.TenTrangThai,
                    GhiChu = p.GhiChu,
                    ChiTietXuats = p.ChiTietXuats.Select(ct => new DetailCTPhieuXuatDto
                    {
                        MaChiTietXuat = ct.MaChiTietXuat,
                        MaCauHinh = ct.MaCauHinh,
                        SoLuong = (int)ct.SoLuong,
                        DonGia = ct.DonGia,
                        MauSac = ct.CauHinh.MauSac,
                        Ram = ct.CauHinh.Ram,
                        Rom = ct.CauHinh.Rom,
                        ColorCode = ct.CauHinh.ColorCode,
                        TenPhienBan = ct.CauHinh.TenPhienBan
                    }).ToList()
                }).FirstOrDefaultAsync(p => p.MaPhieuXuat == maPhieuXuat);

                if (result == null)
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Không tìm thấy phiếu xuất", 404);

                return ServiceResult<DetailPhieuXuatDto>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<DetailPhieuXuatDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<DetailPhieuXuatDto>> ChangeStatus(int id, TRANGTHAI trangThai)
        {
            try
            {
                var phieuXuat = await db.PhieuXuat
                    .Include(px => px.HangHoa)
                    .Include(px => px.ChiTietXuats)
                    .ThenInclude(ct => ct.CauHinh)
                    .FirstOrDefaultAsync(px => px.MaPhieuXuat == id);

                if (phieuXuat == null)
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Không tìm thấy phiếu xuất", 404);

                return trangThai switch
                {
                    TRANGTHAI.Done => await ChangeStatusToFinish(phieuXuat),
                    TRANGTHAI.Cancel => await ChangeStatusToCancel(phieuXuat),
                    _ => ServiceResult<DetailPhieuXuatDto>.Fail("Trạng thái không hợp lệ", 400),
                };
            }
            catch (Exception ex)
            {
                return ServiceResult<DetailPhieuXuatDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        private async Task<ServiceResult<DetailPhieuXuatDto>> ChangeStatusToFinish(PhieuXuat phieuXuat)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var hangHoa = await db.HangHoa.FirstOrDefaultAsync(h => h.MaHH == phieuXuat.MaHH);
                if (hangHoa == null)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Không tìm thấy hàng hóa", 404);
                }

                // Lấy danh sách cấu hình liên quan trong 1 truy vấn
                var cauHinhIds = phieuXuat.ChiTietXuats.Select(ct => ct.MaCauHinh).ToList();
                var cauHinhs = await db.CauHinh.Where(ch => cauHinhIds.Contains(ch.Id)).ToListAsync();

                // Kiểm tra đủ cấu hình
                if (cauHinhs.Count != cauHinhIds.Count)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Một hoặc nhiều cấu hình không tồn tại", 404);
                }

                // Kiểm tra tồn kho tổng
                if (hangHoa.SoLuongTon < phieuXuat.SoLuong)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Không đủ hàng trong kho để xuất", 400);
                }

                // Kiểm tra từng cấu hình
                foreach (var ct in phieuXuat.ChiTietXuats)
                {
                    var cauHinh = cauHinhs.First(ch => ch.Id == ct.MaCauHinh);
                    if (cauHinh.SoLuongTon < ct.SoLuong)
                    {
                        await transaction.RollbackAsync();
                        return ServiceResult<DetailPhieuXuatDto>.Fail(
                            $"Cấu hình {cauHinh.Id} không đủ số lượng để xuất (hiện có {cauHinh.SoLuongTon}, cần {ct.SoLuong})", 400);
                    }
                }

                // Thực hiện trừ hàng hóa và cấu hình
                hangHoa.SoLuongTon -= (int)phieuXuat.SoLuong;
                foreach (var ct in phieuXuat.ChiTietXuats)
                {
                    var cauHinh = cauHinhs.First(ch => ch.Id == ct.MaCauHinh);
                    cauHinh.SoLuongTon -= (int)ct.SoLuong;
                }

                // Cập nhật trạng thái phiếu xuất
                phieuXuat.MaTrangThai = (int)TRANGTHAI.Done;
                await db.SaveChangesAsync();

                var resultDto = mapper.Map<DetailPhieuXuatDto>(phieuXuat);

                // 🔹 Cập nhật thống kê (Done = trừ)
                var thongKeOk = await thongKe.CreateOrUpdateThongKePhieuXuat(resultDto, TRANGTHAI.Done);
                if (!thongKeOk)
                {
                    Console.WriteLine($"[Cảnh báo] Cập nhật thống kê thất bại cho phiếu xuất #{phieuXuat.MaPhieuXuat}");
                }

                await transaction.CommitAsync();
                return ServiceResult<DetailPhieuXuatDto>.Ok(resultDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<DetailPhieuXuatDto>.Fail($"Lỗi khi hoàn tất phiếu xuất: {ex.Message}", 500);
            }
        }

        private async Task<ServiceResult<DetailPhieuXuatDto>> ChangeStatusToCancel(PhieuXuat phieuXuat)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var hangHoa = await db.HangHoa.FirstOrDefaultAsync(h => h.MaHH == phieuXuat.MaHH);
                if (hangHoa == null)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Không tìm thấy hàng hóa", 404);
                }

                // Lấy danh sách cấu hình
                var cauHinhIds = phieuXuat.ChiTietXuats.Select(ct => ct.MaCauHinh).ToList();
                var cauHinhs = await db.CauHinh.Where(ch => cauHinhIds.Contains(ch.Id)).ToListAsync();

                if (cauHinhs.Count != cauHinhIds.Count)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult<DetailPhieuXuatDto>.Fail("Một hoặc nhiều cấu hình không tồn tại", 404);
                }

                // Khi hủy xuất => cộng lại hàng hóa + cấu hình
                hangHoa.SoLuongTon += (int)phieuXuat.SoLuong;

                foreach (var ct in phieuXuat.ChiTietXuats)
                {
                    var cauHinh = cauHinhs.First(ch => ch.Id == ct.MaCauHinh);
                    cauHinh.SoLuongTon += (int)ct.SoLuong;
                }

                // Cập nhật trạng thái
                phieuXuat.MaTrangThai = (int)TRANGTHAI.Cancel;
                await db.SaveChangesAsync();

                var resultDto = mapper.Map<DetailPhieuXuatDto>(phieuXuat);

                // 🔹 Cập nhật thống kê (Cancel = cộng)
                var thongKeOk = await thongKe.CreateOrUpdateThongKePhieuXuat(resultDto, TRANGTHAI.Cancel);
                if (!thongKeOk)
                {
                    Console.WriteLine($"[Cảnh báo] Cập nhật thống kê thất bại cho phiếu xuất #{phieuXuat.MaPhieuXuat}");
                }

                await transaction.CommitAsync();
                return ServiceResult<DetailPhieuXuatDto>.Ok(resultDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<DetailPhieuXuatDto>.Fail($"Lỗi khi hủy phiếu xuất: {ex.Message}", 500);
            }
        }

    }
}
