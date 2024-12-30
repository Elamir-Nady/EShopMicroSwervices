﻿

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exception.Message, DateTime.Now);
        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status500InternalServerError),
            ValidationException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status400BadRequest),
            BadRequestException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status404NotFound),
            _ => (exception.Message, exception.GetType().Name, context.Response.StatusCode = StatusCodes.Status500InternalServerError)
        };
        //var ProblemDetails = new ProblemDetails
        //{
        //    Title = details.Title,
        //    Status = details.StatusCode,
        //    Detail = details.Detail,
        //    Instance = context.Request.Path,
        //};
        //ProblemDetails.Extensions.Add("tracId", context.TraceIdentifier);
        //if(exception is ValidationException validationException)
        //{
        //    ProblemDetails.Extensions.Add("ValidationErrors", validationException.Errors);


        //}
        //await context.Response.WriteAsJsonAsync(ProblemDetails,cancellationToken:cancellationToken);
        return true;
    }
}
 