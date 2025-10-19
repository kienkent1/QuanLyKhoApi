using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class HangHoaService : IHangHoaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HangHoaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<HangHoa>> CreateHangHoaAsync(HangHoaDto dto)
        {
            try
            {
                var loai = await _context.Loai.FindAsync(dto.IdLoai);
                if (loai is null)
                    return ServiceResult<HangHoa>.Fail("Loại hàng hóa không tồn tại", 404);

                var nhaCungCap = await _context.NhaCungCap.FindAsync(dto.NhaCungCapId);
                if (nhaCungCap is null)
                    return ServiceResult<HangHoa>.Fail("Nhà cung cấp không tồn tại", 404);

                var newMaHH = Guid.NewGuid();
                Console.WriteLine($"Tạo MaHH mới: {newMaHH}");

                var hangHoa = new HangHoa
                {
                    MaHH = newMaHH,
                    Model = string.IsNullOrEmpty(dto.Model) ? "Chưa đặt tên" : dto.Model,
                    MoTa = dto.MoTa,
                    DonViTinh = string.IsNullOrEmpty(dto.DonViTinh) ? "Cái" : dto.DonViTinh,
                    NhaCungCapId = dto.NhaCungCapId,
                    SoLuongTon = dto.SoLuongTon,
                    IdLoai = dto.IdLoai,
                    Deleted = false
                };

                _context.HangHoa.Add(hangHoa);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Lưu thành công MaHH: {hangHoa.MaHH}");

                return ServiceResult<HangHoa>.Ok(hangHoa, 201, "Tạo hàng hóa thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo hàng hóa: {ex.Message}");
                return ServiceResult<HangHoa>.Fail($"Lỗi hệ thống khi tạo hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<IEnumerable<HangHoaDto>>> GetAllHangHoaAsync()
        {
            try
            {
                var hangHoas = await _context.HangHoa
                    .Where(h => h.Deleted == false)
                    .Select(h => new HangHoaDto
                    {
                        MaHH = h.MaHH,
                        Model = h.Model,
                        MoTa = h.MoTa,
                        DonViTinh = h.DonViTinh,
                        NhaCungCapId = h.NhaCungCapId,
                        SoLuongTon = h.SoLuongTon,
                        IdLoai = h.IdLoai
                    })
                    .ToListAsync();

                var hangHoasWithCanhBao = hangHoas.Select(h => AddCanhBaoSoLuong(h)).ToList();

                return ServiceResult<IEnumerable<HangHoaDto>>.Ok(hangHoasWithCanhBao, 200, "Lấy danh sách hàng hóa thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<HangHoaDto>>.Fail($"Lỗi hệ thống khi lấy danh sách hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<HangHoaDto>> GetHangHoaByIdAsync(Guid id)
        {
            try
            {
                Console.WriteLine($"Tìm hàng hóa với ID: {id}");

                var hangHoa = await _context.HangHoa
                    .Where(h => h.Deleted == false && h.MaHH == id)
                    .Select(h => new HangHoaDto
                    {
                        MaHH = h.MaHH,
                        Model = h.Model,
                        MoTa = h.MoTa,
                        DonViTinh = h.DonViTinh,
                        NhaCungCapId = h.NhaCungCapId,
                        SoLuongTon = h.SoLuongTon,
                        IdLoai = h.IdLoai
                    })
                    .FirstOrDefaultAsync();

                Console.WriteLine($"Kết quả tìm kiếm: {hangHoa != null}");

                if (hangHoa == null)
                {
                    Console.WriteLine($"Không tìm thấy hàng hóa với ID: {id}");
                    return ServiceResult<HangHoaDto>.Fail("Không tìm thấy hàng hóa", 404);
                }

                hangHoa = AddCanhBaoSoLuong(hangHoa);

                Console.WriteLine($"Tìm thấy hàng hóa: {hangHoa.Model}");
                return ServiceResult<HangHoaDto>.Ok(hangHoa, 200, "Lấy thông tin hàng hóa thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy hàng hóa theo ID: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return ServiceResult<HangHoaDto>.Fail($"Lỗi hệ thống khi lấy thông tin hàng hóa: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<HangHoa>> UpdateHangHoaAsync(Guid id, HangHoaDto dto)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(id);
                if (hangHoa == null)
                    return ServiceResult<HangHoa>.Fail("Không tìm thấy hàng hóa", 404);

                var loai = await _context.Loai.FindAsync(dto.IdLoai);
                if (loai == null)
                    return ServiceResult<HangHoa>.Fail("Loại hàng hóa không tồn tại", 404);

                var nhaCungCap = await _context.NhaCungCap.FindAsync(dto.NhaCungCapId);
                if (nhaCungCap == null)
                    return ServiceResult<HangHoa>.Fail("Nhà cung cấp không tồn tại", 404);

                hangHoa.Model = dto.Model;
                hangHoa.MoTa = dto.MoTa;
                hangHoa.DonViTinh = dto.DonViTinh;
                hangHoa.NhaCungCapId = dto.NhaCungCapId;
                hangHoa.SoLuongTon = dto.SoLuongTon;
                hangHoa.IdLoai = dto.IdLoai;

                await _context.SaveChangesAsync();

                return ServiceResult<HangHoa>.Ok(hangHoa, 200, "Cập nhật hàng hóa thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<HangHoa>.Fail($"Lỗi hệ thống khi cập nhật hàng hóa: {ex.Message}", 500);
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

        public async Task<ServiceResult<IEnumerable<CauHinhDto>>> GetCauHinhByHangHoaIdAsync(Guid hangHoaId)
        {
            try
            {
                var cauHinhs = await _context.CauHinh
                    .Where(c => c.MaHH == hangHoaId && c.Deleted == false)
                    .Select(c => new CauHinhDto
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
                        SoLuongHidden = c.SoLuongHidden
                    })
                    .ToListAsync();

                return ServiceResult<IEnumerable<CauHinhDto>>.Ok(cauHinhs, 200, "Lấy danh sách cấu hình thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<CauHinhDto>>.Fail($"Lỗi hệ thống khi lấy danh sách cấu hình: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<List<CauHinh>>> CreateMultipleConfigsAsync(CreateMultipleConfigsDto dto)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(dto.MaHH);
                if (hangHoa == null)
                    return ServiceResult<List<CauHinh>>.Fail("Không tìm thấy hàng hóa", 404);

                var configs = new List<CauHinh>();

                using var transaction = await _context.Database.BeginTransactionAsync();

                foreach (var configDto in dto.Configs)
                {
                    var config = new CauHinh
                    {
                        Id = Guid.NewGuid(),
                        MaHH = dto.MaHH,
                        GiaBan = configDto.GiaBan,
                        SoLuongTon = configDto.SoLuongTon,
                        MoTa = configDto.MoTa,
                        MauSac = configDto.MauSac,
                        ColorCode = configDto.ColorCode,
                        Ram = configDto.Ram,
                        Rom = configDto.Rom,
                        SoLuongHidden = configDto.SoLuongHidden ?? 0,
                        Deleted = false
                    };

                    _context.CauHinh.Add(config);
                    configs.Add(config);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ServiceResult<List<CauHinh>>.Ok(configs, 201, "Tạo nhiều cấu hình thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CauHinh>>.Fail($"Lỗi hệ thống khi tạo nhiều cấu hình: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<IEnumerable<HangHoaDto>>> GetHangHoaCanhBaoAsync()
        {
            try
            {
                var hangHoas = await _context.HangHoa
                    .Where(h => h.Deleted == false && h.SoLuongTon < 10)
                    .Select(h => new HangHoaDto
                    {
                        MaHH = h.MaHH,
                        Model = h.Model,
                        MoTa = h.MoTa,
                        DonViTinh = h.DonViTinh,
                        NhaCungCapId = h.NhaCungCapId,
                        SoLuongTon = h.SoLuongTon,
                        IdLoai = h.IdLoai,
                        CanhBaoSoLuong = true,
                        ThongBaoSoLuong = $"Cảnh báo: Số lượng tồn của {h.Model} chỉ còn {h.SoLuongTon} {h.DonViTinh}. Vui lòng nhập thêm hàng!"
                    })
                    .ToListAsync();

                return ServiceResult<IEnumerable<HangHoaDto>>.Ok(hangHoas, 200, $"Tìm thấy {hangHoas.Count} hàng hóa cần cảnh báo");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<HangHoaDto>>.Fail($"Lỗi hệ thống khi lấy hàng hóa cảnh báo: {ex.Message}", 500);
            }
        }
        private HangHoaDto AddCanhBaoSoLuong(HangHoaDto hangHoa)
        {
            if (hangHoa.SoLuongTon < 10)
            {
                hangHoa.CanhBaoSoLuong = true;
                hangHoa.ThongBaoSoLuong = $"Cảnh báo: Số lượng tồn của {hangHoa.Model} chỉ còn {hangHoa.SoLuongTon} {hangHoa.DonViTinh}. Vui lòng nhập thêm hàng!";
            }
            else
            {
                hangHoa.CanhBaoSoLuong = false;
                hangHoa.ThongBaoSoLuong = null;
            }

            return hangHoa;
        }
    }
}