using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateGameRequest([FromBody] CreateGameRequest request)
        {


            var newGame = new Game
            {
                Name = request.Name,
                Publisher = request.Publisher,
                Price = request.Price,
                ReleaseDate = request.ReleaseDate,
                PlayingType = request.PlayingType,
                GamePlatform = request.GamePlatform,
                GenreId = request.GenreId
            };

            _context.Games.Add(newGame);
            _context.SaveChanges();

            var response = new GameResponse
            (
                newGame.Id,
                newGame.Name,
                newGame.ReleaseDate,
                newGame.Publisher,
                newGame.Price,
                newGame.CreatedAt,
                newGame.UpdatedAt,
                newGame.PlayingType,
                newGame.GamePlatform,
                newGame.GenreId
            );

            return CreatedAtAction(nameof(GetGameById), new { id = newGame.Id }, response);
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            var responseList = _context.Games.Select(g => new GameResponse
                (
                    g.Id,
                    g.Name,
                    g.ReleaseDate,
                    g.Publisher,
                    g.Price,
                    g.CreatedAt,
                    g.UpdatedAt,
                    g.PlayingType,
                    g.GamePlatform,
                    g.GenreId
                )).ToList();
            return Ok(responseList);
        }

        [HttpGet("{id}", Name = "GetGameById")]
        public IActionResult GetGameById(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var response = new GameResponse
            (
                game.Id,
                game.Name,
                game.ReleaseDate,
                game.Publisher,
                game.Price,
                game.CreatedAt,
                game.UpdatedAt,
                game.PlayingType,
                game.GamePlatform,
                game.GenreId
            );
            return Ok(response);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateGame(int id, [FromBody] UpdateGameRequest request)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            game.Name = request.Name ?? game.Name;
            game.Publisher = request.Publisher ?? game.Publisher;
            game.Price = request.Price != default ? request.Price : game.Price;
            game.ReleaseDate = request.ReleaseDate != default ? request.ReleaseDate : game.ReleaseDate;
            game.PlayingType = request.PlayingType != default ? request.PlayingType : game.PlayingType;
            game.GamePlatform = request.GamePlatform != default ? request.GamePlatform : game.GamePlatform;
            game.GenreId = request.GenreId != default ? request.GenreId : game.GenreId;
            game.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
