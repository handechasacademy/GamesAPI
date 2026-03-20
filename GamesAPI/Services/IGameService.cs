using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameResponse>> GetGamesAsync();

        Task<PagedResponse<GameResponse>> GetPagedGamesAsync(int page, int pageSize, GameFilter filter);
        Task<GameResponse?> GetGameByIdAsync(int id);
        Task<GameResponse>  CreateGameAsync(CreateGameRequest request);
        Task<bool> UpdateGameAsync(int id, UpdateGameRequest request);
        Task<bool> DeleteGameAsync(int id);
    }
}
