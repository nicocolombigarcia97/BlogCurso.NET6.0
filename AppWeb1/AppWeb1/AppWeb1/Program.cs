using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppWeb1.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BlogDatabase");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server =.\\SQLEXPRESS; Database = BlogDatabase; Trusted_Connection = True;"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Si se cumple lo que esta a la izquierda de "?" se le da el valor de Nicolas, sino lo que esta a 
// la derecha " : " .
var dev = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_USER")) ? "Nicolas"
                              : Environment.GetEnvironmentVariable("ASPNETCORE_USER");

builder.Configuration.AddJsonFile($"appsettings.{dev}.json");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}  
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
