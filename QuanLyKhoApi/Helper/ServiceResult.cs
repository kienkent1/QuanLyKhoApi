namespace QuanLyKhoApi.Helper
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "Success") =>
            new ServiceResult<T> { Success = true, Message = message, StatusCode = 200, Data = data };

        public static ServiceResult<T> Fail(string message, int statusCode = 400) =>
            new ServiceResult<T> { Success = false, Message = message, StatusCode = statusCode };
    }
}
