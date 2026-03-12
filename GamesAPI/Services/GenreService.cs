using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;

namespace GamesAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;
        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GenreResponse> GetGenres()
        {
            return _context.Genres.Select(g => new GenreResponse
            (
                g.Id,
                g.Name
            )).ToList();
        }

        public GenreResponse? GetGenreById(int id)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null) return null;

            return new GenreResponse
            (
                genre.Id,
                genre.Name
            );
        }

        public GenreResponse CreateGenre(CreateGenreRequest request)
        {
            var newGenre = new Genre
            {
                Name = request.Name
            };

            _context.Genres.Add(newGenre);
            _context.SaveChanges();

            return new GenreResponse
            (
                newGenre.Id,
                newGenre.Name
            );
        }

        public GenreResponse? UpdateGenre(int id, UpdateGenreRequest request)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null) return null;

            genre.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : genre.Name;

            _context.SaveChanges();

            return new GenreResponse
            (
                genre.Id,
                genre.Name
            );
        }

        public GenreResponse? DeleteGenre(int id)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null) return null;

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return new GenreResponse
            (
                genre.Id,
                genre.Name
            );
        }


    }
}
