using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Models
{
    public class Library
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<GameLibrary> GameLibraries { get; set; } = new List<GameLibrary>();
    }
}