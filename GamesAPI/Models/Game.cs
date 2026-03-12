
using System.ComponentModel.DataAnnotations;
using GamesAPI.Models.Enums;

namespace GamesAPI.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }

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
