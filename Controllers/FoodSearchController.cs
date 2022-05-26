using FoodFinderWebApp.Models;
using Google.Maps;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Encodings.Web;

namespace FoodFinderWebApp.Controllers
{
    public class FoodSearchController : Controller
    {
        public IActionResult Index(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                searchString = "33463";

            //Get latlong from zip:
            HttpWebRequest zipRequest = (HttpWebRequest)WebRequest.Create("https://thezipcodes.com/api/v1/search?zipCode="+ searchString + "&countryCode=US&apiKey=1e77aaee2bd54aafa23b46c9f14cc543");
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

            var foodLocationsList = new List<FoodLocation>();

            foreach (var item in temp2.Results)
            {
                foodLocationsList.Add(new FoodLocation()
                {
                    Address = !string.IsNullOrWhiteSpace(item.FormattedAddress) ? item.FormattedAddress : "Not Found",
                    Category = item.Types[0].ToString(),
                    Name = item.Name
                });
            }

            var foodCategoryVM = new FoodLocationResultsViewModel
            {
                //Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                FoodLocations = foodLocationsList
            };

            return View(foodCategoryVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: search for " + searchString;
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
