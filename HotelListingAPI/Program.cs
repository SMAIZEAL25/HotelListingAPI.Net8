using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using AutoMapper;
using HotelListingAPI.Contract;
using HotelListingAPI.Respository;
using HotelListingAPI.DTO.HotelDTO;
using Microsoft.AspNetCore.Identity;
using HotelListingAPI.Model;
using HotelListingAPI.AuthManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelListingAPI.Middleware;
using Asp.Versioning;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("HostelListingDbConnectionStrings");
builder.Services.AddDbContext<HotelListingDbContext>(options =>
{
    options.UseSqlServer(connectionstring);
});

builder.Services.AddIdentityCore<APIUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<APIUser>>("HostelListingApi")
    .AddEntityFrameworkStores<HotelListingDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Stands for Cross origin resoucres sharing study IOC Container // AllowAll is the name of the policy here 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod());
});

// Configuration for Serilog 
// ctx stands for context variable lc stands for logger configuration   

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
var app = builder.Build();

// AtuMapper Config 
builder.Services.AddAutoMapper(typeof(Mapper));

// Repository 
builder.Services.AddScoped(typeof(IGenericRespository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRespository, CountriesRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();




// JWT TOKEN FOR LOGIN AUTHENTICATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Bearer
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,  // Validation for the bearer and the issuser
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))

    };

});



// This is service is to add Identity users roles for our JWT Token Using entity Frame work store 
builder.Services.AddIdentityCore<APIUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HotelListingDbContext>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Api Versioning 
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion =Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("Ver")
        );
});

builder.Services.AddVersionedApiExplorer(
    Options =>
    {
        Options.GroupNameFormat = "'v'vvv";
        Options.SubstituteApiVersionInUrl = true;
    });


// the middleware that managers the Global exception handlers inside the exception folder 
app.UseMiddleware<ExceptionMIddleWare>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// study this configuration

app.UseCors("Allowall");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();


app.Run();
