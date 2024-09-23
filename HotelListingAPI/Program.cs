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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("HostelListingDbConnectionStrings");
builder.Services.AddDbContext<HotelListingDbContext>(options =>{
    options.UseSqlServer(connectionstring);
});


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
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// study this configuration

app.UseCors("Allowall");

app.UseAuthorization();

app.MapControllers();


app.Run();
