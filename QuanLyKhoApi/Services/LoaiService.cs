using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class LoaiService(AppDbContext db, IMapper mapper) : ILoaiService
    {
        public async Task<ServiceResult<LoaiDto>> ThemLoaiAsync(LoaiDto loai)
        {
            try
            {
                var existingLoai = await db.Loai.FirstOrDefaultAsync(l => l.TenLoai == loai.TenLoai);
                if (existingLoai is not null)
                {
                    return ServiceResult<LoaiDto>.Fail("Tên loại đã tồn tại", 400);
                }

                var newLoai = mapper.Map<Loai>(loai);
                await db.Loai.AddAsync(newLoai);
                await db.SaveChangesAsync();

                var result = mapper.Map<LoaiDto>(newLoai);
                return ServiceResult<LoaiDto>.Ok(result, 201, "Thêm loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<LoaiDto>.Fail("Lỗi hệ thống khi thêm loại", 500);
            }
        }

        public async Task<ServiceResult<LoaiDto>> SuaLoai(int id, LoaiDto dto)
        {
            try
            {
                var loai = await db.Loai.FirstOrDefaultAsync(l => l.Id == id);
                if (loai == null)
                {
                    return ServiceResult<LoaiDto>.Fail("Không tìm thấy loại", 404);
                }

                var existingLoai = await db.Loai.FirstOrDefaultAsync(l => l.TenLoai == dto.TenLoai && l.Id != id);
                if (existingLoai is not null)
                {
                    return ServiceResult<LoaiDto>.Fail("Tên loại đã tồn tại", 400);
                }

                mapper.Map(dto, loai);
                await db.SaveChangesAsync();

                var result = mapper.Map<LoaiDto>(loai);
                return ServiceResult<LoaiDto>.Ok(result, 200, "Cập nhật loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<LoaiDto>.Fail("Lỗi hệ thống khi cập nhật loại", 500);
            }
        }

        public async Task<ServiceResult<List<LoaiDto>>> GetLoai(string? query)
        {
            try
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
                return ServiceResult<List<LoaiDto>>.Ok(result, 200, "Lấy danh sách loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<LoaiDto>>.Fail("Lỗi hệ thống khi lấy danh sách loại", 500);
            }
        }

        public async Task<ServiceResult<LoaiDto>> GetLoaiById(int id)
        {
            try
            {
                var dto = await db.Loai.Where(l => l.Id == id)
                    .Select(l => mapper.Map<LoaiDto>(l))
                    .FirstOrDefaultAsync();

                if (dto == null)
                {
                    return ServiceResult<LoaiDto>.Fail("Không tìm thấy loại", 404);
                }

                return ServiceResult<LoaiDto>.Ok(dto, 200, "Lấy thông tin loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<LoaiDto>.Fail("Lỗi hệ thống khi lấy thông tin loại", 500);
            }
        }

        public async Task<ServiceResult<bool>> XoaLoaiTamAsync(int id)
        {
            try
            {
                var loai = await db.Loai.FirstOrDefaultAsync(l => l.Id == id);
                if (loai == null)
                {
                    return ServiceResult<bool>.Fail("Không tìm thấy loại", 404);
                }

                loai.Deleted = true;
                loai.DeletedAt = DateTime.UtcNow;
                db.Loai.Update(loai);
                await db.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true, 200, "Xóa loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail("Lỗi hệ thống khi xóa loại", 500);
            }
        }
    }
}