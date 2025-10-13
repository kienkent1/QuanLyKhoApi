using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class HangHoaService(AppDbContext context, IMapper mapper) : IHangHoaService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<HangHoa?> CreateHangHoaAsync(HangHoaDto dto)
        {
            try
            {
                var loai = await _context.Loai.FindAsync(dto.IdLoai);
                if (loai is null) return null;

                var nhaCungCap = await _context.NhaCungCap.FindAsync(dto.NhaCungCapId);
                if (nhaCungCap is null) return null;

                var hangHoa = _mapper.Map<HangHoa>(dto);
                hangHoa.MaHH = Guid.NewGuid();
                hangHoa.Deleted = false;

                _context.HangHoa.Add(hangHoa);
                await _context.SaveChangesAsync();

                return hangHoa;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CauHinh?> CreateCauHinhAsync(CauHinhDto dto)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(dto.MaHH);
                if (hangHoa is null) return null;

                var cauHinh = _mapper.Map<CauHinh>(dto);
                cauHinh.Id = Guid.NewGuid();
                cauHinh.Deleted = false;

                _context.CauHinh.Add(cauHinh);
                await _context.SaveChangesAsync();

                return cauHinh;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<HangHoaDto>> GetAllHangHoaAsync()
        {
            return await _context.HangHoa
                .Include(h => h.loai)
                .Include(h => h.CauHinhs.Where(c => c.Deleted == false))
                .Where(h => h.Deleted == false)
                .Select(h => _mapper.Map<HangHoaDto>(h)) 
                .ToListAsync();
        }

        public async Task<HangHoaDto?> GetHangHoaByIdAsync(Guid id)
        {
            var hangHoa = await _context.HangHoa
                .Include(h => h.loai)
                .Include(h => h.CauHinhs.Where(c => c.Deleted == false))
                .FirstOrDefaultAsync(h => h.MaHH == id && h.Deleted == false);

            if (hangHoa is null) return null;

            return _mapper.Map<HangHoaDto>(hangHoa);
        }

        public async Task<bool> UpdateHangHoaAsync(Guid id, HangHoaDto dto)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(id);
                if (hangHoa is null) return false;

                _mapper.Map(dto, hangHoa);
                _context.HangHoa.Update(hangHoa);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteHangHoaAsync(Guid id)
        {
            try
            {
                var hangHoa = await _context.HangHoa.FindAsync(id);
                if (hangHoa is null) return false;

                hangHoa.Deleted = true;
                hangHoa.DeletedAt = DateTime.UtcNow;
                _context.HangHoa.Update(hangHoa);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCauHinhAsync(Guid id, CauHinhDto dto)
        {
            try
            {
                var cauHinh = await _context.CauHinh.FindAsync(id);
                if (cauHinh is null) return false;

                _mapper.Map(dto, cauHinh);
                _context.CauHinh.Update(cauHinh);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCauHinhAsync(Guid id)
        {
            try
            {
                var cauHinh = await _context.CauHinh.FindAsync(id);
                if (cauHinh is null) return false;

                cauHinh.Deleted = true;
                cauHinh.DeletedAt = DateTime.UtcNow;
                _context.CauHinh.Update(cauHinh);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}