using AdminAuth.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Amazon.S3;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

//THIS CURRENT BRANCH USES R2 intead of MINIO 

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
    
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseMySql(
        builder.Configuration["CONNECTION_STRING"],  new MySqlServerVersion(new Version(8, 0, 34))
    )
);

var s3Config = new AmazonS3Config
{
    ServiceURL = Environment.GetEnvironmentVariable("R2_ENDPOINT"), // https://<account-id>.r2.cloudflarestorage.com
    ForcePathStyle = true,
    AuthenticationRegion = "auto",
    RequestChecksumCalculation = Amazon.Runtime.RequestChecksumCalculation.WHEN_REQUIRED,
    ResponseChecksumValidation = Amazon.Runtime.ResponseChecksumValidation.WHEN_REQUIRED
};

builder.Services.AddSingleton(s3Config);

builder.Services.AddSingleton<IAmazonS3>(sp =>
    new AmazonS3Client(
        Environment.GetEnvironmentVariable("R2_ACCESS_KEY_ID"),
        Environment.GetEnvironmentVariable("R2_SECRET_ACCESS_KEY"),
        s3Config));
        
builder.Services.AddSingleton<BucketService>()
;



builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICaseStudyRepository, CaseStudyRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
builder.Services.AddScoped<ISkillDomainReposirtory, SkillDomainRepository>();
builder.Services.AddScoped<IProjectBriefRepository, ProjectBriefRepository>();
builder.Services.AddHttpClient<IEmailService, EmailService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath        = "/auth/login";
        options.LogoutPath       = "/auth/logout";
        options.AccessDeniedPath = "/auth/denied";
 
        options.ExpireTimeSpan    = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
 
        // Harden the cookie
        options.Cookie.HttpOnly   = true;
        options.Cookie.SameSite   = SameSiteMode.Strict;
        options.Cookie.Name       = "__admin_session";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
}); 

var app = builder.Build();

if (args.Contains("--apply-migrations"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();

    db.Database.Migrate();
    Console.WriteLine("Migrations applied successfully.");
}

app.UseForwardedHeaders(); 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //BUCKET SETUP 
    var bucketService = services.GetRequiredService<BucketService>();
    await bucketService.CreateBucketAsync();

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
