using CBApp.Data;
using CBApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add ApplicationDbContext
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
// Dependency injection for Questions DB context class
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbContext")
       )
    ); // AddDbContext creates a DbContextOptions object with settings for database server/connection string


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Configure for session state usage between Http requests
builder.Services.AddMemoryCache();
// Configure session state settings
builder.Services.AddSession(options =>
{
    // Change timeout from default (20 mins) to 1 hour
    options.IdleTimeout = TimeSpan.FromSeconds(60 * 60);
    // Allows client-side scripts to access the cookie (for JS/view model interaction to display error messages)
    options.Cookie.HttpOnly = false; // default is 'true'
    // Make cookies essential (cookie is required for app to function/transfer form data between http requests)
    options.Cookie.IsEssential = true;
});


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
// Add NewtonsoftJson allows serialization of JSON objects in session storage
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
// IMPORANT CODE FIX: https://www.google.com/search?q=System.InvalidOperationException%3A+%27Unable+to+find+the+required+services.+Please+add+all+the+required+services+by+calling+%27IServiceCollection.AddRazorPages%27+inside+the+call+to+%27ConfigureServices(...)%27+in+the+application+startup+code.%27&oq=System.InvalidOperationException%3A+%27Unable+to+find+the+required+services.+Please+add+all+the+required+services+by+calling+%27IServiceCollection.AddRazorPages%27+inside+the+call+to+%27ConfigureServices(...)%27+in+the+application+startup+code.%27&aqs=chrome.0.69i59l4.188j0j7&sourceid=chrome&ie=UTF-8#fpstate=ive&vld=cid:b7e1b052,vid:uwW4uQXojVI
builder.Services.AddRazorPages(); // !!! doesn't work without this --> lots of runtime errors

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

// Congifuring sessions --> must be called before UseEndpoints();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapRazorPages();

app.Run();
