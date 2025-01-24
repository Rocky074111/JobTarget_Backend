using Newtonsoft.Json;

namespace api.Models
{
    public class Location
    {
        public string City { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
    }
}