using AdminAuth.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Amazon.S3;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var BucketRootUser = builder.Configuration["MINIO_ROOT_USER"];
var BucketRootPassword = builder.Configuration["MINIO_ROOT_PASSWORD"];

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(
        builder.Configuration["CONNECTION_STRING"],  new MySqlServerVersion(new Version(8, 0, 34))
    )
);

 var s3Config = new AmazonS3Config
{
    ServiceURL = "http://ppmpdb:9000",
    ForcePathStyle = true // Essential for MinIO
};

builder.Services.AddSingleton(s3Config);

builder.Services.AddSingleton<IAmazonS3>(sp => 
    new AmazonS3Client(BucketRootUser, BucketRootPassword, s3Config));

builder.Services.AddSingleton<BucketService>();

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

    var bucketService = services.GetRequiredService<BucketService>();
    await bucketService.CreateBucketAsync();

    var repo = services.GetRequiredService<ICaseStudyRepository>();
    var seeder = new CaseStudySeedTest();
    //await seeder.GenerateCaseStudies(repo);
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
