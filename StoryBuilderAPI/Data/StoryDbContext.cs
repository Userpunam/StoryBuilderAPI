using Microsoft.EntityFrameworkCore;
using StoryBuilderAPI.Models.Domain;


namespace StoryBuilderAPI.Data
{
    public class StoryContext : DbContext
    {
        public StoryContext(DbContextOptions<StoryContext> options) : base(options) { }
        public DbSet<Story> Stories { get; set; }
    }
}