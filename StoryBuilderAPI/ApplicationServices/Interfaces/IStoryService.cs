using StoryBuilderAPI.Models.Domain;

namespace StoryBuilderAPI.ApplicationServices.Interfaces;
public interface IStoryService
{
    Task<Story> CreateStoryAsync(string word);
    Task<List<Story>> GetPaginatedStoriesAsync(int page, int size);
    Task<int> CountWordStoryAsync(string word, string storyText);
    Task<Story> UploadImageAsync(int storyId, IFormFile imageFile);  
}
