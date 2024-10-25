using AplicationLayer;
using AplicationLayer.GenericUseCases;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Presenters.SaleViewModel;
using Repository;
using Repository.ExternalServices;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agrega las configuraciones desde los archivos appsettings
ConfigureConfiguration(builder.Configuration, builder.Environment);

// Configuración de JWT
ConfigureJwtAuthentication(builder.Services, builder.Configuration);

// Determina si estás en un entorno de desarrollo
var isDevelopment = builder.Environment.IsDevelopment();

// Obtiene la cadena de conexión basada en el entorno
var connectionString = GetConnectionString(builder.Configuration, isDevelopment);

// Validaciones
ConfigureValidations(builder.Services);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Generar los endpoints para la gestion de sessiones
builder.Services.AddIdentityApiEndpoints<UserModel>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Servicios externos
builder.Services.AddHttpClient<IReniecService, ReniecServices>();
builder.Services.AddScoped<IReniecService, ReniecServices>();

// Nuevas API'S
ConfigureNewApis(builder.Services);

// Viejas API'S
ConfigureOldApis(builder.Services);

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

app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Ajusta la llamada a MapIdentityApi
app.MapIdentityApi<UserModel>();

app.Run();

void ConfigureConfiguration(IConfigurationBuilder configuration, IWebHostEnvironment environment)
{
    configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}

void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("JwtSettings");
    var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? "");

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
}

string GetConnectionString(IConfiguration configuration, bool isDevelopment)
{
    return isDevelopment ?
        configuration.GetSection("ConnectionStrings")["idenitycs"] :
        Environment.GetEnvironmentVariable("CONNECTION_STRING");
}

void ConfigureValidations(IServiceCollection services)
{
    services.AddValidatorsFromAssemblyContaining<CartValidator>();
    services.AddFluentValidationAutoValidation();
    services.AddFluentValidationClientsideAdapters();
}

void ConfigureNewApis(IServiceCollection services)
{
    services.AddScoped<IMapper<CartRequestDTO, Cart>, CartMapper>();
    services.AddScoped<IRepository<Cart>, CartRepository>();
    services.AddScoped<IRepositorySearch<CartModel, Cart>, CartRepository>();
    services.AddScoped<IPresenter<Cart, CartDetailViewModel>, CartDetailPresenter>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<GenerateOrderThroughCartUseCase>();
    services.AddScoped<GetCartSearchUseCase<CartModel>>();
    services.AddScoped<AddCartUseCase<CartRequestDTO>>();
    services.AddScoped<GetCartUseCase<Cart, CartDetailViewModel>>();
    services.AddScoped<UpdateCartUseCase<CartRequestDTO>>();
    services.AddScoped<DeleteCartUseCase>();

    services.AddScoped<IPeopleRepository, PeopleRepository>();

    services.AddScoped<IMapper<GenereteOrderPerWorkerDTO, Order>, OrderMapper>();
    services.AddScoped<IRepository<Order>, OrderRepository>();
    services.AddScoped<IOrderDetailPresenter<OrderViewModel>, OrderPresenter>();
    services.AddScoped<IPresenter<Order, OrdersViewModel>, OrdersPresenter>();
    services.AddScoped<IRepositorySearch<OrderModel, Order>, OrderRepository>();
    services.AddScoped<GetEntitiesSearchUseCase<OrderModel, Order, OrdersViewModel>>();
    services.AddScoped<CreateOrderForCustomerUseCase<GenereteOrderPerWorkerDTO>>();
    services.AddScoped<GetOrderDetailByIdUseCase<OrderViewModel>>();
    services.AddScoped<GetAllEntitiesUseCase<Order, OrdersViewModel>>();

    services.AddScoped<IProductRepository<Product>, ProductRepository>();
    services.AddScoped<IRepository<Product>, ProductRepository>();
    services.AddScoped<IRepositorySearch<ProductModel, Product>, ProductRepository>();

    services.AddScoped<IRepository<Status>, StatusRepository>();
    services.AddScoped<IPresenter<Status, StatusViewModel>, StatusPresenter>();
    services.AddScoped<GetAllEntitiesUseCase<Status, StatusViewModel>>();
}

void ConfigureOldApis(IServiceCollection services)
{
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IProduct, ProductService>();
    services.AddScoped<IProductCategory, ProductCategoryService>();
    services.AddScoped<IProductBrand, ProductBrandService>();
    services.AddScoped<IState, StateService>();
    services.AddScoped<IEntry, EntryService>();
    services.AddScoped<ILoss, LossService>();
    services.AddScoped<ILossReason, LossReasonService>();
    services.AddScoped<IProductPhoto, ProductPhotoService>();
    services.AddScoped<IProductSpecification, ProductSpecificationService>();
    services.AddScoped<IBatch, BatchService>();
}
