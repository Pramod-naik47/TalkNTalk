using System.Net;
using TalkNTalk.Helper;
using TalkNTalk.Models;

namespace TalkNTalk.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _requestDelegate;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, 
                                       RequestDelegate next)
    {
        _logger = logger;
        _requestDelegate = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            //If no error invoking the next middleware in the pipeline
            await _requestDelegate.Invoke(context);
        }
        catch (Exception ex) 
        {
            //Logging error for debugging purpose
            _logger.LogInformation($"Reaquest {context.Request.Path} - {context.Request.Method} failed due to error {ex.Message} \n stackTrace {ex.StackTrace}");
            ErrorModel errorModel = new ErrorModel
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = "Internal server error"
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //Checking if environment is development then attaching error message to api reponse.
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == nameof(ApplicationEnvironment.Development))
            {
                errorModel.Message = ex.Message;
                errorModel.StackTrace = ex.StackTrace;
            }
            //Attaching error response
            await context.Response.WriteAsJsonAsync(errorModel);
        }
    }
}
