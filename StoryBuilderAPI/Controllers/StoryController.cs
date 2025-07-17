using Microsoft.AspNetCore.Mvc;
using StoryBuilderAPI.ApplicationServices.Interfaces;
using StoryBuilderAPI.Models.Dtos;

namespace StoryBuilderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        private readonly ILogger<StoryController> _logger;

        public StoryController(IStoryService storyService, ILogger<StoryController> logger)
        {
            _storyService = storyService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStory([FromBody] CreateStoryDto request)
        {
            try
            {
                var result = await _storyService.CreateStoryAsync(request.Word);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating story");
                return StatusCode(500, "An error occurred while creating the story.");
            }
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedStories([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            try
            {
                var result = await _storyService.GetPaginatedStoriesAsync(page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving stories");
                return StatusCode(500, "An error occurred while retrieving the stories.");
            }
        }

        [HttpPost("count-word")]
        public async Task<IActionResult> CountWord([FromQuery] string word, [FromBody] string text)
        {
            try
            {
                var count = await _storyService.CountWordStoryAsync(word, text);
                return Ok(new { Word = word, Count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting word in text");
                return StatusCode(500, "An error occurred while counting the word.");
            }
        }

        [HttpPost("upload-image/{storyId}")]
        public async Task<IActionResult> UploadImage(int storyId, IFormFile file)
        {
            try
            {
                var result = await _storyService.UploadImageAsync(storyId, file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image for story ID: {StoryId}", storyId);
                return StatusCode(500, "An error occurred while uploading the image.");
            }
        }
    }
}
