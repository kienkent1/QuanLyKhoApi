using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Text.Json;

namespace QuanLyKhoApi.Services
{
    public class NhaCungCapService(AppDbContext context, IMapper mapper, GitHubImageService git) : INhaCungCapService
    {
        public async Task<ServiceResult<NhaCungCap?>> CreateNhaCungCapAsync(NhaCungCapDto dto)
        {
            try
            {
                var nhaCungCap = mapper.Map<NhaCungCap>(dto);
                nhaCungCap.CreateAt = DateTime.UtcNow;
                context.NhaCungCap.Add(nhaCungCap);
                await context.SaveChangesAsync();

                return ServiceResult<NhaCungCap?>.Ok(nhaCungCap, 201, "Tạo nhà cung cấp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<NhaCungCap?>.Fail("Lỗi hệ thống khi tạo nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<NhaCungCap>>>> GetAllNhaCungCapAsync(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var nhaCungCaps = context.NhaCungCap
                    .Select(n => new NhaCungCap
                    {
                        MaNCC = n.MaNCC,
                        TenNCC = n.TenNCC,
                        DiaChi = n.DiaChi,
                        DienThoai = n.DienThoai,
                        Email = n.Email,
                        HinhAnh = n.HinhAnh,
                        CreateAt = n.CreateAt,
                        Deleted = n.Deleted,
                    })
                    .Where(n => n.Deleted != true)
                    .AsQueryable();
                if (nhaCungCaps is not null && !string.IsNullOrEmpty(query))
                {
                    nhaCungCaps = nhaCungCaps.Where(n =>
                        n.TenNCC.Contains(query) ||
                        n.DienThoai.Contains(query) ||
                        n.Email.Contains(query));
                }
                var result = await Helper.Pagination<NhaCungCap>.PaginationAsync(nhaCungCaps, page, pageSize, sort);
                return ServiceResult<PaginatedResult<List<NhaCungCap>>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<NhaCungCap>>>.Fail("Lỗi hệ thống khi lấy danh sách nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<NhaCungCap?>> GetNhaCungCapByIdAsync(int id)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap
                    .Select(n => new NhaCungCap
                    {
                        MaNCC = n.MaNCC,
                        TenNCC = n.TenNCC,
                        DiaChi = n.DiaChi,
                        DienThoai = n.DienThoai,
                        Email = n.Email,
                        HinhAnh = n.HinhAnh,
                        CreateAt = n.CreateAt,
                        Deleted = n.Deleted,
                    })
                    .FirstOrDefaultAsync(n => n.MaNCC == id && n.Deleted == false);

                if (nhaCungCap == null)
                {
                    return ServiceResult<NhaCungCap?>.Fail("Không tìm thấy nhà cung cấp", 404);
                }

                return ServiceResult<NhaCungCap?>.Ok(nhaCungCap);
            }
            catch (Exception ex)
            {
                return ServiceResult<NhaCungCap?>.Fail("Lỗi hệ thống khi lấy thông tin nhà cung cấp", 500);
            }
        }

        public async Task<ServiceResult<NhaCungCapUpdateDto>> UpdateNhaCungCapAsync(int id, NhaCungCapUpdateDto dto)
        {
            try
            {
                var nhaCungCap = await context.NhaCungCap.FindAsync(id);
                if (nhaCungCap is null)
                    return ServiceResult<NhaCungCapUpdateDto>.Fail("Không tìm thấy nhà cung cấp", 404);
                if (dto.NewAnh is not null)
                {
                    var res = await git.UpdateOneImg(dto.NewAnh, "NhaCungCap");
                    dto.HinhAnh = res.Url;
                }

                mapper.Map(dto, nhaCungCap);

                context.NhaCungCap.Update(nhaCungCap);
                await context.SaveChangesAsync();

                return ServiceResult<NhaCungCapUpdateDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<NhaCungCapUpdateDto>.Fail("Lỗi hệ thống khi cập nhật nhà cung cấp", 500);
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

                return ServiceResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail("Lỗi hệ thống khi xóa nhà cung cấp", 500);
            }
        }
    }
}