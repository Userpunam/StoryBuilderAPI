namespace StoryBuilderAPI.Models.Dtos;
public class CountWordRequestDto
{
    public string WordToFind { get; set; } = string.Empty;
    public string StoryText { get; set; } = string.Empty;
}
