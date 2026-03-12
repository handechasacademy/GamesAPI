using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public ActionResult<GenreResponse> CreateGenreRequest([FromBody] CreateGenreRequest request)
        {

            var response = _genreService.CreateGenre(request);

            return CreatedAtAction(nameof(GetGenreById), new { id = response.Id }, response);
        }

        [HttpGet]
        public ActionResult<IEnumerable<GenreResponse>> GetGenres()
        {
            var genres = _genreService.GetGenres();
            return Ok(genres);
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        public ActionResult<GenreResponse> GetGenreById(int id)
        {
            var genre = _genreService.GetGenreById(id);

            if (genre == null)
            {
                return NotFound();
            }


            return Ok(genre);
        }

        [HttpPut("{id}")]
        public ActionResult<GenreResponse> UpdateGenre(int id, [FromBody] UpdateGenreRequest request)
        {
            var response = _genreService.UpdateGenre(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<GenreResponse> DeleteGenre(int id)
        {
            var response = _genreService.DeleteGenre(id);

            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
