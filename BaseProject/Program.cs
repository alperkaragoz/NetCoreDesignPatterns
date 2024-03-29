using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStr"));
});

// IdentityUser' � miras alacak bir class olu�turup extend ediyoruz.(Models>AppUser.cs)
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();


var app = builder.Build();

// Bu scope ile beraber AppIdentityDbContext'ten bir nesne �rne�i al�yoruz, i�imiz bitti�i zaman memory'den d��mesi i�in.
using var scope = app.Services.CreateScope();

// ServiceProvider bize, eklenen servisleri almam�za imkan sa�l�yor.
// ServiceProvider.GetService  -> �lgili nesneyi alabilirse al�r, alamazsa null d�ner.
// GetRequiredService          ->  �lgili nesneyi alabilirse al�r, alamazsa hata d�ner
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

// Kullan�c�lar ile ilgili i�lemler i�in UserManager, Rollerle ilgili i�lem i�in RoleManager, Login-Logout i�lemleri i�in SignInManager kullan�l�r.
// Biz yeni bir kullan�c� kaydetmek istiyoruz.
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

// Package Console' da Add-Migration,Update-Database kodlar�n� yazmam�za gerek kalmadan, uygulama aya�a kalkt��� anda Migration'lar� otomatik uygular.
identityDbContext.Database.Migrate();

if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser { UserName = "Alper1", Email = "alperkaragoz@outlook.com", }, "Password123*" ).Wait();
userManager.CreateAsync(new AppUser { UserName = "Alper2", Email = "alperkaragoz2@outlook.com", }, "Password123*" ).Wait();
userManager.CreateAsync(new AppUser { UserName = "Alper3", Email = "alperkaragoz3@outlook.com", }, "Password123*" ).Wait();
userManager.CreateAsync(new AppUser { UserName = "Alper4", Email = "alperkaragoz4@outlook.com", }, "Password123*" ).Wait();
userManager.CreateAsync(new AppUser { UserName = "Alper5", Email = "alperkaragoz5@outlook.com", }, "Password123*" ).Wait();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDeveloperExceptionPage();

app.UseRouting();
// Authentication middleware'ini ekliyoruz.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
