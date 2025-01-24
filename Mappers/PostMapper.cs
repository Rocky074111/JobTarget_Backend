using api.Models;

public static class PostMapper
{
    public static Post ToPost(PostDTO postDTO)
    {
        return new Post
        {
            Id = postDTO.Id,
            Sitename = postDTO.Sitename ?? "",
            Duration = postDTO.Duration
        };
    }

    public static PostDTO ToPostDTO(Post post)
    {
        return new PostDTO
        {
            Id = post.Id,
            Sitename = post.Sitename,
            Duration = post.Duration
        };
    }
}