using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Models;

namespace QuanLyKhoApi.IServices
{
    public interface IAuthService
    {
        Task<TaiKhoan> RegisterAsync(TaiKhoan req);
        Task<TokenResponseDto?> LoginAsync(LoginDto req);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto req);
        Task<TokenResponseDto?> GoogleLoginAsync(string idToken);
        Task<bool> ExitsUser(Guid id);
        Task<bool> ValidateAccount(Guid id, string userName);
        
    }
}
