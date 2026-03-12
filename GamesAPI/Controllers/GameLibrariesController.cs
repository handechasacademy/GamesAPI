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
        public ActionResult<GameLibraryResponse> CreateGameLibraryRequest([FromBody] CreateGameLibraryRequest request)
        {
            var response = _gameLibraryService.CreateGameLibrary(request);

            return CreatedAtAction(nameof(GetGameLibraryById), new { gameId = response.GameId, libraryId = response.LibraryId }, response);
        }

        [HttpGet]
        public ActionResult<GameLibraryResponse> GetGameLibraries()
        {
            var responseList = _gameLibraryService.GetGameLibraries();

            return Ok(responseList);
        }

        [HttpGet("{gameId}/{libraryId}", Name = "GetGameLibraryById")]
        public ActionResult<GameLibraryResponse> GetGameLibraryById(int gameId, int libraryId)
        {
            var gameLibrary = _gameLibraryService.GetGameLibraryById(gameId, libraryId);
            if (gameLibrary == null)
            {
                return NotFound();            
            }


            return Ok(gameLibrary);
        }

        [HttpPut("{gameId}/{libraryId}")]
        public ActionResult<GameLibraryResponse> UpdateGameLibrary(int gameId, int libraryId, [FromBody] UpdateGameLibraryRequest request)
        {
            var gameLibrary = _gameLibraryService.UpdateGameLibrary(gameId, libraryId, request);

            if (gameLibrary == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{gameId}/{libraryId}")]
        public ActionResult<GameLibraryResponse> DeleteGameLibrary(int gameId, int libraryId)
        {
            var gameLibrary = _gameLibraryService.DeleteGameLibrary(gameId, libraryId);

            if (gameLibrary == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
