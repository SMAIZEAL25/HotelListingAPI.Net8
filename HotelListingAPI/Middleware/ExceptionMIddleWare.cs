using HotelListingAPI.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HotelListingAPI.Middleware
{
    public class ExceptionMIddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMIddleWare> _logger;

        public ExceptionMIddleWare(RequestDelegate next, ILogger<ExceptionMIddleWare> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"Something went wrong {context.Response}");
                await HandlerExceptionAsync(context, ex);
            }
        }

        private Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var Errordeatails = new ErrorDetails
            {
                ErrorType = "failure",
                ErrorMessage = ex.Message,
            };
           
            switch (ex)
            {
                case NotfoundException notfoundException:
                    statusCode = HttpStatusCode.NotFound;
                    Errordeatails.ErrorType = "Not Found";
                    break;
                default:
                    break;
            }

            string response = JsonConvert.SerializeObject(Errordeatails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }
    }

    public class ErrorDetails
    {
        public string ErrorType { get; set; }

        public string ErrorMessage { get; set; }
    }
}
