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
        public async Task<ActionResult<GameResponse>> CreateGame([FromBody] CreateGameRequest request)
        {

            var response = await _gameService.CreateGameAsync(request);

            return CreatedAtAction(nameof(GetGameById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameResponse>>> GetGames()
        {
            var games = await _gameService.GetGamesAsync();

            return Ok(games);
        }

        [HttpGet("paged")]

        public async Task<ActionResult<PagedResponse<GameResponse>>> GetPagedGames(
            [AsParameters] GameFilter filter,
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20
            )
        {
            pageSize =Math.Clamp(pageSize, 1, 100);

            var response = await _gameService.GetPagedGamesAsync(page, pageSize, filter);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetGameById")]
        public async Task<ActionResult<GameResponse>> GetGameById(int id)
        {
            var response = await _gameService.GetGameByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<GameResponse>> UpdateGame(int id, [FromBody] UpdateGameRequest request)
        {
            var response = await _gameService.UpdateGameAsync(id, request);

            if (response == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<GameResponse>> DeleteGame(int id)
        {
            var response = await _gameService.DeleteGameAsync(id);

            if (response == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
