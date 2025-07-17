namespace StoryBuilderAPI.Models.Dtos;
public class PaginatedStoriesDto
{
    public List<StoryDto> Stories { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
