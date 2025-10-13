using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class NhaCungCapService(AppDbContext context, IMapper mapper) : INhaCungCapService
    {
        public async Task<NhaCungCap?> CreateNhaCungCapAsync(NhaCungCapDto dto)
        {
            try
            {
                var nhaCungCap = mapper.Map<NhaCungCap>(dto);
                nhaCungCap.Deleted = false;
                nhaCungCap.CreateAt = DateTime.UtcNow;

                context.NhaCungCap.Add(nhaCungCap);
                await context.SaveChangesAsync();

                return nhaCungCap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<NhaCungCap>> GetAllNhaCungCapAsync()
        {
            return await context.NhaCungCap
                .Where(n => n.Deleted == false)
                .OrderBy(n => n.TenNCC)
                .ToListAsync();
        }

        public async Task<NhaCungCap?> GetNhaCungCapByIdAsync(int id)
        {
            return await context.NhaCungCap
                .FirstOrDefaultAsync(n => n.MaNCC == id && n.Deleted == false);
        }

        public async Task<bool> UpdateNhaCungCapAsync(int id, NhaCungCapDto dto)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap.FindAsync(id);
                if (nhaCungCap is null) return false;

                mapper.Map(dto, nhaCungCap);
                context.NhaCungCap.Update(nhaCungCap);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteNhaCungCapAsync(int id)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap.FindAsync(id);
                if (nhaCungCap is null) return false;

                nhaCungCap.Deleted = true;
                nhaCungCap.DeletedAt = DateTime.UtcNow;
                context.NhaCungCap.Update(nhaCungCap);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}