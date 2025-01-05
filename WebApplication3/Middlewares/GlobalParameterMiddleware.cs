using WebApplication3.Interfaces;

namespace WebApplication3.Middlewares;

public class GlobalParameterMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalParameterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IGlobalParameterService parameterService)
    {
        // قراءة البارامترات من الـ Header
        if (context.Request.Headers.TryGetValue("X-Schema", out var schemaValue))
        {
            parameterService.SetSchemaName(schemaValue);
        }

        await _next(context);
    }
}