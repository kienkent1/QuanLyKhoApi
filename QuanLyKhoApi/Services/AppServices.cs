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
            services.AddScoped<GitHubImageService>();
            services.AddScoped<Ironbarcode>();
            return services;
        }
    }
}
