using Newtonsoft.Json;

namespace api.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Sitename { get; set; } = String.Empty;
        public int Duration { get; set; }
    }

}