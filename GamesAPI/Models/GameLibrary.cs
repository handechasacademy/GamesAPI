namespace GamesAPI.Models
{
    public class GameLibrary
    {
        public bool IsFavourite { get; set; }

        public decimal HoursPlayed { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
