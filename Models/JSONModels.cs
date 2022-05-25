namespace FoodFinderWebApp.Models
{
    public class JSONModels
    {
    }

    public class ZIPLocation
    {
        public string country { get; set; }
        public string countryCode2 { get; set; }
        public string countryCode3 { get; set; }
        public string state { get; set; }
        public string stateCode2 { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
    }

    public class ZIPRoot
    {
        public bool success { get; set; }
        public List<ZIPLocation> location { get; set; }
    }
}
