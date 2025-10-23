using QuanLyKhoApi.Helper;
using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public static class AppServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<INhanVienService, NhanVienService>();
            services.AddScoped<ILoaiService, LoaiService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IHangHoaService, HangHoaService>();
            services.AddScoped<INhaCungCapService, NhaCungCapService>();
            services.AddScoped<GitHubImageService>();
            services.AddScoped<Ironbarcode>();
            services.AddScoped<IThongKeServices, ThongKeService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPhieuNhap, PhieuNhapService>();
            services.AddScoped<IPhieuXuat, PhieuXuatService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
