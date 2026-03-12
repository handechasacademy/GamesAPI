using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGameLibraryService
    {
        IEnumerable<GameLibraryResponse> GetGameLibraries();
        GameLibraryResponse? GetGameLibraryById(int gameId, int libraryId);
        GameLibraryResponse CreateGameLibrary(CreateGameLibraryRequest request);
        GameLibraryResponse? UpdateGameLibrary(int gameId, int libraryId, UpdateGameLibraryRequest request);   
        GameLibraryResponse? DeleteGameLibrary(int gameId, int libraryId);
    }
}
