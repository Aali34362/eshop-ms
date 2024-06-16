namespace Base.Exceptions.ExceptionHandler;

public class CustomExceptionHandler
    (ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exceptionMessage, DateTime.UtcNow);
        // Return false to continue with the default behavior
        // - or - return true to signal that this exception is handled

        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            ValidationException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            BadRequestException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            NotFoundException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status404NotFound
            ),
            ForbiddenAccessException =>(
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status403Forbidden
            ),
            TimeoutException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status408RequestTimeout
            ),
            UnauthorizedAccessException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status401Unauthorized
            ),
            NotImplementedException => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status501NotImplemented
            ),
            _ => (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path,
            ////Type = $"https://example.com/errors/{details.Title.Replace(" ", "-").ToLower()}"
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        ////problemDetails.Extensions.Add("Request", context.Request);
        ////problemDetails.Extensions.Add("Response", context.Response);
        ////problemDetails.Extensions.Add("Session", context.Session);
        ////problemDetails.Extensions.Add("WebSockets", context.WebSockets);

        if (exception is ValidationException validation)
            problemDetails.Extensions.Add("ValidationErrors", validation.Errors);

        context.Response.StatusCode = problemDetails.Status.Value;

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
