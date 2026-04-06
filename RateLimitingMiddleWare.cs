using System.Collections.Concurrent;

namespace AdminAuth.Middleware;

/// <summary>
/// Tracks failed login attempts per IP and blocks after threshold.
/// </summary>
public class LoginRateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _maxAttempts;
    private readonly TimeSpan _window;

    // Thread-safe store: IP → (attempt count, window start)
    private static readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> _attempts = new();

    public LoginRateLimitMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next        = next;
        _maxAttempts = config.GetValue<int>("RateLimit:MaxLoginAttempts", 5);
        _window      = TimeSpan.FromMinutes(config.GetValue<int>("RateLimit:WindowMinutes", 10));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Only gate POST /auth/login
        if (context.Request.Path.StartsWithSegments("/auth/login") &&
            context.Request.Method == "POST")
        {
            var ip = GetClientIp(context);
            if (IsBlocked(ip))
            {
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsync("Too many login attempts. Please wait and try again.");
                return;
            }
        }

        await _next(context);
    }

    public static void RecordFailedAttempt(string ip)
    {
        _attempts.AddOrUpdate(ip,
            addValue: (1, DateTime.UtcNow),
            updateValueFactory: (_, existing) =>
            {
                var (count, start) = existing;
                // Reset window if expired
                if (DateTime.UtcNow - start > TimeSpan.FromMinutes(10))
                    return (1, DateTime.UtcNow);
                return (count + 1, start);
            });
    }

    public static void ClearAttempts(string ip) => _attempts.TryRemove(ip, out _);

    private bool IsBlocked(string ip)
    {
        if (!_attempts.TryGetValue(ip, out var entry)) return false;
        if (DateTime.UtcNow - entry.WindowStart > _window)
        {
            _attempts.TryRemove(ip, out _);
            return false;
        }
        return entry.Count >= _maxAttempts;
    }

    public static string GetClientIp(HttpContext context)
    {
        return context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
            ?? context.Connection.RemoteIpAddress?.ToString()
            ?? "unknown";
    }
}