using Newtonsoft.Json;

public class JobDTO {
    public List<PostDTO> Postings { get; set; } = new List<PostDTO>();
    public int Id { get; set; }    
    
    [JsonProperty("req_name")]
    public string? ReqName { get; set; }
    public LocationDTO? Location { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
}