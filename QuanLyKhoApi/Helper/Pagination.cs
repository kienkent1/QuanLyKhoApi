using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;
namespace QuanLyKhoApi.Helper
{
    public class Pagination<T>
    {
        public static async Task<PaginatedResult<List<T>>> PaginationAsync(IQueryable<T> items, int pageNumber, int pageSize, SortOBJ? query)
        {
            if (query is not null &&
    !string.IsNullOrEmpty(query.FilterName) &&
    !string.IsNullOrEmpty(query.FilterValue))
            {
                var property = typeof(T).GetProperty(query.FilterName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    object? typedValue = null;

                    try
                    {
                        if (propertyType == typeof(bool))
                        {
                            if (bool.TryParse(query.FilterValue, out bool boolVal))
                                typedValue = boolVal;
                        }
                        else if (propertyType == typeof(int))
                        {
                            if (int.TryParse(query.FilterValue, out int intVal))
                                typedValue = intVal;
                        }
                        else if (propertyType == typeof(decimal))
                        {
                            if (decimal.TryParse(query.FilterValue, out decimal decVal))
                                typedValue = decVal;
                        }
                        else if (propertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(query.FilterValue, out DateTime dateVal))
                                typedValue = dateVal;
                        }
                        else
                        {
                            typedValue = query.FilterValue;
                        }

                        if (typedValue != null)
                        {

                            if (propertyType == typeof(string))
                            {
                                items = items.Where($"{query.FilterName}.ToLower().Contains(@0)", typedValue.ToString()!.ToLower());
                            }
                            else
                            {
                                items = items.Where($"{query.FilterName} == @0", typedValue);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Filter parse error: {ex.Message}");
                    }
                }
            }

            var totalItems = await items.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (query is not null && !string.IsNullOrEmpty(query.FieldName))
            {
                items = items.OrderBy($"{query.FieldName} {(query.Isdesc == true ? "desc" : "asc")}");
            }

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var result = await items.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

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
        public string? FilterName { get; set; }
        public string? FilterValue { get; set; }
    }
}
