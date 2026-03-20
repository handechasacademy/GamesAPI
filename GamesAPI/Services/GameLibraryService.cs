using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class GameLibraryService : IGameLibraryService
    {
        private readonly AppDbContext _context;
        public GameLibraryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameLibraryResponse>> GetGameLibrariesAsync()
        {
            await Task.Delay(20);
            return await _context.GameLibraries.Select(gl => new GameLibraryResponse
            (
                gl.IsFavourite,
                gl.HoursPlayed,
                gl.GameId,
                gl.LibraryId
            )).ToListAsync();
        }

        public async Task<PagedResponse<GameLibraryResponse>> GetPagedGameLibrariesAsync(int page, int pageSize, GameLibraryFilter filter)
        {
            await Task.Delay(20);

            var query = _context.GameLibraries.AsQueryable();
            if (filter.IsFavourite.HasValue)
            {
                query = query.Where(gl => gl.IsFavourite == filter.IsFavourite.Value);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(gl => new GameLibraryResponse
                (
                    gl.IsFavourite,
                    gl.HoursPlayed,
                    gl.GameId,
                    gl.LibraryId
                )).ToListAsync();

            var meta = new PaginationMeta(page, pageSize, totalPages, totalCount, page < totalPages, page > 1);
            return new PagedResponse<GameLibraryResponse>(items, meta);
        }

        public async Task<GameLibraryResponse?> GetGameLibraryByIdAsync(int gameId, int libraryId)
        {
            var gameLibrary = await _context.GameLibraries.FirstOrDefaultAsync(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null)
            {
                throw new NotFoundException($"GameLibrary with GameId {gameId} and LibraryId {libraryId} not found.");
            }

            await Task.Delay(20);
            return new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );
        }

        public async Task<GameLibraryResponse> CreateGameLibraryAsync(CreateGameLibraryRequest request)
        {
            await Task.Delay(20);
            var exists = await _context.GameLibraries
                            .AnyAsync(gl => gl.GameId == request.GameId && gl.LibraryId == request.LibraryId);

            if (exists)
            {
                throw new BadRequestException($"Game {request.GameId} is already in library {request.LibraryId}.");
            }

            var gameLibrary = new GameLibrary
            {
                IsFavourite = request.IsFavourite,
                HoursPlayed = request.HoursPlayed,
                GameId = request.GameId,
                LibraryId = request.LibraryId
            };

            _context.GameLibraries.Add(gameLibrary);
            await _context.SaveChangesAsync();

            return new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );
        }

        public async Task<bool> UpdateGameLibraryAsync(int gameId, int libraryId, UpdateGameLibraryRequest request)
        {
            var gameLibrary = await _context.GameLibraries.FirstOrDefaultAsync(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null)
            {
                throw new NotFoundException($"GameLibrary with GameId {gameId} and LibraryId {libraryId} not found.");
            }

            gameLibrary.IsFavourite = request.IsFavourite;
            gameLibrary.HoursPlayed = request.HoursPlayed;
            gameLibrary.GameId = gameId;
            gameLibrary.LibraryId = libraryId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGameLibraryAsync(int gameId, int libraryId)
        {
            var gameLibrary = await _context.GameLibraries.FirstOrDefaultAsync(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null)
            {
                throw new NotFoundException($"GameLibrary with GameId {gameId} and LibraryId {libraryId} not found.");
            }

            _context.GameLibraries.Remove(gameLibrary);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
