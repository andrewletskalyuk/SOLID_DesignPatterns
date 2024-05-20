using RedisCacheExample.Services;

namespace RedisCacheExample.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly RateLimiterService _rateLimiterService;

    public RateLimitingMiddleware(RequestDelegate next, RateLimiterService rateLimiterService)
    {
        _next = next;
        _rateLimiterService = rateLimiterService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : context.Connection.RemoteIpAddress.ToString();

        // TODO: maxRequests get from outside
        if (!await _rateLimiterService.IsRequestAllowedAsync(userId, 5, TimeSpan.FromMinutes(1)))
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            return;
        }

        await _next(context);
    }
}
