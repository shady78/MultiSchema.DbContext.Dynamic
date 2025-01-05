using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication3.ActionFilteres;
using WebApplication3.Common;
using WebApplication3.Data;
using WebApplication3.Interfaces;
using WebApplication3.Middlewares;
using WebApplication3.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IGlobalParameterService, GlobalParameterService>();

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    );
    // إضافة هذا الخيار لتجنب caching للـ model
    options.EnableServiceProviderCaching(false);
});


// تسجيل الإعدادات العامة
builder.Services.Configure<GlobalSettings>(
    builder.Configuration.GetSection("GlobalSettings"));


// تسجيل خدمة البارامترات العامة
//builder.Services.AddScoped<IGlobalParameterService, GlobalParameterService>();

// إضافة Action Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalParameterActionFilter>();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// إضافة Middleware
app.UseMiddleware<GlobalParameterMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
