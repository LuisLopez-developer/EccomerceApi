using EccomerceApi.Data;
using EccomerceApi.Entity;
using EccomerceApi.Interfaces;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Interfaces.ProductInterfaces;
using EccomerceApi.Services;
using EccomerceApi.Services.ProductServices;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Agrega las configuraciones desde los archivos appsettings
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Determina si estás en un entorno de desarrollo
var isDevelopment = builder.Environment.IsDevelopment();

// Obtiene la cadena de conexión basada en el entorno
var connectionString = isDevelopment ?
    builder.Configuration.GetSection("ConnectionStrings")["idenitycs"] :
    Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Add services to the container.
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

// Generar los endpoints para la gestion de sessiones
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IProductCategory, ProductCategoryService>();
builder.Services.AddScoped<IProductBrand, ProductBrandService>();
builder.Services.AddScoped<IState, StateService>();
builder.Services.AddScoped<IEntry, EntryService>();
builder.Services.AddScoped<ILoss, LossService>();
builder.Services.AddScoped<ILossReason, LossReasonService>();
builder.Services.AddScoped<IProductPhoto, ProductPhotoService>();
builder.Services.AddScoped<IProductSpecification, ProductSpecificationService>();
builder.Services.AddScoped<IBatch, BatchService>();


builder.Services.AddScoped<ICloudflare, CloudflareService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(
                builder.Configuration.GetSection("Url")["FrontendUrlAdmin"] ?? "https://localhost:7033", // Enlace para el panel administrativo del eccomerce
                builder.Configuration.GetSection("Url")["FrontendUrlUser"] ?? "https://localhost:7040" // Enlace para la página web del Eccomerce
            )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Content-Length", "X-Custom-Header")
        .AllowCredentials();
    });
});

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// Logging para errores no controlados
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (errorFeature != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(errorFeature.Error, "Unhandled exception");
            await context.Response.WriteAsync(new
            {
                error = errorFeature.Error.Message
            }.ToString());
        }
    });
});

// Aquí añadimos la inicialización de migraciones
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<IdentityDbContext>();
//        context.Database.Migrate(); // Aplica migraciones
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones.");
//    }
//}

app.Run();
