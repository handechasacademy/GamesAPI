using GamesAPI.DTOs;

namespace GamesAPI.Services
{
    public interface IGameService
    {
        IEnumerable<GameResponse> GetGames();
        GameResponse ? GetGameById(int id);
        GameResponse  CreateGame(CreateGameRequest request);
        GameResponse? UpdateGame(int id, UpdateGameRequest request);
        GameResponse? DeleteGame(int id);
    }
}
