using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;

namespace GamesAPI.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly AppDbContext _context;
        public LibraryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LibraryResponse> GetLibraries()
        {
            return _context.Libraries.Select(l => new LibraryResponse
            (
                l.Id,
                l.Name,
                l.Description,
                l.CreatedAt,
                l.UpdatedAt
            )).ToList();
        }

        public LibraryResponse? GetLibraryById(int id)
        {
            var library = _context.Libraries.FirstOrDefault(l => l.Id == id);

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

        public LibraryResponse CreateLibrary(CreateLibraryRequest request)
        {
            var newLibrary = new Library
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Libraries.Add(newLibrary);
            _context.SaveChanges();

            return new LibraryResponse
            (
                newLibrary.Id,
                newLibrary.Name,
                newLibrary.Description,
                newLibrary.CreatedAt,
                newLibrary.UpdatedAt
            );
        }
        public LibraryResponse? UpdateLibrary(int id, UpdateLibraryRequest request)
        {
            var library = _context.Libraries.FirstOrDefault(l => l.Id == id);

            if (library == null) return null;

            library.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : library.Name;
            library.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : library.Description;
            library.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return new LibraryResponse
            (
                library.Id,
                library.Name,
                library.Description,
                library.CreatedAt,
                library.UpdatedAt
            );
        }
        public LibraryResponse? DeleteLibrary(int id)
        {
            var library = _context.Libraries.FirstOrDefault(l => l.Id == id);

            if (library == null) return null;

            _context.Libraries.Remove(library);
            _context.SaveChanges();

            return new LibraryResponse
            (
                library.Id,
                library.Name,
                library.Description,
                library.CreatedAt,
                library.UpdatedAt
            );
        }
    }
}
