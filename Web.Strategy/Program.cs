using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Strategy.Models;
using Web.Strategy.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStr"));
});

// IdentityUser' ý miras alacak bir class oluþturup extend ediyoruz.(Models>AppUser.cs)
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

// HttpContext'e eriþebilmemiz için AddHttpContextAccessor ekliyoruz.
builder.Services.AddHttpContextAccessor();

// Önce cookie'ye eriþmemiz gerekiyor.HttpContext'e ihtiyacýmýz var dolayýsýyla.
builder.Services.AddScoped<IProductRepository>(sp =>
{
    //GetService metodu eðer almaya çalýþtýðýmýz service yoksa geriye null döner.GetRequiredService metodu eðer almaya çalýþtýðýmýz service yoksa hata döner.
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var claims = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault();

    var context = sp.GetRequiredService<AppIdentityDbContext>();

    if (claims == null) return new ProductRepositoryFromSqlServer(context);

    var databaseType = (EDatabaseType)int.Parse(claims.Value);

    // Bir request esnasýnda herhangi bir classýn constructorýnda bu interface(IProductRepository) ile karþýlaþýldýðýnda bu interface i implement eden classlarýn hangisi olacaðýný dinamik olarak cookie de tutulan claim deðerine göre belirliyoruz.
    return databaseType switch
    {
        EDatabaseType.SqlServer => new ProductRepositoryFromSqlServer(context),
        EDatabaseType.MongoDb => new ProductRepositoryFromMongoDb(builder.Configuration),
        _ => throw new NotImplementedException()
    };
});

var app = builder.Build();

// Bu scope ile beraber AppIdentityDbContext'ten bir nesne örneði alýyoruz, iþimiz bittiði zaman memory'den düþmesi için.
using var scope = app.Services.CreateScope();

// ServiceProvider bize, eklenen servisleri almamýza imkan saðlýyor.
// ServiceProvider.GetService  -> Ýlgili nesneyi alabilirse alýr, alamazsa null döner.
// GetRequiredService          ->  Ýlgili nesneyi alabilirse alýr, alamazsa hata döner
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

// Kullanýcýlar ile ilgili iþlemler için UserManager, Rollerle ilgili iþlem için RoleManager, Login-Logout iþlemleri için SignInManager kullanýlýr.
// Biz yeni bir kullanýcý kaydetmek istiyoruz.
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

// Package Console' da Update-Database kodlarýný yazmamýza gerek kalmadan, uygulama ayaða kalktýðý anda Migration'larý otomatik uygular.
identityDbContext.Database.Migrate();

if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser { UserName = "Alper1", Email = "alperkaragoz@outlook.com", }, "Password123*").Wait();
    userManager.CreateAsync(new AppUser { UserName = "Alper2", Email = "alperkaragoz2@outlook.com", }, "Password123*").Wait();
    userManager.CreateAsync(new AppUser { UserName = "Alper3", Email = "alperkaragoz3@outlook.com", }, "Password123*").Wait();
    userManager.CreateAsync(new AppUser { UserName = "Alper4", Email = "alperkaragoz4@outlook.com", }, "Password123*").Wait();
    userManager.CreateAsync(new AppUser { UserName = "Alper5", Email = "alperkaragoz5@outlook.com", }, "Password123*").Wait();
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
