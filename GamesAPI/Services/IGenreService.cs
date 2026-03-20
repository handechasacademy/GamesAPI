using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponse>> GetGenresAsync();
        Task<PagedResponse<GenreResponse>> GetPagedGenresAsync(int page, int pageSize);
        Task<GenreResponse?> GetGenreByIdAsync(int id);
        Task<GenreResponse> CreateGenreAsync(CreateGenreRequest request);
        Task<bool> UpdateGenreAsync(int id, UpdateGenreRequest request);
        Task<bool> DeleteGenreAsync(int id);
    }
}
