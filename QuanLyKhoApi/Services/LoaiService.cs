using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class LoaiService(AppDbContext db, IMapper mapper, GitHubImageService git) : ILoaiService
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
                if (loai.HinhAnh is not null)
                {

                    var imagePath = await git.UpdateOneImg(loai.HinhAnh, "Loai");
                    newLoai.HinhAnh = imagePath.Url;
                }

                await db.Loai.AddAsync(newLoai);
                await db.SaveChangesAsync();

                var result = new LoaiDto()
                {
                    CreateAt = newLoai.CreateAt,
                    Id = newLoai.Id,
                    HinhAnhReturn = newLoai.HinhAnh,
                    TenLoai = newLoai.TenLoai,
                    MoTa = newLoai.MoTa,


                };
                return ServiceResult<LoaiDto>.Ok(result, 201, "Thêm loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<LoaiDto>.Fail($"Lỗi hệ thống: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<UpdateLoaiDto>> SuaLoai(int id, UpdateLoaiDto dto)
        {
            try
            {
                var loai = await db.Loai.FirstOrDefaultAsync(l => l.Id == id);
                if (loai == null)
                {
                    return ServiceResult<UpdateLoaiDto>.Fail("Không tìm thấy loại", 404);
                }

                var existingLoai = await db.Loai.FirstOrDefaultAsync(l => l.TenLoai == dto.TenLoai && l.Id != id);
                if (existingLoai is not null)
                {
                    return ServiceResult<UpdateLoaiDto>.Fail("Tên loại đã tồn tại", 400);
                }
                if (dto.NewHinhAnh is not null)
                {
                    var imagePath = await git.UpdateOneImg(dto.NewHinhAnh, "Loai");
                    dto.HinhAnh = imagePath.Url;
                }
                mapper.Map(dto, loai);
                await db.SaveChangesAsync();

                var result = mapper.Map<UpdateLoaiDto>(loai);
                return ServiceResult<UpdateLoaiDto>.Ok(result, 200, "Cập nhật loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<UpdateLoaiDto>.Fail($"Lỗi hệ thống {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<Loai>>>> GetLoai(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var loaiQuery = db.Loai
                    .Select(l => new Loai
                    {
                        Id = l.Id,
                        TenLoai = l.TenLoai,
                        MoTa = l.MoTa,
                        HinhAnh = l.HinhAnh,
                        Deleted = l.Deleted,
                        CreateAt = l.CreateAt,
                    })
                    .Where(l => l.Deleted != true).AsQueryable();
                if (!string.IsNullOrEmpty(query) && loaiQuery is not null)
                {
                    loaiQuery = loaiQuery.Where(l =>
                        l.TenLoai.Contains(query) ||
                        l.Id.ToString().Contains(query));
                }
                var result = await Helper.Pagination<Loai>.PaginationAsync(loaiQuery, page, pageSize, sort);
                return ServiceResult<PaginatedResult<List<Loai>>>.Ok(result, 200, "Lấy danh sách loại thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<Loai>>>.Fail("Lỗi hệ thống khi lấy danh sách loại", 500);
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