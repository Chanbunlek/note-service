using System;
using System.Net.Mime;

namespace TheFirstProject.Utils;

public class ExceptionHandler(RequestDelegate _next)
{
    private readonly RequestDelegate _next = _next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            var response = new ResponseMsg<string>(statusCode, false, ex.Message);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
