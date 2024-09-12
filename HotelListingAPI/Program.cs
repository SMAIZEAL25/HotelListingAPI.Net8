using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
// ctx stands for context variable lc stands for logger configuration 

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("Allowall");

app.UseAuthorization();

app.MapControllers();

app.Run();
