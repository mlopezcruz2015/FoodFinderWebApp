using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodFinderWebApp.Data;
using FoodFinderWebApp.Models;
using Google.Maps;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FoodFinderWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodFinderWebAppContext") ?? throw new InvalidOperationException("Connection string 'FoodFinderWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Set up Google API
GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAnl6DeaeqQAMLAOthZjCHlaBeppbig998"));


Google.Maps.Places.PlacesService temp = new Google.Maps.Places.PlacesService();
var request = new Google.Maps.Places.NearbySearchRequest();
request.Location = new LatLng(26.593694196025627, -80.14389127523728);
request.Radius = 500;
request.Keyword = "Food";
var temp2 = temp.GetResponse(request);

app.Run();
