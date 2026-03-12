
using GamesAPI.Models.Enums;

namespace GamesAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public PlayType PlayingType { get; set; }
        public Platform GamePlatform { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<GameLibrary> GameLibraries { get; set; } = new List<GameLibrary>();
    }
}
