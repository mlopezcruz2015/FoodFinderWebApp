using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodFinderWebApp.Data;
using FoodFinderWebApp.Models;
using Google.Maps;
using System.Net;
using Newtonsoft.Json;

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

//Get latlong from zip:

HttpWebRequest zipRequest = (HttpWebRequest)WebRequest.Create("https://thezipcodes.com/api/v1/search?zipCode=33463&countryCode=US&apiKey=1e77aaee2bd54aafa23b46c9f14cc543");
var zipResponse = zipRequest.GetResponse();
var tempStr = zipResponse.GetResponseStream();
StreamReader reader = new StreamReader(tempStr);
var anotherTemp = reader.ReadToEnd();
var zipInfo = JsonConvert.DeserializeObject<ZIPRoot>(anotherTemp);

//Set up Google API
GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAnl6DeaeqQAMLAOthZjCHlaBeppbig998"));


Google.Maps.Places.PlacesService temp = new Google.Maps.Places.PlacesService();
var request = new Google.Maps.Places.NearbySearchRequest();

request.Location = new LatLng(Convert.ToDecimal(zipInfo.location[0].latitude), Convert.ToDecimal(zipInfo.location[0].longitude));
request.Radius = 5000;
request.Keyword = "Food";
var temp2 = temp.GetResponse(request);

app.Run();