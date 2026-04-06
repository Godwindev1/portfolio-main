using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using AdminAuth.Middleware;
using System.ComponentModel.DataAnnotations;

namespace AdminAuth.Controllers;


public class AuthController : Controller
{
    private readonly IConfiguration _config;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration config, ILogger<AuthController> logger)
    {
        _config = config;
        _logger = logger;
    }

    // GET /auth/login
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        // Already logged in → send to admin dashboard
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Dashboard", "Admin");

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST /auth/login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm]LoginRequest model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var ip             = LoginRateLimitMiddleware.GetClientIp(HttpContext);
        var adminUsername  = _config["BACKEND_LOGIN_USER"];
        var adminPassword  = _config["BACKEND_LOGIN_KEY"];

        // ── Constant-time comparison to prevent timing attacks ───────────────
        var usernameMatch = string.Equals(model.Username, adminUsername, StringComparison.Ordinal);
        var passwordMatch = string.Equals(model.Password, adminPassword, StringComparison.Ordinal);

        if (!usernameMatch || !passwordMatch)
        {
            LoginRateLimitMiddleware.RecordFailedAttempt(ip);
            _logger.LogWarning("Failed login attempt from IP {IP} at {Time}", ip, DateTime.UtcNow);

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        // ── Success: clear rate limit and sign in ────────────────────────────
        LoginRateLimitMiddleware.ClearAttempts(ip);
        _logger.LogInformation("Admin login from IP {IP} at {Time}", ip, DateTime.UtcNow);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name,           adminUsername!),
            new(ClaimTypes.Role,           "Admin"),
            new("login_time",              DateTime.UtcNow.ToString("o")),
            new("login_ip",                ip),
        };

        var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProps = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe,
            ExpiresUtc   = model.RememberMe
                ? DateTimeOffset.UtcNow.AddDays(7)
                : DateTimeOffset.UtcNow.AddHours(8),
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProps);

        // Safe redirect: only allow local URLs
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Dashboard", "Admin");
    }

    // POST /auth/logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    // GET /auth/denied
    [HttpGet]
    public IActionResult Denied() => View();
}