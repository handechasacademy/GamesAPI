using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;
        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreResponse>> GetGenresAsync()
        {
            await Task.Delay(20);
            return await _context.Genres.Select(g => new GenreResponse
            (
                g.Id,
                g.Name
            )).ToListAsync();
        }

        public async Task<GenreResponse?> GetGenreByIdAsync(int id)
        {
            await Task.Delay(20);
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null) return null;

            return new GenreResponse
            (
                genre.Id,
                genre.Name
            );
        }

        public async Task<GenreResponse> CreateGenreAsync(CreateGenreRequest request)
        {
            await Task.Delay(20);
            var newGenre = new Genre
            {
                Name = request.Name
            };

            _context.Genres.Add(newGenre);
            await _context.SaveChangesAsync();

            return new GenreResponse
            (
                newGenre.Id,
                newGenre.Name
            );
        }

        public async Task<bool> UpdateGenreAsync(int id, UpdateGenreRequest request)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null) return false;

            genre.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : genre.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null) return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
