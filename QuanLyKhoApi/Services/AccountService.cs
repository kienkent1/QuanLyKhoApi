using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class AccountService(AppDbContext db, IAuthService auth) : IAccountService
    {
        public async Task<ServiceResult<RegisterDto>> CreateAccount(RegisterDto dto)
        {
            try
            {
                var newAcc = await auth.RegisterAsync(dto, true);

                return newAcc;

            }
            catch (Exception ex)
            {
                return ServiceResult<RegisterDto>.Fail($"Lỗi thêm  tài khoản: {ex.Message}");
            }
        }

        public async Task<ServiceResult<PaginatedResult<List<AccountDto>>>> GetAllAccount(string? query, int page, int pageSize, SortOBJ? sort)
        {
            try
            {
                var accounts = db.TaiKhoan
                    .Include(t => t.NhanVien)
                    .AsQueryable();

                var listAcc = accounts.Select(a => new AccountDto
                {
                    IdNhanVien = a.IdNhanVien,
                    TenDangNhap = a.TenDangNhap,
                    TenNhanVien = a.NhanVien.TenNhanVien,
                    Email = a.NhanVien.email,
                    TrangThai = a.NhanVien.trangthai,
                    CreatedAt = a.CreatedAt
                });

                if (query is not null && !string.IsNullOrEmpty(query))
                {
                    listAcc = listAcc.Where(l => l.IdNhanVien.ToString().Contains(query) ||
                    l.TenNhanVien.Contains(query) ||
                    l.TenDangNhap.Contains(query) ||
                    l.Email.Contains(query));
                }

                var result = await Pagination<AccountDto>.PaginationAsync(listAcc, page, pageSize, sort);
                return ServiceResult<PaginatedResult<List<AccountDto>>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginatedResult<List<AccountDto>>>.Fail($"Lỗi lấy danh sách tài khoản: {ex.Message}");
            }
        }
    }
}
