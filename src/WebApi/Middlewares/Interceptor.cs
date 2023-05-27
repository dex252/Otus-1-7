using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApi.Exceptions;

namespace WebApi.Middlewares
{
    public class Interceptor
    {
        private RequestDelegate Next;

        public Interceptor(RequestDelegate next)
        {
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (CustomException exception)
            {
                var response = context.Response;
                response.ContentType = "text/plain";
                response.StatusCode = (int)exception.ErrorType;

                await response.WriteAsync(exception.Message);
            }
        }
    }
}
