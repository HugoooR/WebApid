using Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://+:8080");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://angular-product-wheat.vercel.app/products")  
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(
        builder.Configuration.GetConnectionString("ApplicationDbContext")
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("EnableSwagger") == "true")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
