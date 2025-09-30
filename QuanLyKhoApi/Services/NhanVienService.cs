using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public class NhanVienService(AppDbContext db, IMapper mapper) : INhanVienService
    {
        

        public async Task<IQueryable<NhanVien>> GetNhanVienAsync()
        {
            var nhanVien =   db.NhanVien.AsQueryable();
            return nhanVien;
        }

        public async Task<ProfileUserDto> ProfileUser(string id)
        {       
             var profile = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien.ToString() == id);
            var userName = await db.TaiKhoan.FirstOrDefaultAsync(tk => tk.IdNhanVien.ToString() == id);  
            
            return new ProfileUserDto
            {
                TenNhanVien = profile.TenNhanVien,
                UserName = userName.TenDangNhap,
                email = profile.email,
                sdt = profile.sdt,
                diaChi = profile.diaChi,
                UrlHinh = profile.UrlHinh,
                ngaySinh = profile.ngaySinh,
                gioiTinh = profile.gioiTinh,
                UpdateAt = profile.UpdateAt
            };

        }

        public async Task< NhanVienDto?> ThemNhanVienAsync(NhanVienDto nhanVien)
        {
            var NewNhanVien = mapper.Map<NhanVien>(nhanVien);
            await db.NhanVien.AddAsync(NewNhanVien);
            await db.SaveChangesAsync();
            return mapper.Map<NhanVienDto>(NewNhanVien);
        }

        public async Task<NhanVienDto> UpdateNhanVienAsync(Guid id, NhanVienDto dto)
        {
            var UpdateNV = await db.NhanVien.FirstOrDefaultAsync(nv => nv.IdNhanVien == id);
            if (UpdateNV is null) return null;

            mapper.Map(dto, UpdateNV);
             db.NhanVien.Update(UpdateNV);
            db.SaveChanges();
            return mapper.Map<NhanVienDto>(UpdateNV);
        }

        public async Task<string> ChangePassword(ChangePassworDto Pass, Guid id)
        {
            var userPass = await db.TaiKhoan.FirstOrDefaultAsync(tk => tk.IdNhanVien == id);
            var user = await db.NhanVien.FirstOrDefaultAsync(us => us.IdNhanVien == id) ;
            if (user is null || userPass is null) return "User not found";
            var NewReqCP = mapper.Map<ChangePassModel>(user);
            NewReqCP.PasswordHash = userPass.Password;
            
            if (new PasswordHasher<ChangePassModel>().VerifyHashedPassword(NewReqCP, userPass.Password, Pass.OldPassword)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }
            if (Pass.NewPassword != Pass.ConfirmPassword) return null;

            string HashPass = new PasswordHasher<ChangePassModel>().HashPassword(NewReqCP, Pass.NewPassword);
            return HashPass;
        }   
    }
}
