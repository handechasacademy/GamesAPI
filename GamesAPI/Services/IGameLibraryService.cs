using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGameLibraryService
    {
        Task<IEnumerable<GameLibraryResponse>> GetGameLibrariesAsync();
        Task<GameLibraryResponse?> GetGameLibraryByIdAsync(int gameId, int libraryId);
        Task<GameLibraryResponse> CreateGameLibraryAsync(CreateGameLibraryRequest request);
        Task<bool> UpdateGameLibraryAsync(int gameId, int libraryId, UpdateGameLibraryRequest request);   
        Task<bool> DeleteGameLibraryAsync(int gameId, int libraryId);
    }
}
