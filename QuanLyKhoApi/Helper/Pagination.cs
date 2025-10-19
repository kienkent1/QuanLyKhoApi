using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace QuanLyKhoApi.Helper
{
    public class Pagination<T>
    {
        public static async Task<PaginatedResult<List<T>>> PaginationAsync(IQueryable<T> items, int pageNumber, int pageSize, SortOBJ? query)
        {
            var totalItems = await items.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (query is not null && query.FieldName is not null)
            {
                items = items.OrderBy($"{query.FieldName} {(query.Isdesc == true ? "desc" : "asc")}");
            }
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var result = await items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<List<T>>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Data = result
            };
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
    public class SortOBJ
    {
        public string? FieldName { get; set; }
        public bool? Isdesc { get; set; } = false;
    }
}
