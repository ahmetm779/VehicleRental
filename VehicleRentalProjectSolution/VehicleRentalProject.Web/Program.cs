
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using VehicleRentalProject.Models;
using VehicleRentalProject.Repositories;
using VehicleRentalProject.Repositories.DataSeeding;
using VehicleRentalProject.Repositories.Implementation;
using VehicleRentalProject.Repositories.Infrastructure;
using VehicleRentalProject.Web.CustomMiddleWare;
using VehicleRentalProject.Web.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<CarContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailSender, IEmailSender>();
//builder.Services.AddAutoMapper(typeof(VehicleRepository));

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new VehicleProfile(builder.Environment));
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "VehicleProjectCookie";
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.LogoutPath = $"/Identity/Account/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

//app.UseHttpsRedirection();
app.UseStaticFiles();
DataSeeding();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

     endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Vehicles}/{action=Index}/{id?}"
         );
 });
#pragma warning restore ASP0014 // Suggest using top level route registrations




app.MapRazorPages();
app.Run();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitial = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitial.Initialize();
    }
}