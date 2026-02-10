using Serilog;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Middleware;
using ProductManagement.Repository;
using ProductManagement.Services;
using ProductManagement.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        retainedFileCountLimit: 7)
    .CreateLogger();

try
{
    Log.Information("🚀 Starting ProductManagement API...");

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog();

    // Database Context
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Repositories and Services
    builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
    builder.Services.AddScoped<IProductRepo, ProductRepo>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();

    builder.Services.AddControllers();

    // ⭐ CORS Configuration - ADD THIS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp", policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    });

    // JWT Authentication
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"] ?? "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256";
    var key = Encoding.ASCII.GetBytes(secretKey);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Middleware
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    app.UseSerilogRequestLogging();

    // ⭐ CORS - MUST BE HERE, BEFORE Authentication
    app.UseCors("AllowAngularApp");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("✅ ProductManagement API started successfully");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "💥 Application failed to start");
}
finally
{
    Log.Information("🛑 Shutting down ProductManagement API...");
    Log.CloseAndFlush();
}