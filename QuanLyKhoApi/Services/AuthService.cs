using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.IServices;
using QuanLyKhoApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyKhoApi.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        //tạo token
        private string CreateToken(TaiKhoan user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.NameIdentifier, user.IdNhanVien.ToString()),
                new Claim(ClaimTypes.Role, user.roles.ToString())
            };

            //system.identity.tokens.jwt tai thu vien ve
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
                );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDes = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDes);
        }

        //tạo chuỗi ngãu nhiên
        private string CreateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        //kiểm tra token có còn hợp lệ
        private async Task<TaiKhoan?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.TaiKhoan.FindAsync(userId);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }

        //tạo vào lưu token mới
        private async Task<string> GenerateAndSaveRefreshToken(TaiKhoan user)
        {
            var refreshToken = CreateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(2);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        //tạo token trả về
        private async Task<TokenResponseDto> CreateTokenResponseAsync(TaiKhoan user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
        }

        //check user tồn tại
        public async Task<bool> ExitsUser(Guid id)
        {
            return await context.NhanVien.AnyAsync(u => u.IdNhanVien == id);
        }
        //kiểm tra tài khoản có hợp lệ để register
        public async Task<bool> ValidateAccount(Guid id, string userName)
        {
            if(await context.TaiKhoan.AnyAsync(u => u.IdNhanVien == id))
             return false;
            if(await context.TaiKhoan.AnyAsync(u => u.TenDangNhap == userName))
                return false;
            return true;
        }

       
        public async Task<TaiKhoan> RegisterAsync(TaiKhoan req)
        {
            var exitsUser = await ExitsUser(req.IdNhanVien);
            if (exitsUser is false) return null;

            var validateAccount = await ValidateAccount(req.IdNhanVien, req.TenDangNhap);
            if (validateAccount is false) return null;
            var user = new TaiKhoan();
            var hashedPass = new PasswordHasher<TaiKhoan>()
                .HashPassword(user, req.Password);

            user.IdNhanVien = req.IdNhanVien;
            user.TenDangNhap = req.TenDangNhap;
            user.Password = hashedPass;
            context.TaiKhoan.Add(user);
            var userRole = new Role
            {
               Id = "1",
       
                VaiTro = "User"
            };
            await context.SaveChangesAsync();
            return user;
        } 
        public Task<TokenResponseDto?> LoginAsync(LoginDto req)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto req)
        {
            var user = await ValidateRefreshTokenAsync(req.UserId, req.RefreshToken);
            if (user is null) return null;
            return await CreateTokenResponseAsync(user);
        }

        
        public Task<TokenResponseDto?> GoogleLoginAsync(string idToken)
        {
            throw new NotImplementedException();
        }
    }
}
