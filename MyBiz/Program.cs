using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBiz.Areas.Manage.Services;
using MyBiz.DAL;
using MyBiz.Models;
using MyBiz.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{

    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = false;

    opt.User.RequireUniqueEmail = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<ManageLayoutService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});


app.Run();
