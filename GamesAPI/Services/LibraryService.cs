using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace GamesAPI.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _context;
        private readonly HybridCache _cache;
        public LibraryService(AppDbContext context, HybridCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<LibraryResponse>> GetLibrariesAsync()
        {
            await Task.Delay(20);
            return await _context.Libraries.Select(l => new LibraryResponse
            (
                l.Id,
                l.Name,
                l.Description,
                l.CreatedAt,
                l.UpdatedAt
            )).ToListAsync();
        }

        public async Task<LibraryResponse?> GetLibraryByIdAsync(int id)
        {
            var cacheKey = $"library_{id}";

            var library = await _cache.GetOrCreateAsync(
                cacheKey,
                async cancel =>
                {
                    await Task.Delay(200, cancel);
                    return await _context.Libraries.FirstOrDefaultAsync(l => l.Id == id);
                }
                );

            if (library == null) return null;

            return new LibraryResponse
            (
                library.Id,
                library.Name,
                library.Description,
                library.CreatedAt,
                library.UpdatedAt
            );
        }

        public async Task<LibraryResponse> CreateLibraryAsync(CreateLibraryRequest request)
        {
            await Task.Delay(20);
            var newLibrary = new Library
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Libraries.Add(newLibrary);
            await _context.SaveChangesAsync();

            return new LibraryResponse
            (
                newLibrary.Id,
                newLibrary.Name,
                newLibrary.Description,
                newLibrary.CreatedAt,
                newLibrary.UpdatedAt
            );
        }
        public async Task<bool> UpdateLibraryAsync(int id, UpdateLibraryRequest request)
        {
            await Task.Delay(20);
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == id);

            if (library == null) return false;

            library.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : library.Name;
            library.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : library.Description;
            library.UpdatedAt = DateTime.UtcNow;

            await _cache.RemoveAsync($"library_{id}");
            await _context.SaveChangesAsync();
            

            return true;
        }
        public async Task<bool> DeleteLibraryAsync(int id)
        {
            await Task.Delay(20);
            var library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == id);

            if (library == null) return false;

            _context.Libraries.Remove(library);
            await _cache.RemoveAsync($"library_{id}");
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
