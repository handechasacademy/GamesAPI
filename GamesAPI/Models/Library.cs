namespace GamesAPI.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<GameLibrary> GameLibraries { get; set; } = new List<GameLibrary>();
    }
}