

using PlayListProject.Application.CustomsExceptions;
using System.Net;
using System.Text.Json;

namespace PlayListProject.Presentation.Middleware
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                if (ex is PlayListException playListException)
                {
                    context.Response.StatusCode = (int)playListException.StatusCode;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        playListException.StatusCode,
                        playListException.Message
                    }));
                }
                else
                {
                    await context.Response.WriteAsync(
                            JsonSerializer.Serialize(new
                            {
                                StatusCode = HttpStatusCode.InternalServerError,
                                Message = "InternalServerError"
                            })
                        );
                }
            }

        }
    }
}
