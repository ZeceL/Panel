using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication4.Data;

var builder = WebApplication.CreateBuilder(args);

// Добавление контекста базы данных в контейнер служб
builder.Services.AddDbContext<WebApplication4Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication4Context")));

// Добавление служб Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Настройка обработки ошибок и статических файлов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Настройка маршрутов
app.MapRazorPages();

// Перенаправление корневого URL на страницу /Users/Index
app.MapGet("/", context =>
{
    context.Response.Redirect("/Users");
    return Task.CompletedTask;
});

app.Run();
