namespace StoryBuilderAPI.Models.Domain;
public class Story
{
    public int Id { get; set; }
    public string Word { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? ImageFileName { get; set; }
    public string? ImageContentType { get; set; }
    public long? ImageSize { get; set; }
    public DateTime? UploadedAt { get; set; }
}
