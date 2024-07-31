namespace KinKeeper.Api.Middlewares;

/// <summary>
/// Middleware для добавления идентификатора корреляции
/// </summary>
public sealed class CorrelationIdMiddleware : IMiddleware
{
    /// <summary>
    /// Название заголовка с идентификатором корреляции
    /// </summary>
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    /// <inheritdoc />
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string correlationId = GetCorrelationId(context);

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            return next(context);
        }
    }

    /// <summary>
    /// Получение идентификатора корреляции из HTTP-контекста(если возможно)
    /// </summary>
    /// <param name="context">HTTP-контекст</param>
    /// <returns>Идентификатор корреляции</returns>
    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? Guid.NewGuid().ToString();
    }
}
