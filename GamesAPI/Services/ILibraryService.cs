using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface ILibraryService
    {
        IEnumerable<LibraryResponse> GetLibraries();
        LibraryResponse? GetLibraryById(int id);
        LibraryResponse? CreateLibrary(CreateLibraryRequest request);
        LibraryResponse? UpdateLibrary(int id, UpdateLibraryRequest request);
        LibraryResponse? DeleteLibrary(int id);

    }
}
