using AdminAuth.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(
        builder.Configuration["CONNECTION_STRING"],  new MySqlServerVersion(new Version(8, 0, 34))
    )
);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICaseStudyRepository, CaseStudyRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath        = "/auth/login";
        options.LogoutPath       = "/auth/logout";
        options.AccessDeniedPath = "/auth/denied";
 
        options.ExpireTimeSpan    = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
 
        // Harden the cookie
        //options.Cookie.HttpOnly   = true;
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //options.Cookie.SameSite   = SameSiteMode.Strict;
        options.Cookie.Name       = "__admin_session";
    });
 

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});
 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var repo = services.GetRequiredService<ICaseStudyRepository>();
    var seeder = new CaseStudySeedTest();
    await seeder.GenerateCaseStudies(repo);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Rate limiting middleware on /auth/login to block brute force
app.UseMiddleware<LoginRateLimitMiddleware>();
 
app.UseAuthentication(); 
app.UseAuthorization();
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
