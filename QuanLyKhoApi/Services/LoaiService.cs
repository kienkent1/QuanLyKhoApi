using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class LoaiService(AppDbContext db, IMapper mapper) : ILoaiService
    {
        public async Task<LoaiDto?> ThemLoaiAsync(LoaiDto loai)
        {
           var existingLoai = await db.Loai.FirstOrDefaultAsync(l => l.TenLoai == loai.TenLoai);
              if(existingLoai is not null)
              {
                return null;
            }
            var newLoai = mapper.Map<Loai>(loai);
            await db.Loai.AddAsync(newLoai);
            await db.SaveChangesAsync();
            return mapper.Map<LoaiDto>(newLoai);
        }
        public async Task<LoaiDto?> SuaLoai(int id, LoaiDto dto)
        {
            var Loai = await db.Loai.FirstOrDefaultAsync(l => l.Id == id);
            if (Loai == null)
            {
                return null;
            }
            var existingLoai = await db.Loai.FirstOrDefaultAsync(l => l.TenLoai == dto.TenLoai && l.Id != id);
            if (existingLoai is not null)
            {
                return null;
            }
            mapper.Map(dto, Loai);
            await db.SaveChangesAsync();
            return mapper.Map<LoaiDto>(Loai);
        }
        public async Task<List<LoaiDto>> GetLoai(string? query)
        {
            var loaiQuery = db.Loai.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                var keyword = query.Trim();
                var isId = int.TryParse(keyword, out var idValue);
                loaiQuery = loaiQuery.Where(l =>
                    EF.Functions.Like(l.TenLoai, $"%{keyword}%") ||
                    EF.Functions.Like(l.MoTa, $"%{keyword}%") ||
                    (isId && l.Id == idValue)
                );
            }
            var result = await loaiQuery.Select(l => mapper.Map<LoaiDto>(l)).ToListAsync();
            return result;
        }
        public async Task<LoaiDto?> GetLoaiById(int id)
        {
            var dto = await db.Loai.Where(l => l.Id == id)
                .Select(l => mapper.Map<LoaiDto>(l))
                .FirstOrDefaultAsync();
            return dto;
        }
        public async Task<bool> XoaLoaiTamAsync(int id)
        {
            var loai = await db.Loai.FirstOrDefaultAsync(l => l.Id == id);
            if (loai == null)
            {
                return false;
            }
            db.Loai.Remove(loai);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
