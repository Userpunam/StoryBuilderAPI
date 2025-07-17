namespace StoryBuilderAPI.Models.Dtos;
public class StoryDto
{
    public int Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string? ImageFilename { get; set; }
    public string? ImageContentType { get; set; }
    public long? ImageSize { get; set; }
    public DateTime? UploadedAt { get; set; }
}
