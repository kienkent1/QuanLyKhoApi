using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class NhaCungCapService(AppDbContext context, IMapper mapper) : INhaCungCapService
    {
        public async Task<ServiceResult<NhaCungCap?>> CreateNhaCungCapAsync(NhaCungCapDto dto)
        {
            try
            {
                var nhaCungCap = new NhaCungCap
                {
                    TenNCC = dto.TenNCC,
                    DienThoai = dto.DienThoai,
                    Email = dto.Email,
                    HinhAnh = dto.HinhAnh,
                    DiaChi = !string.IsNullOrEmpty(dto.DiaChi) 
                ? new Dictionary<string, object> { { "address", dto.DiaChi } }
                : null,
                    Deleted = false,
                    CreateAt = DateTime.UtcNow
                };

                context.NhaCungCap.Add(nhaCungCap);
                await context.SaveChangesAsync();

                return ServiceResult<NhaCungCap?>.Ok(nhaCungCap, 201, "Tạo nhà cung cấp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<NhaCungCap?>.Fail("Lỗi hệ thống khi tạo nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<List<NhaCungCap>>> GetAllNhaCungCapAsync()
        {
            try
            {
                var nhaCungCaps = await context.NhaCungCap
                    .Where(n => n.Deleted == false)
                    .OrderBy(n => n.TenNCC)
                    .ToListAsync();

                return ServiceResult<List<NhaCungCap>>.Ok(nhaCungCaps, 200, "Lấy danh sách nhà cung cấp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<NhaCungCap>>.Fail("Lỗi hệ thống khi lấy danh sách nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<NhaCungCap?>> GetNhaCungCapByIdAsync(int id)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap
                    .FirstOrDefaultAsync(n => n.MaNCC == id && n.Deleted == false);

                if (nhaCungCap == null)
                {
                    return ServiceResult<NhaCungCap?>.Fail("Không tìm thấy nhà cung cấp", 404);
                }

                return ServiceResult<NhaCungCap?>.Ok(nhaCungCap, 200, "Lấy thông tin nhà cung cấp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<NhaCungCap?>.Fail("Lỗi hệ thống khi lấy thông tin nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<bool>> UpdateNhaCungCapAsync(int id, NhaCungCapDto dto)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap.FindAsync(id);
                if (nhaCungCap is null)
                    return ServiceResult<bool>.Fail("Không tìm thấy nhà cung cấp", 404);

                mapper.Map(dto, nhaCungCap);
                context.NhaCungCap.Update(nhaCungCap);
                await context.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true, 200, "Cập nhật nhà cung cấp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail("Lỗi hệ thống khi cập nhật nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<bool>> DeleteNhaCungCapAsync(int id)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap.FindAsync(id);
                if (nhaCungCap is null)
                    return ServiceResult<bool>.Fail("Không tìm thấy nhà cung cấp", 404);

                nhaCungCap.Deleted = true;
                nhaCungCap.DeletedAt = DateTime.UtcNow;
                context.NhaCungCap.Update(nhaCungCap);
                await context.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true, 200, "Xóa nhà cung cấp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail("Lỗi hệ thống khi xóa nhà cung cấp", 500);
            }
        }
    }
}