using Microsoft.EntityFrameworkCore;
using ProyekRentCar.Models;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan konfigurasi untuk DbContext
builder.Services.AddDbContext<RentCarsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentCarDatabase")));

// Tambahkan konfigurasi session
builder.Services.AddSession();

// Tambahkan konfigurasi untuk IHttpContextAccessor agar bisa digunakan di seluruh aplikasi
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Tambahkan services lainnya
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Tambahkan middleware session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
