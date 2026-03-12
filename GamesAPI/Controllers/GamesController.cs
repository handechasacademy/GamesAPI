using GamesAPI.DTOs;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public ActionResult<GameResponse> CreateGame([FromBody] CreateGameRequest request)
        {

            var response = _gameService.CreateGame(request);

            return CreatedAtAction(nameof(GetGameById), new { id = response.Id }, response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameResponse>> GetGames()
        {
            var games = _gameService.GetGames();

            return Ok(games);
        }

        [HttpGet("{id}", Name = "GetGameById")]
        public ActionResult<GameResponse> GetGameById(int id)
        {
            var response = _gameService.GetGameById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut("{id}")]

        public ActionResult<GameResponse> UpdateGame(int id, [FromBody] UpdateGameRequest request)
        {
            var response = _gameService.UpdateGame(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult<GameResponse> DeleteGame(int id)
        {
            var response = _gameService.DeleteGame(id);

            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
