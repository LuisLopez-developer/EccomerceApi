using AplicationLayer;
using AplicationLayer.Sale;
using Data;
using EccomerceApi.Interfaces;
using EccomerceApi.Interfaces.ProductIntefaces;
using EccomerceApi.Interfaces.ProductInterfaces;
using EccomerceApi.Middlewares;
using EccomerceApi.Services;
using EccomerceApi.Services.ProductServices;
using EccomerceApi.Validators;
using EnterpriseLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mappers;
using Mappers.Dtos.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models;
using Presenters.SaleViewModel;
using Repository;
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

// Validaciones
builder.Services.AddValidatorsFromAssemblyContaining<CartValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Generar los endpoints para la gestion de sessiones
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Nuevas API'S
builder.Services.AddScoped<IMapper<CartRequestDTO, Cart>, CartMapper>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IRepositorySearch<CartModel, Cart>, CartRepository>();
builder.Services.AddScoped<IPresenter<Cart, CartDetailViewModel>, CartDetailPresenter>();
builder.Services.AddScoped<GetCartSearchUseCase<CartModel>>();
builder.Services.AddScoped<AddCartUseCase<CartRequestDTO>>();
builder.Services.AddScoped<GetCartUseCase<Cart, CartDetailViewModel>>();
builder.Services.AddScoped<UpdateCartUseCase<CartRequestDTO>>();
builder.Services.AddScoped<DeleteCartUseCase>();

// Viejas API'S
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

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
