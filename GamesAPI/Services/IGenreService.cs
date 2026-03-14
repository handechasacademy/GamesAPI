using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponse>> GetGenresAsync();
        Task<GenreResponse?> GetGenreByIdAsync(int id);
        Task<GenreResponse> CreateGenreAsync(CreateGenreRequest request);
        Task<bool> UpdateGenreAsync(int id, UpdateGenreRequest request);
        Task<bool> DeleteGenreAsync(int id);
    }
}
