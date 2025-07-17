using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoryBuilderAPI.ApplicationServices.Interfaces;
using StoryBuilderAPI.Data;
using StoryBuilderAPI.Models.Domain;
using System.Text.RegularExpressions;

namespace StoryBuilderAPI.ApplicationServices;
public class StoryService : IStoryService
{
    private readonly StoryContext _context;
    private readonly ILogger<StoryService> _logger;

    public StoryService(StoryContext context, ILogger<StoryService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Story> CreateStoryAsync(string word)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentException("Word cannot be null or empty.", nameof(word));

            var story = new Story
            {
                Word = word,
                Content = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Story created successfully with word: {Word}", word);
            return story;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating story");
            throw;
        }
    }
    public async Task<List<Story>> GetPaginatedStoriesAsync(int page, int size)
    {
        try
        {
            return await _context.Stories
                .OrderByDescending(s => s.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving paginated stories");
            throw;
        }
    }
    public async Task<int> CountWordStoryAsync(string word, string storyText)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentException("Word cannot be null or empty.", nameof(word));

            if (string.IsNullOrWhiteSpace(storyText))
                throw new ArgumentException("Story text cannot be null or empty.", nameof(storyText));

            var regex = new Regex($@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase);
            int count = regex.Matches(storyText).Count;

            _logger.LogInformation("Word '{Word}' occurred {Count} times", word, count);
            return await Task.FromResult(count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while counting word in story");
            throw;
        }
    }
    public async Task<Story> UploadImageAsync(int storyId, IFormFile file)
    {
        try
        {
            var story = await _context.Stories.FindAsync(storyId);
            if (story == null)
                throw new Exception("Story not found");

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            story.ImageFileName = file.FileName;
            story.ImageSize = file.Length;
            story.ImageContentType = file.ContentType;
            story.UploadedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Image uploaded successfully for story ID: {StoryId}", storyId);
            return story;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while uploading image for story ID: {StoryId}", storyId);
            throw;
        }
    }
}
