using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> option) : DbContext(option)
    {
    }
}
