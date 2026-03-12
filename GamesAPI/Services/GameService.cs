using GamesAPI.Models;
using GamesAPI.Models.Enums;
using GamesAPI.DTOs;
using GamesAPI.Data;

namespace GamesAPI.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GameResponse> GetGames()
        {
            return _context.Games.Select(g => new GameResponse
            (
                g.Id,
                g.Name,
                g.ReleaseDate,
                g.Publisher,
                g.Price,
                g.CreatedAt,
                g.UpdatedAt,
                g.PlayingType,
                g.GamePlatform,
                g.GenreId
            )).ToList();
        }

        public GameResponse? GetGameById(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null) return null;

            return new GameResponse
            (
                game.Id,
                game.Name,
                game.ReleaseDate,
                game.Publisher,
                game.Price,
                game.CreatedAt,
                game.UpdatedAt,
                game.PlayingType,
                game.GamePlatform,
                game.GenreId
            );
        }

        public GameResponse CreateGame(CreateGameRequest request)
        {
            var newGame = new Game
            {
                Name = request.Name,
                Publisher = request.Publisher,
                Price = request.Price,
                ReleaseDate = request.ReleaseDate,
                PlayingType = request.PlayingType,
                GamePlatform = request.GamePlatform,
                GenreId = request.GenreId
            };

            _context.Games.Add(newGame);
            _context.SaveChanges();

            return new GameResponse
            (
                newGame.Id,
                newGame.Name,
                newGame.ReleaseDate,
                newGame.Publisher,
                newGame.Price,
                newGame.CreatedAt,
                newGame.UpdatedAt,
                newGame.PlayingType,
                newGame.GamePlatform,
                newGame.GenreId
            );
        }

        public GameResponse? UpdateGame(int id, UpdateGameRequest request)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null) return null;

            game.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : game.Name;
            game.Publisher = !string.IsNullOrEmpty(request.Publisher) ? request.Publisher : game.Publisher;
            game.Price = request.Price != default ? request.Price : game.Price;
            game.ReleaseDate = request.ReleaseDate != default ? request.ReleaseDate : game.ReleaseDate;
            game.PlayingType = request.PlayingType != default ? request.PlayingType : game.PlayingType;
            game.GamePlatform = request.GamePlatform != default ? request.GamePlatform : game.GamePlatform;
            game.GenreId = request.GenreId != default ? request.GenreId : game.GenreId;
            game.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return new GameResponse
            (
                game.Id,
                game.Name,
                game.ReleaseDate,
                game.Publisher,
                game.Price,
                game.CreatedAt,
                game.UpdatedAt,
                game.PlayingType,
                game.GamePlatform,
                game.GenreId
            );
        }

        public GameResponse? DeleteGame(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null) return null;

            _context.Games.Remove(game);
            _context.SaveChanges();

            return new GameResponse
            (
                game.Id,
                game.Name,
                game.ReleaseDate,
                game.Publisher,
                game.Price,
                game.CreatedAt,
                game.UpdatedAt,
                game.PlayingType,
                game.GamePlatform,
                game.GenreId
            );
        }
    }
}
