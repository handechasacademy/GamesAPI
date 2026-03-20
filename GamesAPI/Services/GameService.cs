using GamesAPI.Models;
using GamesAPI.Models.Enums;
using GamesAPI.DTOs;
using GamesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using GamesAPI.Exceptions;

namespace GamesAPI.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;
        private readonly HybridCache _cache;

        public GameService(AppDbContext context, HybridCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<GameResponse>> GetGamesAsync()
        {
            await Task.Delay(50);
            return await _context.Games.Select(g => new GameResponse
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
            )).ToListAsync();
        }

        public async Task<PagedResponse<GameResponse>> GetPagedGamesAsync(int page, int pageSize, GameFilter filter)
        {
            await Task.Delay(50);
            

            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Genre))
                query = query.Where(g => g.Genre.Name == filter.Genre);
            if (filter.PlayingType.HasValue)
                query = query.Where(g => g.PlayingType == filter.PlayingType.Value);
            if (filter.GamePlatform.HasValue)
                query = query.Where(g => g.GamePlatform == filter.GamePlatform.Value);
            if (!string.IsNullOrEmpty(filter.SearchTerm))
                query = query.Where(g => g.Name.Contains(filter.SearchTerm));

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new GameResponse
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
                )).ToListAsync();

            var meta = new PaginationMeta(page, pageSize, totalPages, totalCount, page < totalPages, page > 1);

            return new PagedResponse<GameResponse>(items, meta);

        }

        public async Task<GameResponse?> GetGameByIdAsync(int id)
        {
            var cacheKey = $"game_{id}";

            var game = await _cache.GetOrCreateAsync(
                cacheKey,
                async cancel =>
                {
                    await Task.Delay(500, cancel);
                    return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
                }
                );           

            if (game == null)
            {
                throw new NotFoundException($"Game with ID {id} not found in the database.");
            }

            
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

        public async Task<GameResponse> CreateGameAsync(CreateGameRequest request)
        {
            await Task.Delay(20);
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
            await _context.SaveChangesAsync();

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

        public async Task<bool> UpdateGameAsync(int id, UpdateGameRequest request)
        {
            await Task.Delay(20);
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            { 
                throw new NotFoundException($"Game with ID {id} not found.");
            }

            game.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : game.Name;
            game.Publisher = !string.IsNullOrEmpty(request.Publisher) ? request.Publisher : game.Publisher;
            game.Price = request.Price != default ? request.Price : game.Price;
            game.ReleaseDate = request.ReleaseDate != default ? request.ReleaseDate : game.ReleaseDate;
            game.PlayingType = request.PlayingType != default ? request.PlayingType : game.PlayingType;
            game.GamePlatform = request.GamePlatform != default ? request.GamePlatform : game.GamePlatform;
            game.GenreId = request.GenreId != default ? request.GenreId : game.GenreId;
            game.UpdatedAt = DateTime.UtcNow;

            await _cache.RemoveAsync($"game_{id}");
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            await Task.Delay(20);
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                throw new NotFoundException($"Game with ID {id} not found.");
            }

            _context.Games.Remove(game);
            await _cache.RemoveAsync($"game_{id}");
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
