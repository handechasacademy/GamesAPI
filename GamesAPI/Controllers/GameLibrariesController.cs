using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;


namespace GamesAPI.Controllers
{
    [Route("api/gamelibraries")]
    [ApiController]
    public class GameLibrariesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GameLibrariesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateGameLibraryRequest([FromBody] CreateGameLibraryRequest request)
        {
            var newGameLibrary = new GameLibrary
            {
                IsFavourite = request.IsFavourite,
                HoursPlayed = request.HoursPlayed,
                GameId = request.GameId,
                LibraryId = request.LibraryId
            };

            _context.GameLibraries.Add(newGameLibrary);
            _context.SaveChanges();

            var response = new GameLibraryResponse
            (
                newGameLibrary.IsFavourite,
                newGameLibrary.HoursPlayed,
                newGameLibrary.GameId,
                newGameLibrary.LibraryId
            );

            return CreatedAtAction(nameof(GetGameLibraryById), new { gameId = newGameLibrary.GameId, libraryId = newGameLibrary.LibraryId }, response);
        }

        [HttpGet]
        public IActionResult GetGameLibraries()
        {
            var responseList = _context.GameLibraries.Select(gl => new GameLibraryResponse(
                    gl.IsFavourite,
                    gl.HoursPlayed,
                    gl.GameId,
                    gl.LibraryId
                )).ToList();

            return Ok(responseList);
        }

        [HttpGet("{gameId}/{libraryId}", Name = "GetGameLibraryById")]
        public IActionResult GetGameLibraryById(int gameId, int libraryId)
        {
            var gameLibrary = _context.GameLibraries.FirstOrDefault(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null)
            {
                return NotFound();
            }

            var response = new GameLibraryResponse
            (
                gameLibrary.IsFavourite,
                gameLibrary.HoursPlayed,
                gameLibrary.GameId,
                gameLibrary.LibraryId
            );

            return Ok(response);
        }

        [HttpPut("{gameId}/{libraryId}")]
        public IActionResult UpdateGameLibrary(int gameId, int libraryId, [FromBody] UpdateGameLibraryRequest request)
        {
            var gameLibrary = _context.GameLibraries.FirstOrDefault(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null)
            {
                return NotFound();
            }

            gameLibrary.IsFavourite = request.IsFavourite;
            gameLibrary.HoursPlayed = request.HoursPlayed;

            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{gameId}/{libraryId}")]
        public IActionResult DeleteGameLibrary(int gameId, int libraryId)
        {
            var gameLibrary = _context.GameLibraries.FirstOrDefault(gl => gl.GameId == gameId && gl.LibraryId == libraryId);

            if (gameLibrary == null)
            {
                return NotFound();
            }

            _context.GameLibraries.Remove(gameLibrary);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
