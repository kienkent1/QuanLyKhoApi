using QuanLyKhoApi.IServices;

namespace QuanLyKhoApi.Services
{
    public static class AppServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<INhanVienService, NhanVienService>();
            return services;
        }
    }
}
