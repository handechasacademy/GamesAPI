using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryResponse>> GetLibrariesAsync();
        Task<PagedResponse<LibraryResponse>> GetPagedLibrariesAsync(int page, int pageSize);
        Task<LibraryResponse?> GetLibraryByIdAsync(int id);
        Task<LibraryResponse?> CreateLibraryAsync(CreateLibraryRequest request);
        Task<bool> UpdateLibraryAsync(int id, UpdateLibraryRequest request);
        Task<bool> DeleteLibraryAsync(int id);

    }
}
