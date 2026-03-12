using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateGenreRequest([FromBody] CreateGenreRequest request)
        {
            var newGenre = new Genre
            {
                Name = request.Name
            };

            _context.Genres.Add(newGenre);
            _context.SaveChanges();

            var response = new GenreResponse
            (
                newGenre.Id,
                newGenre.Name
            );

            return CreatedAtAction(nameof(GetGenreById), new { id = newGenre.Id }, response);
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            var responseList = _context.Genres.Select(g => new GenreResponse
            (
                g.Id,
                g.Name
            )).ToList();
            return Ok(responseList);
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        public IActionResult GetGenreById(int id)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            var response = new GenreResponse
            (
                genre.Id,
                genre.Name
            );

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreRequest request)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            genre.Name = request.Name ?? genre.Name;
            _context.SaveChanges();

            var response = new GenreResponse
            (
                genre.Id,
                genre.Name
            );

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
