using System.Diagnostics;

namespace CustomMiddleware;

public class TransactionMiddleware(RequestDelegate next, ILogger<TransactionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IGuidGenerator guidGenerator)
    {
        var stopwatch = Stopwatch.StartNew();
        context.Response.Headers.Append("X-Transaction-Id", guidGenerator.GetGuid());

        await _next(context);

        stopwatch.Stop();
        logger.LogInformation("[Execution time: {TotalTime} ms]", stopwatch.ElapsedMilliseconds);
    }
}


public static class TransactionMiddlewareExtensions
{
    public static IApplicationBuilder UseTransactionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TransactionMiddleware>();
    }
}