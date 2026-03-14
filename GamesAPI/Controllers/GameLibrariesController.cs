using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Services;


namespace GamesAPI.Controllers
{
    [Route("api/gamelibraries")]
    [ApiController]
    public class GameLibrariesController : ControllerBase
    {
        private readonly IGameLibraryService _gameLibraryService;
        public GameLibrariesController(IGameLibraryService gameLibraryService)
        {
            _gameLibraryService = gameLibraryService;
        }

        [HttpPost]
        public async Task<ActionResult<GameLibraryResponse>> CreateGameLibraryRequest([FromBody] CreateGameLibraryRequest request)
        {
            var response = await _gameLibraryService.CreateGameLibraryAsync(request);

            return CreatedAtAction(nameof(GetGameLibraryById), new { gameId = response.GameId, libraryId = response.LibraryId }, response);
        }

        [HttpGet]
        public async Task<ActionResult<GameLibraryResponse>> GetGameLibraries()
        {
            var responseList = await _gameLibraryService.GetGameLibrariesAsync();

            return Ok(responseList);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResponse<GameLibraryResponse>>> GetPagedGameLibraries(
            [AsParameters] GameLibraryFilter filter,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            var response = await _gameLibraryService.GetPagedGameLibrariesAsync(page, pageSize, filter);
            return Ok(response);
        }

        [HttpGet("{gameId}/{libraryId}", Name = "GetGameLibraryById")]
        public async Task<ActionResult<GameLibraryResponse>> GetGameLibraryById(int gameId, int libraryId)
        {
            var gameLibrary = await _gameLibraryService.GetGameLibraryByIdAsync(gameId, libraryId);

            return Ok(gameLibrary);
        }

        [HttpPut("{gameId}/{libraryId}")]
        public async Task<ActionResult<GameLibraryResponse>> UpdateGameLibrary(int gameId, int libraryId, [FromBody] UpdateGameLibraryRequest request)
        {
            var gameLibrary = await _gameLibraryService.UpdateGameLibraryAsync(gameId, libraryId, request);

            return NoContent();
        }

        [HttpDelete("{gameId}/{libraryId}")]
        public async Task<ActionResult<GameLibraryResponse>> DeleteGameLibrary(int gameId, int libraryId)
        {
            var gameLibrary = await _gameLibraryService.DeleteGameLibraryAsync(gameId, libraryId);

            return NoContent();
        }
    }
}
