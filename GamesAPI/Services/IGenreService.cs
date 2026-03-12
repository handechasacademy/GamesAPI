using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGenreService
    {
        IEnumerable<GenreResponse> GetGenres();
        GenreResponse? GetGenreById(int id);
        GenreResponse CreateGenre(CreateGenreRequest request);
        GenreResponse? UpdateGenre(int id, UpdateGenreRequest request);
        GenreResponse? DeleteGenre(int id);
    }
}
