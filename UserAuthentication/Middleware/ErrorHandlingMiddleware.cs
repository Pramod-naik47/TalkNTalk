namespace UserAuthentication.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _requestDelegate;
    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _requestDelegate = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate.Invoke(context);
        }
        catch (Exception ex) 
        {
            _logger.LogInformation($"Reaquest {context.Request.Path} - {context.Request.Method} failed due to error {ex.Message} stackTrace {ex.StackTrace}");
        }
    }
}
