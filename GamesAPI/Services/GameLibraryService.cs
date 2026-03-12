using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;

namespace GamesAPI.Services
{
    public class GameLibraryService : IGameLibraryService
    {
        private readonly AppDbContext _context;
        public GameLibraryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GameLibraryResponse> GetGameLibraries()
        {
            return _context.GameLibraries.Select(gl => new GameLibraryResponse
            (
                gl.IsFavourite,
                gl.HoursPlayed,
                gl.GameId,
                gl.LibraryId
            )).ToList();
        }

        public GameLibraryResponse GetGameLibraryById(int gameId, int libraryId)
        {
            var gameLibrary = _context.GameLibraries.FirstOrDefault(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null) return null;

            return new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );
        }

        public GameLibraryResponse CreateGameLibrary(CreateGameLibraryRequest request)
        {
            var gameLibrary = new GameLibrary
            {
                IsFavourite = request.IsFavourite,
                HoursPlayed = request.HoursPlayed,
                GameId = request.GameId,
                LibraryId = request.LibraryId
            };

            _context.GameLibraries.Add(gameLibrary);
            _context.SaveChanges();

            return new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );
        }

        public GameLibraryResponse? UpdateGameLibrary(int gameId, int libraryId, UpdateGameLibraryRequest request)
        {
            var gameLibrary = _context.GameLibraries.FirstOrDefault(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null) return null;

            gameLibrary.IsFavourite = request.IsFavourite;
            gameLibrary.HoursPlayed = request.HoursPlayed;
            gameLibrary.GameId = gameId;
            gameLibrary.LibraryId = libraryId;

            _context.SaveChanges();

            return new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );
        }

        public GameLibraryResponse? DeleteGameLibrary(int gameId, int libraryId)
        {
            var gameLibrary = _context.GameLibraries.FirstOrDefault(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null) return null;

            _context.GameLibraries.Remove(gameLibrary);
            _context.SaveChanges();

            return new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );
        }
    }
}
