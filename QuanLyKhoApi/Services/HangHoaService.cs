using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Text;

namespace QuanLyKhoApi.Services
{
    public class HangHoaService(AppDbContext _context, IMapper mapper, GitHubImageService git, Ironbarcode barCode) : IHangHoaService
    {

        public async Task<ServiceResult<HangHoaDto>> CreateHangHoaAsync(HangHoaDto dto)
        {
            try
            {
                var loai = await _context.Loai.FindAsync(dto.IdLoai);
                if (loai is null)
                    return ServiceResult<HangHoaDto>.Fail("Loại hàng hóa không tồn tại", 404);

                var nhaCungCap = await _context.NhaCungCap.FindAsync(dto.NhaCungCapId);
                if (nhaCungCap is null)
                    return ServiceResult<HangHoaDto>.Fail("Nhà cung cấp không tồn tại", 404);

                if (string.IsNullOrEmpty(dto.MaHHShow))
                {
                    int CountHH = await _context.HangHoa.CountAsync();
                    dto.MaHHShow = $"HH{CountHH + 1}";
                }

                var IsHHExit = await _context.HangHoa.AnyAsync(h => h.MaHHShow == dto.MaHHShow);
                if (IsHHExit) return ServiceResult<HangHoaDto>.Fail("Mã hàng hóa đã tồn tại", 400);
                var hangHoa = mapper.Map<HangHoa>(dto);
                hangHoa.Deleted = false;

                _context.HangHoa.Add(hangHoa);
                await _context.SaveChangesAsync();


                return ServiceResult<HangHoaDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<HangHoaDto>.Fail($"Lỗi hệ thống khi tạo hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<DetailHangHoaDto>> FindByBarCode(IFormFile file)
        {
            try
            {
                var codeResult = await barCode.ReadBarcode(file);
                var originalId = Encoding.UTF8.GetString(Convert.FromBase64String(codeResult));

                if (originalId is null || string.IsNullOrEmpty(originalId))
                    return ServiceResult<DetailHangHoaDto>.Fail("Không đọc được mã vạch", 400);

                var result = await GetHangHoaByIdAsync(originalId);
                return result;
            }
            catch (Exception ex)
            {
                return ServiceResult<DetailHangHoaDto>.Fail($"Lỗi hệ thống khi quét mã: {ex.Message}", 500);

            }
        }

        public async Task<ServiceResult<string>> GenBarCode(string id)
        {
            try
            {
                var isHangHoa = await _context.HangHoa.AnyAsync(h => h.MaHH.ToString() == id && h.Deleted != true);
                if (!isHangHoa)
                    return ServiceResult<string>.Fail("Hàng hóa không tồn tại", 404);
                var barCodeUrl = await barCode.GeneratedBarcode(id);
                return ServiceResult<string>.Ok(barCodeUrl);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Fail($"Lỗi hệ thống khi tạo mã vạch: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<ListHangHoaDto>>>> GetAllHangHoaAsync(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var hangHoas = _context.HangHoa
                    .Include(h => h.loai)
                    .Include(h => h.NhaCungCap)
                    .Where(h => h.Deleted != true)
                    .Select(h => new ListHangHoaDto
                    {
                        Id = h.MaHH,
                        MaHHShow = h.MaHHShow,
                        Model = h.Model,
                        MoTa = h.MoTa,
                        DonViTinh = h.DonViTinh,
                        NhaCungCapId = h.NhaCungCapId,
                        TenNhaCungCap = h.NhaCungCap.TenNCC,
                        SoLuongTon = h.SoLuongTon,
                        IdLoai = h.IdLoai,
                        TenLoai = h.loai.TenLoai,
                        ThongBaoSoLuong = AddCanhBaoSoLuong(h.Model, h.SoLuongTon, h.DonViTinh),
                    })
                    .AsQueryable();
                if (!string.IsNullOrEmpty(query))
                {
                    hangHoas = hangHoas.Where(
                        h => h.Model.Contains(query) ||
                        h.TenLoai.Contains(query) ||
                        h.TenNhaCungCap.Contains(query) ||
                        h.MaHHShow == query ||
                        h.Id.ToString().Contains(query)
                    );
                }
                var result = await Pagination<ListHangHoaDto>.PaginationAsync(hangHoas, page, pageSize, sort);

                return ServiceResult<PaginatedResult<List<ListHangHoaDto>>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<ListHangHoaDto>>>.Fail($"Lỗi hệ thống khi lấy danh sách hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<DetailHangHoaDto>> GetHangHoaByIdAsync(string id)
        {
            try
            {
                var hangHoa = await _context.HangHoa
                    .Include(h => h.CauHinhs.Where(c => c.Deleted != true))
                    .ThenInclude(c => c.HinhAnhs)
                    .Include(h => h.NhaCungCap)
                    .Include(h => h.loai)
                    .FirstOrDefaultAsync(h => h.Deleted == false && h.MaHH.ToString() == id);

                if (hangHoa is null)
                {
                    return ServiceResult<DetailHangHoaDto>.Fail("Không tìm thấy hàng hóa", 404);
                }

                var hangHoaDto = new DetailHangHoaDto
                {
                    Id = hangHoa.MaHH,
                    MaHHShow = hangHoa.MaHHShow,
                    Model = hangHoa.Model,
                    MoTa = hangHoa.MoTa,
                    DonViTinh = hangHoa.DonViTinh,
                    NhaCungCapId = hangHoa.NhaCungCapId,
                    TenNhaCungCap = hangHoa.NhaCungCap.Deleted == true ? "Nhà cung cấp đã bị xóa" : hangHoa.NhaCungCap.TenNCC,
                    SoLuongTon = hangHoa.SoLuongTon,
                    IdLoai = hangHoa.IdLoai,
                    TenLoai = hangHoa.loai.Deleted == true ? "Loại đã bị xóa" : hangHoa.loai.TenLoai,
                    CauHinhs = hangHoa.CauHinhs.Select(c => new CauHinhDto
                    {
                        Id = c.Id,
                        MaHH = c.MaHH,
                        GiaBan = c.GiaBan,
                        SoLuongTon = c.SoLuongTon,
                        MoTa = c.MoTa,
                        MauSac = c.MauSac,
                        ColorCode = c.ColorCode,
                        Ram = c.Ram,
                        Rom = c.Rom,
                        SoLuongHidden = c.SoLuongHidden,
                        CanhBaoSoLuong = AddCanhBaoSoLuong(
                            CanhBaoMessage(hangHoa.Model, c.MauSac, c.Ram, c.Rom),
                            c.SoLuongTon, hangHoa.DonViTinh),
                        HinhAnh = c.HinhAnhs.Select(img => new HinhAnhDto
                        {
                            Id = img.Id,
                            Url = img.Url,
                            CreateAt = img.CreateAt,
                        }).OrderBy(img => img.CreateAt).ToList()
                    }).ToList()
                };

                return ServiceResult<DetailHangHoaDto>.Ok(hangHoaDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<DetailHangHoaDto>.Fail($"Lỗi hệ thống khi lấy thông tin hàng hóa: {ex.Message}", 500);
            }
        }
        private static string CanhBaoMessage(string model, string mau, string ram, string rom)
        {
            string result = model;
            if (mau is not null && !string.IsNullOrEmpty(mau)) result += ", Màu: " + mau;
            if (ram is not null && !string.IsNullOrEmpty(ram)) result += ", Ram: " + ram;
            if (rom is not null && !string.IsNullOrEmpty(rom)) result += ", Rom: " + rom;
            return result += ".";
        }

        public async Task<ServiceResult<HangHoaDto>> UpdateHangHoaAsync(Guid id, HangHoaDto dto)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(id);
                if (hangHoa == null)
                    return ServiceResult<HangHoaDto>.Fail("Không tìm thấy hàng hóa", 404);

                var loai = await _context.Loai.FindAsync(dto.IdLoai);
                if (loai == null)
                    return ServiceResult<HangHoaDto>.Fail("Loại hàng hóa không tồn tại", 404);

                var nhaCungCap = await _context.NhaCungCap.FindAsync(dto.NhaCungCapId);
                if (nhaCungCap == null)
                    return ServiceResult<HangHoaDto>.Fail("Nhà cung cấp không tồn tại", 404);

                hangHoa.Model = dto.Model;
                hangHoa.MoTa = dto.MoTa;
                hangHoa.DonViTinh = dto.DonViTinh != null ? dto.DonViTinh : "cái";
                hangHoa.NhaCungCapId = dto.NhaCungCapId;
                hangHoa.SoLuongTon = dto.SoLuongTon;
                hangHoa.IdLoai = dto.IdLoai;
                _context.Update(hangHoa);

                await _context.SaveChangesAsync();

                return ServiceResult<HangHoaDto>.Ok(dto, 200, "Cập nhật hàng hóa thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<HangHoaDto>.Fail($"Lỗi hệ thống khi cập nhật hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<bool>> DeleteHangHoaAsync(Guid id)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(id);
                if (hangHoa == null)
                    return ServiceResult<bool>.Fail("Không tìm thấy hàng hóa", 404);

                hangHoa.Deleted = true;
                hangHoa.DeletedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true, 200, "Xóa hàng hóa thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail($"Lỗi hệ thống khi xóa hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<CauHinhDto>> GetCauHinhById(Guid id)
        {
            try
            {
                var c = await _context.CauHinh
                    .Include(c => c.HinhAnhs)

                    .FirstOrDefaultAsync(c => c.Id == id && c.Deleted != true);
                if (c == null)
                {
                    return ServiceResult<CauHinhDto>.Fail("Cấu hình không tồn tại", 404);
                }
                var selectCauHinh = new CauHinhDto
                {
                    Id = c.Id,
                    MaHH = c.MaHH,
                    GiaBan = c.GiaBan,
                    SoLuongTon = c.SoLuongTon,
                    MoTa = c.MoTa,
                    MauSac = c.MauSac,
                    ColorCode = c.ColorCode,
                    Ram = c.Ram,
                    Rom = c.Rom,
                    SoLuongHidden = c.SoLuongHidden,
                    HinhAnh = c.HinhAnhs.Select(img => new HinhAnhDto
                    {
                        Url = img.Url,
                        CreateAt = img.CreateAt,
                    }).OrderBy(img => img.CreateAt).ToList()
                };

                return ServiceResult<CauHinhDto>.Ok(selectCauHinh);
            }
            catch (Exception ex)
            {
                return ServiceResult<CauHinhDto>.Fail($"Lỗi hệ thống khi lấy danh sách cấu hình: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<List<CreateCauHinhDto>>> CreateMultipleConfigsAsync(Guid maHH, List<CreateCauHinhDto> dto)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(maHH);
                if (hangHoa is null)
                    return ServiceResult<List<CreateCauHinhDto>>.Fail("Không tìm thấy hàng hóa", 404);


                using var transaction = await _context.Database.BeginTransactionAsync();
                foreach (var config in dto)
                {
                    config.MaHH = maHH;
                }

                var newConfigs = mapper.Map<List<CauHinh>>(dto);

                await _context.CauHinh.AddRangeAsync(newConfigs);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


                var resultDtos = mapper.Map<List<CreateCauHinhDto>>(newConfigs);
                return ServiceResult<List<CreateCauHinhDto>>.Ok(resultDtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CreateCauHinhDto>>.Fail($"Lỗi hệ thống khi tạo nhiều cấu hình: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<List<HinhAnhDto>>> AddHinhAnhCauHing(Guid id, IFormFile[] files)
        {
            try
            {
                var cauHinh = await _context.CauHinh.FindAsync(id);
                if (cauHinh is null)
                    return ServiceResult<List<HinhAnhDto>>.Fail("Không tìm thấy cấu hình hàng hóa", 404);
                var imgUrls = await git.UpdateimgList(files, "CauHinh");
                if (imgUrls is null) return ServiceResult<List<HinhAnhDto>>.Fail("Lỗi khi tải hình ảnh lên GitHub", 500);

                foreach (var img in imgUrls)
                {
                    var hinhAnh = new HinhAnhHH
                    {
                        Url = img.Url,
                        CreateAt = DateTime.UtcNow,
                    };
                    cauHinh.HinhAnhs.Add(hinhAnh);
                    _context.HinhAnhHH.Add(hinhAnh);
                }
                await _context.SaveChangesAsync();
                return ServiceResult<List<HinhAnhDto>>.Ok(cauHinh.HinhAnhs.Select(img => new HinhAnhDto
                {
                    Url = img.Url,
                    CreateAt = img.CreateAt,
                }).ToList());

            }
            catch (Exception ex)
            {
                return ServiceResult<List<HinhAnhDto>>.Fail($"Lỗi hệ thống khi thêm hình ảnh cấu hình: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<bool>> DeleteHinhAnhCauHinhAsync(Guid[] ids)
        {
            try
            {
                var hinhAnhList = await _context.HinhAnhHH.Where(img => ids.Contains(img.Id)).ToListAsync();
                if (hinhAnhList == null || !hinhAnhList.Any())
                    return ServiceResult<bool>.Fail("Không tìm thấy hình ảnh cấu hình", 404);

                foreach (var img in hinhAnhList)
                {
                    _context.HinhAnhHH.Remove(img);
                }

                await _context.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail($"Lỗi hệ thống khi xóa hình ảnh cấu hình: {ex.Message}", 500);
            }
        }
        private static string AddCanhBaoSoLuong(string ten, int soLuong, string? donVi)
        {
            if (soLuong < 10)
            {
                return $"Cảnh báo: Số lượng tồn của {ten} chỉ còn {soLuong} {donVi}. Vui lòng nhập thêm hàng!";
            }
            else
            {
                return null;
            }
        }

    }
}