using Newtonsoft.Json;
// using System.Text.Json.Serialization;

namespace api.Models
{
    public class Job
    {
        public int? Id { get; set; }
        [JsonProperty("req_name")]
        // [JsonPropertyName("req_name")]
        public string ReqName { get; set; } = String.Empty;
        public Location? Location { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public List<Post> Postings { get; set; } = new List<Post>();  // List of job postings
    }    
}
