using Microsoft.EntityFrameworkCore;

namespace QuanLyKhoApi.Helper
{
    public class Pagination<T>
    {
        public static async Task<List<T>> PaginationAsync(IQueryable<T> items, int pageNumber, int pageSize)
        {
            var totalItems = await items.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var result = await items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return result;
        }

    }       
    public class PaginatedResult<T>
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
            public T? Data { get; set; }
        }
}
