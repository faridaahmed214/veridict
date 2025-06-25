using System;
using System.Text.Json;
using DomanLayer.Exceptions;
using Shared.ErrorModels;
using static System.Net.Mime.MediaTypeNames;

namespace API.CustomeMiddleWares
{
    public class CustomExceptionHandlerMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWares> _logger;
     

        public CustomExceptionHandlerMiddleWares(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWares> logger)
        {
            _next = next;
            _logger = logger;
            
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Want Wrong");
                await HandleExcepthionAsync(httpContext, ex);


            }
        }

        private static async Task HandleExcepthionAsync(HttpContext httpContext,Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                
                ErrorMessage = ex.Message
            };
            Response.StatusCode = ex switch
            {
                BadRequestdException badRequestdException => GetBadRequestdErrors(badRequestdException , Response),
                NotFoundException => StatusCodes.Status404NotFound,
                UnAthorizedException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError

            };
            httpContext.Response.StatusCode = Response.StatusCode;

            // Return Object As JSON
            await httpContext.Response.WriteAsJsonAsync(value: Response);

        }
        private static int GetBadRequestdErrors(BadRequestdException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
