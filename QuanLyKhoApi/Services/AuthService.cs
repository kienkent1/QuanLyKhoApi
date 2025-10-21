using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyKhoApi.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration, IMapper mapper) : IAuthService
    {
        //tạo token
        private string CreateToken(TaiKhoan user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.NameIdentifier, user.IdNhanVien.ToString()),

            };

            //vì dùng user id để tìm role của user đó nên không cần lặp qua roles nữa
            //foreach (var role in user.TaiKhoanRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.RoleId));
            //}
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
        private async Task<TaiKhoan?> ValidateRefreshTokenAsync(string refreshToken)
        {
            var token = await context.TaiKhoanToken
        .FirstOrDefaultAsync(t => t.Id.ToString() == refreshToken);

            if (token is null) return null;

            // load user kèm role 
            return await context.TaiKhoan
                .Include(u => u.TaiKhoanRoles)
                .ThenInclude(tr => tr.Role)
                .FirstOrDefaultAsync(u => u.IdNhanVien == token.IdTaiKhoan);
        }

        //tạo vào lưu token mới
        private async Task<string> GenerateAndSaveRefreshToken(Guid IdTaiKhoan)
        {
            var token = await context.TaiKhoanToken.AddAsync(new TaiKhoanToken
            {
                IdTaiKhoan = IdTaiKhoan,
                ExpiryTime = DateTime.UtcNow.AddDays(2)
            });

            await context.SaveChangesAsync();

            return token.Entity.Id.ToString();
        }

        //tạo token trả về
        private async Task<TokenResponseDto> CreateTokenResponseAsync(TaiKhoan user, bool isTokenExpry, string? refToken)
        {
            if (isTokenExpry is true)
            {
                return new TokenResponseDto
                {
                    AccessToken = CreateToken(user),
                    RefreshToken = refToken
                };
            }
            else
            {
                return new TokenResponseDto
                {
                    AccessToken = CreateToken(user),
                    RefreshToken = await GenerateAndSaveRefreshToken(user.IdNhanVien)
                };
            }

        }

        //check user tồn tại
        public async Task<bool> ExitsUser(Guid id)
        {
            return await context.NhanVien.AnyAsync(u => u.IdNhanVien == id);
        }
        //kiểm tra tài khoản có hợp lệ để register
        public async Task<bool> ValidateAccount(Guid id, string userName)
        {
            if (await context.TaiKhoan.AnyAsync(u => u.IdNhanVien == id))
                return false;
            if (await context.TaiKhoan.AnyAsync(u => u.TenDangNhap == userName))
                return false;
            if (await ExitsUser(id))
                return true;
            return true;
        }

        public async Task<bool> IsEmailExit(string email)
        {
            return await context.NhanVien.AnyAsync(u => u.email == email);
        }
        public async Task<ServiceResult<RegisterDto>> RegisterAsync(RegisterDto req)
        {
            try
            {
                if (await ValidateAccount(req.IdNhanVien, req.TenDangNhap) == false)
                    return ServiceResult<RegisterDto>.Fail("Tên đăng nhập hoặc tài khoản đã tồn tại", 400);

                var hashedPass = new PasswordHasher<RegisterDto>()
                    .HashPassword(req, req.Password);

                req.Password = hashedPass;
                req.CreatedAt = DateTime.UtcNow;
                var account = mapper.Map<TaiKhoan>(req);
                context.TaiKhoan.Add(account);
                var userRole = new TaiKhoanRole
                {
                    TaiKhoanId = req.IdNhanVien,
                    RoleId = "user"
                };
                await context.TaiKhoanRoles.AddAsync(userRole);
                await context.ComfirmAccounts.AddAsync(new ComfirmAccount
                {
                    IdTaiKhoan = req.IdNhanVien,
                    CreatedAt = DateTime.UtcNow
                });

                await context.SaveChangesAsync();

                return ServiceResult<RegisterDto>.Ok(req, 201);
            }
            catch (Exception ex)
            {
                return ServiceResult<RegisterDto>.Fail("Lỗi hệ thống", 500);
            }

        }
        public async Task<ServiceResult<TokenResponseDto>?> LoginAsync(LoginDto req)
        {
            var user = await context.TaiKhoan
                 .Include(t => t.NhanVien)
                 .Include(t => t.TaiKhoanRoles)
         .ThenInclude(tr => tr.Role)
        .FirstOrDefaultAsync(t =>
            (t.TenDangNhap == req.UserNameOrEmail) ||
            t.NhanVien.email == req.UserNameOrEmail);

            if (user is null) return ServiceResult<TokenResponseDto>.Fail("Tên đăng nhập hoặc Email không chính xác", 400);

            if (user.NhanVien.trangthai is false)
            {
                return ServiceResult<TokenResponseDto>.Fail("Tài khoản của bạn hiện không thể đăng nhập. Vui lòng liên hệ Admin", 403);
            }

            if (new PasswordHasher<TaiKhoan>().VerifyHashedPassword(user, user.Password, req.Password)
                == PasswordVerificationResult.Failed)
            {
                return ServiceResult<TokenResponseDto>.Fail("Không đúng mật khẩu", 400);
            }


            return ServiceResult<TokenResponseDto>.Ok(await CreateTokenResponseAsync(user, false, null));
        }

        public async Task<ServiceResult<TokenResponseDto>?> RefreshTokenAsync(RefreshTokenRequestDto req)
        {
            var user = await ValidateRefreshTokenAsync(req.RefreshToken);
            if (user is null) return ServiceResult<TokenResponseDto>.Fail("RefreshToken không hợp lệ", 400);
            var refToken = await context.TaiKhoanToken
                .FirstOrDefaultAsync(t => t.Id.ToString() == req.RefreshToken);
            if (refToken.ExpiryTime < DateTime.UtcNow)
            {
                context.TaiKhoanToken.Remove(refToken);
                await context.SaveChangesAsync();
                return ServiceResult<TokenResponseDto>.Ok(await CreateTokenResponseAsync(user, false, null));
            }


            return ServiceResult<TokenResponseDto>.Ok(await CreateTokenResponseAsync(user, true, req.RefreshToken));
        }


        #region Authentication GG
        public async Task<GoogleResponse> GetGoogleResponse(GoogleAuthDto dto)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                var idtk = configuration.GetValue<string>("Authentication:Google:ClientId")!;
                payload = await GoogleJsonWebSignature.ValidateAsync(
                    dto.IdToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[]
                        {
                            configuration.GetValue<string>("Authentication:Google:ClientId")!
                        }
                    }
                    );

                if (!payload.EmailVerified) return null;
                var res = new GoogleResponse();
                res.GoogleSub = payload.Subject;
                res.Email = payload.Email.Trim().ToLowerInvariant();
                res.EmailVerified = payload.EmailVerified;
                res.Picture = payload.Picture;
                return res;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ServiceResult<RegisterGG>?> RegisterGoogle(RegisterGG req, GoogleAuthDto gg)
        {
            try
            {
                var res = await GetGoogleResponse(gg);
                if (res is null) return ServiceResult<RegisterGG>.Fail("Token không hợp lệ", 400);
                var exitsUser = await ExitsUser(req.IdNhanVien);
                if (exitsUser is false) return ServiceResult<RegisterGG>.Fail("Nhân viên không tồn tại", 404);

                var validateAccount = await ValidateAccount(req.IdNhanVien, req.TenDangNhap);
                if (validateAccount is false) return ServiceResult<RegisterGG>.Fail("Tên đăng nhập đã tồn tại", 400);
                if (String.IsNullOrEmpty(req.TenDangNhap)) req.TenDangNhap = res.Email;

                req.CreatedAt = DateTime.UtcNow;
                var account = mapper.Map<TaiKhoan>(req);
                account.GoogleId = res.GoogleSub;
                context.TaiKhoan.Add(account);
                var userRole = new TaiKhoanRole
                {
                    TaiKhoanId = req.IdNhanVien,
                    RoleId = "user"
                };
                context.TaiKhoanRoles.Add(userRole);

                var user = await context.NhanVien.Include(u => u.TaiKhoan)
                    .FirstOrDefaultAsync(nv => nv.IdNhanVien == req.IdNhanVien && nv.UrlHinh == null);
                if (user is not null)
                {
                    user.UrlHinh = res.Picture;
                    context.NhanVien.Update(user);
                }
                await context.ComfirmAccounts.AddAsync(new ComfirmAccount
                {
                    IdTaiKhoan = req.IdNhanVien,
                    CreatedAt = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
                return ServiceResult<RegisterGG>.Ok(req);
            }
            catch (Exception ex)
            {
                return ServiceResult<RegisterGG>.Fail($"Lỗi: {ex.Message}", 500);
            }
        }
        public async Task<ServiceResult<TokenResponseDto>?> GoogleLoginAsync(GoogleAuthDto dto)
        {
            try
            {
                var GGResponse = await GetGoogleResponse(dto);
                if (GGResponse is null) return ServiceResult<TokenResponseDto>.Fail("Id token không hợp lệ", 400);
                var Account = await context.TaiKhoan.Include(u => u.NhanVien)
                    .FirstOrDefaultAsync(u => u.GoogleId == GGResponse.GoogleSub || u.NhanVien.email == GGResponse.Email);



                if (Account is not null)
                {
                    if (Account.GoogleId == GGResponse.GoogleSub && Account.NhanVien.trangthai != false)
                    {
                        return ServiceResult<TokenResponseDto>.Ok(await CreateTokenResponseAsync(Account, false, null));
                    }
                    else if (Account.NhanVien.trangthai == false)
                    {
                        return ServiceResult<TokenResponseDto>.Fail("Tài khoản của bạn hiện không thể đăng nhập. Vui lòng liên hệ Admin", 403);
                    }
                    else
                    {
                        var isAccExit = await context.TaiKhoan.FirstOrDefaultAsync(t => t.GoogleId == GGResponse.GoogleSub);
                        if (isAccExit is not null) return ServiceResult<TokenResponseDto>.Ok(await CreateTokenResponseAsync(isAccExit, false, null));
                        else return ServiceResult<TokenResponseDto>.Fail("Tài khoản không tồn tại", 404);
                    }

                }
                else
                {
                    return ServiceResult<TokenResponseDto>.Fail("Tài khoản không tồn tại", 404);
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<TokenResponseDto>.Fail($"Lỗi: {ex.Message}", 500);
            }

        }
        #endregion
    }
}
