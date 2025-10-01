using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class KhoHangService(AppDbContext db, IMapper mapper) : IKhoHangService
    {
        public async Task<List<KhoHangDto>> GetKhoHang()
        {
            var khoHang = await db.KhoHang.Select(kh => mapper.Map<KhoHangDto>(kh)).ToListAsync();
            return khoHang;
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
