using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Logging Useing Serilog(Seq)

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5258/")
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

#region DB Context

var connectionString = builder.Configuration.GetConnectionString("PersianConnectionString");

builder.Services.AddDbContext<ParsianNewsDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#region Identity

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ParsianNewsDbContext>();

#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapRazorPages();

app.Run();
