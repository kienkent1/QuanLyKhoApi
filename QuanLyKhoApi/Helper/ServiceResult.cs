using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace QuanLyKhoApi.Helper
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data,int statusCode = 200,  string message = "Success") =>
            new ServiceResult<T> { Success = true, Message = message, StatusCode = statusCode, Data = data };

        public static ServiceResult<T> Fail(string message, int statusCode = 400) =>
            new ServiceResult<T> { Success = false, Message = message, StatusCode = statusCode };
    }

    public static class MyStatusCodeBase
    {
        public static ActionResult MyStatusCode<T>(this ControllerBase controller, ServiceResult<T> result)
        {
            return controller.StatusCode(result.StatusCode, new
            {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }
    }
}
