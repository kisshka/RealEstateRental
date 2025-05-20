using Microsoft.EntityFrameworkCore;
using Real_Estate_Rental_Practic.Models.Data;
using Microsoft.AspNetCore.Identity;
using Real_Estate_Rental_Practic.Data;
using Real_Estate_Rental_Practic.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RealEstateRentalContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<Real_Estate_Rental_PracticIdentityContext>(options =>
    options.UseSqlServer(connectionString));

//Настройка политики пароля
builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;
    })
        .AddRoles<IdentityRole>()
        .AddErrorDescriber<CustomIdentityErrorDescriber>()
        .AddEntityFrameworkStores<Real_Estate_Rental_PracticIdentityContext>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        await SeedData.InitializeAsync(services);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Ошибка при инициализации данных: {ex.Message}");
//    }
//}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EstateObjects}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
