using APP.Domain;
using APP.Models;
using APP.Services;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// IoC Container:
builder.Services.AddDbContext<DbContext, Db>(options => options.UseSqlite(builder.Configuration.GetConnectionString(nameof(Db))));

builder.Services.AddScoped<IService<CategoryRequest, CategoryResponse>, CategoryService>();
builder.Services.AddScoped<IService<StoreRequest, StoreResponse>, StoreService>();
builder.Services.AddScoped<IService<ProductRequest, ProductResponse>, ProductService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
