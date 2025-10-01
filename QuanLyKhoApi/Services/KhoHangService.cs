using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class KhoHangService(AppDbContext db, IMapper mapper) : IKhoHangService
    {
        public async Task<List<KhoHangDto>> GetKhoHang(string? query)
        {
            var khoQuery = db.KhoHang.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                var keyword = query.Trim();
                var isId = int.TryParse(keyword, out var maKhoValue);
                khoQuery = khoQuery.Where(k =>
                    EF.Functions.Like(k.TenKho, $"%{keyword}%") ||
                    EF.Functions.Like(k.DiaChi, $"%{keyword}%") ||
                    (isId && k.MaKho == maKhoValue)
                );
            }
            var result = await khoQuery.Select(k => mapper.Map<KhoHangDto>(k)).ToListAsync();

            return result;

        }
        public async Task<KhoHangDto?> GetKhoHangById(int maKho)
        {
            var dto = await db.KhoHang.Where(k => k.MaKho == maKho)
                .Select(k => mapper.Map<KhoHangDto>(k))
                .FirstOrDefaultAsync();
            return dto;

        }

        public async Task<KhoHangDto?> ThemKhoHangAsync(KhoHangDto khoHang)
        {
            var newKhoHang = await db.KhoHang.FirstOrDefaultAsync(a => a.TenKho == khoHang.TenKho) ;
            if(newKhoHang is not null)
            {
                return null;
            }
            var newHang = mapper.Map<KhoHang>(khoHang);
            await db.KhoHang.AddAsync(newHang);
            await db.SaveChangesAsync();
            return khoHang;
        }

        public async Task<bool> XoaKhoHangTamAsync(int maKho)
        {
            var kho = await db.KhoHang.FirstOrDefaultAsync(k => k.MaKho == maKho);
            if (kho == null)
            {
                return false;
            }
            db.KhoHang.Remove(kho);
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<KhoHangDto?> SuaKhoHang(int maKho, KhoHangDto dto)
        {
            var kho = await db.KhoHang.FirstOrDefaultAsync(k => k.MaKho == maKho);
            if (kho == null)
            {
                return null;
            }
            mapper.Map(dto, kho);
            await db.SaveChangesAsync();
            return mapper.Map<KhoHangDto>(kho);
        }
    }
}
