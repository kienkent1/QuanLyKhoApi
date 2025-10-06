using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;

namespace QuanLyKhoApi.IServices
{
    public interface IAuthService
    {
        Task<TaiKhoan> RegisterAsync(RegisterDto req);
        Task<TokenResponseDto?> LoginAsync(LoginDto req);
        Task<RegisterGG?> RegisterGoogle(RegisterGG dto, GoogleAuthDto ggDto);
        Task<GoogleResponse> GetGoogleResponse(GoogleAuthDto dto);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto req);
        Task<TokenResponseDto?> GoogleLoginAsync(GoogleAuthDto dto);
        Task<bool> ExitsUser(Guid id);
        Task<bool> ValidateAccount(Guid id, string userName);
        Task<bool> IsEmailExit(string email);
    }
}
