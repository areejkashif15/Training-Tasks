using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Middleware;
using ProductManagement.Repository;
using ProductManagement.Services;
using ProductManagement.Interface;



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



// try{
//     Log.Information("🚀 Starting ProductManagement API...");

//     var builder = WebApplication.CreateBuilder(args);

//     builder.Host.UseSerilog();

//     // Add services to the container.
//     // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//     builder.Services.AddControllers();

//     builder.Services.AddDbContext<ApplicationDbContext>(options =>
//         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//     builder.Services.AddScoped<IProductRepo, ProductRepo>();
//     builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
//     builder.Services.AddScoped<IProductService, ProductService>();
//     builder.Services.AddScoped<ICategoryService, CategoryService>();

//     builder.Services.AddEndpointsApiExplorer();
//     builder.Services.AddSwaggerGen();

//     builder.Services.AddOpenApi();

//     var app = builder.Build();

//     app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

//     // Configure the HTTP request pipeline.
//     if (app.Environment.IsDevelopment())
//     {
//         app.UseSwagger();
//         app.UseSwaggerUI();
//     }

//     app.UseHttpsRedirection();
//     app.UseAuthorization();
//     app.MapControllers();

//     app.Run();
// }catch (Exception ex)
// {
//     Log.Fatal(ex, "Application start-up failed");
// }
// finally
// {
//     Log.CloseAndFlush();
// }


try
{
    Log.Information("🚀 Starting ProductManagement API...");

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);

        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
            options.LogTo(Console.WriteLine, LogLevel.Information);
        }
    });
    builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
    builder.Services.AddScoped<IProductRepo, ProductRepo>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
        };
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("✅ ProductManagement API started successfully");

    app.Run();
}
catch (Exception ex){
    Log.Fatal(ex, "💥 Application failed to start");
}
finally{
    Log.Information("🛑 Shutting down ProductManagement API...");
    Log.CloseAndFlush();
}