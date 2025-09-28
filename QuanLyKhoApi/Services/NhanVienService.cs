using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class NhanVienService(AppDbContext db, IMapper mapper) : INhanVienService
    {
        public async Task<List<NhanVien>> GetNhanVien()
        {
            var nhanVien = await  db.NhanVien.Select(nv => mapper.Map<NhanVien>(nv)).ToListAsync();
            return nhanVien;
        }

        public async Task< NhanVienDto?> ThemNhanVienAsync(NhanVienDto nhanVien)
        {
            var NewNhanVien = mapper.Map<NhanVien>(nhanVien);
            await db.NhanVien.AddAsync(NewNhanVien);
            await db.SaveChangesAsync();
            return mapper.Map<NhanVienDto>(NewNhanVien);
        }
    
    }
}
