using Allspark.Application.Exceptions;
using Allspark.Application.Wrappers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Allspark.Web.Middlewares;

[ExcludeFromCodeCoverage]
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            _logger.LogError(error, "Unexpected error occurred: {ErrorMessage}", error.Message);

            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

            switch (error)
            {
                case ApiException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case AllsparkValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}