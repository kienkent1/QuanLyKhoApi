using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.Linq;

namespace QuanLyKhoApi.Services
{
    public class NhanVienService(AppDbContext db, IMapper mapper, GitHubImageService git) : INhanVienService
    {
        public async Task<ServiceResult<PaginatedResult<List<NhanVien>>>> GetNhanVienAsync(string? query, int page, int pageSize)
        {
            try
            {
                var nhanVien = db.NhanVien.AsQueryable();
                if (query is not null)
                {
                    nhanVien = nhanVien.Where(nv =>
                    nv.TenNhanVien.Contains(query) ||
                    nv.IdNhanVien.ToString().Contains(query) ||
                    nv.email.Contains(query));
                }
                var result = await Helper.Pagination<NhanVien>.PaginationAsync(nhanVien, page, pageSize);
                int tongNV = await nhanVien.CountAsync();
                return ServiceResult<PaginatedResult<List<NhanVien>>>.Ok(new PaginatedResult<List<NhanVien>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = tongNV,
                    TotalPages = (int)Math.Ceiling((double)tongNV / pageSize),
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<NhanVien>>>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<NhanVienDto>> GetNhanVienByIdAsync(Guid id)
        {
            try
            {
                var nhanVien = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien == id);
                if (nhanVien is null)
                    return ServiceResult<NhanVienDto>.Fail("Không tìm thấy nhân viên", 404);

                var result = mapper.Map<NhanVienDto>(nhanVien);
                return ServiceResult<NhanVienDto>.Ok(result, 200, "Lấy thông tin nhân viên thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<NhanVienDto>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<bool>> DeleteNhanVienAsync(Guid id)
        {
            try
            {
                var nhanVien = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien == id);
                if (nhanVien is null)
                    return ServiceResult<bool>.Fail("Không tìm thấy nhân viên", 404);

                db.NhanVien.Remove(nhanVien);
                await db.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true, 200, "Xóa nhân viên thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<ProfileUserDto>> ProfileUser(string id)
        {
            var profile = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien.ToString() == id);
            var userName = await db.TaiKhoan.FirstOrDefaultAsync(tk => tk.IdNhanVien.ToString() == id);
            if (profile is null || userName is null) return ServiceResult<ProfileUserDto>
                    .Fail("Nhân viên không tồn tại", 404);
            var result = new ProfileUserDto
            {
                TenNhanVien = profile.TenNhanVien,
                UserName = userName.TenDangNhap,
                email = profile.email,
                sdt = profile.sdt,
                diaChi = profile.diaChi,
                UrlHinh = profile.UrlHinh,
                ngaySinh = profile.ngaySinh,
                gioiTinh = Enum.TryParse<BaseEnum.GIOITINH>(profile.gioiTinh, out var parsedGioiTinh) ? parsedGioiTinh : BaseEnum.GIOITINH.Nam,
                UpdateAt = profile.UpdateAt
            };
            return ServiceResult<ProfileUserDto>.Ok(result);
        }

        public async Task<ServiceResult<NhanVienDto>> ThemNhanVienAsync(NhanVienDto nhanVien)
        {
            try
            {
                if (nhanVien is null) return ServiceResult<NhanVienDto>.Fail("Dữ liệu không hợp lệ", 400);
                var NewNhanVien = mapper.Map<NhanVien>(nhanVien);
                var urlHinh = new GitHubImageService.GitHubRes();
                if (nhanVien.Hinh != null)
                {
                    urlHinh = await git.UpdateOneImg(nhanVien.Hinh, "User");
                }
                NewNhanVien.IdNhanVien = Guid.NewGuid();
                NewNhanVien.ngaySinh = DateTime.SpecifyKind(NewNhanVien.ngaySinh, DateTimeKind.Utc);
                NewNhanVien.UrlHinh = urlHinh.Url;
                await db.NhanVien.AddAsync(NewNhanVien);
                await db.SaveChangesAsync();
                return ServiceResult<NhanVienDto>.Ok(mapper.Map<NhanVienDto>(NewNhanVien));
            }
            catch (Exception ex)
            {
                return ServiceResult<NhanVienDto>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<UpdateNhanVienDto>> UpdateNhanVienAsync(Guid id, UpdateNhanVienDto dto)
        {
            if (dto is null) return ServiceResult<UpdateNhanVienDto>.Fail("Không có dữ liệu để cập nhật", 400);
            var nv = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien == id);
            if (nv is null) return ServiceResult<UpdateNhanVienDto>.Fail("Không tìm thấy nhân viên", 404);

            if (!string.IsNullOrEmpty(dto.TenNhanVien))
                nv.TenNhanVien = dto.TenNhanVien;

            if (!string.IsNullOrEmpty(dto.email))
                nv.email = dto.email;

            if (!string.IsNullOrEmpty(dto.sdt))
                nv.sdt = dto.sdt;

            if (dto.diaChi is not null)
                nv.diaChi = dto.diaChi;

            if (dto.ngaySinh != default)
                nv.ngaySinh = DateTime.SpecifyKind((DateTime)dto.ngaySinh, DateTimeKind.Utc);

            if (!string.IsNullOrEmpty(dto.chucVu))
                nv.chucVu = dto.chucVu;

            nv.trangthai = dto.trangthai;
            nv.UpdateAt = DateTime.UtcNow;
            db.NhanVien.Update(nv);
            db.SaveChanges();
            return ServiceResult<UpdateNhanVienDto>.Ok(dto);
        }

        public async Task<ServiceResult<string>> ChangePassword(ChangePassworDto Pass, Guid id)
        {
            var userPass = await db.TaiKhoan.FirstOrDefaultAsync(tk => tk.IdNhanVien == id);
            var user = await db.NhanVien.FirstOrDefaultAsync(us => us.IdNhanVien == id);
            if (user is null || userPass is null) return ServiceResult<string>.Fail("Không tìm thấy tài khoản", 404);
            var NewReqCP = mapper.Map<ChangePassModel>(user);
            NewReqCP.PasswordHash = userPass.Password;

            if (new PasswordHasher<ChangePassModel>().VerifyHashedPassword(NewReqCP, userPass.Password, Pass.OldPassword)
                == PasswordVerificationResult.Failed)
            {
                return ServiceResult<string>.Fail("Mật khẩu không đúng", 400);
            }
            if (Pass.NewPassword != Pass.ConfirmPassword) return ServiceResult<string>.Fail("Mật khẩu mới và xác nhận mật khẩu khác nhau", 400);

            string HashPass = new PasswordHasher<ChangePassModel>().HashPassword(NewReqCP, Pass.NewPassword);
            return ServiceResult<string>.Ok(HashPass);
        }

        public async Task<ServiceResult<string>> UpdateAvatarNV(string id, IFormFile file)
        {
            try
            {
                var user = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien.ToString() == id);
                if (user is null) return ServiceResult<string>.Fail("Không tìm thấy nhân viên", 404);
                var urlHinh = await git.UpdateOneImg(file, "User");
                if (urlHinh is null) return ServiceResult<string>.Fail("Cập nhật ảnh đại diện thất bại", 500);
                user.UrlHinh = urlHinh.Url;
                user.UpdateAt = DateTime.UtcNow;
                db.NhanVien.Update(user);
                await db.SaveChangesAsync();
                return ServiceResult<string>.Ok(user.UrlHinh);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }

        public async Task<ServiceResult<HashSet<string>>> GetClaimUser(string id)
        {
            try
            {
                var claims = await db.TaiKhoan
                    .Where(tk => tk.IdNhanVien.ToString() == id)
                    .SelectMany(tk => tk.TaiKhoanRoles)
                    .SelectMany(tkr => tkr.Role.RoleClaims)
                    .Select(rc => rc.Claim.Quyen)
                    .Distinct()
                    .ToHashSetAsync();

                if (claims == null || claims.Count == 0)
                    return ServiceResult<HashSet<string>>.Fail("Người dùng không tồn tại hoặc không có quyền", 404);

                return ServiceResult<HashSet<string>>.Ok(claims);
            }
            catch (Exception ex)
            {
                return ServiceResult<HashSet<string>>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }
    }
}