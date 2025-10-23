using QuanLyKhoApi.Data;
using QuanLyKhoApi.Dto.AuthenDto;
using QuanLyKhoApi.Helper;

namespace QuanLyKhoApi.IServices
{
    public interface IAuthService
    {
        Task<ServiceResult<RegisterDto>> RegisterAsync(RegisterDto req, bool isFromAdmin = false);
        Task<ServiceResult<TokenResponseDto>?> LoginAsync(LoginDto req);
        Task<ServiceResult<RegisterGG>?> RegisterGoogle(RegisterGG dto, GoogleAuthDto ggDto);
        Task<GoogleResponse> GetGoogleResponse(GoogleAuthDto dto);
        Task<ServiceResult<TokenResponseDto>?> RefreshTokenAsync(RefreshTokenRequestDto req);
        Task<ServiceResult<TokenResponseDto>?> GoogleLoginAsync(GoogleAuthDto dto);
        Task<bool> ExitsUser(Guid id);
        Task<bool> ValidateAccount(Guid id, string userName);
        Task<bool> IsEmailExit(string email);
        string CreateToken(TaiKhoan user);
    }
}
