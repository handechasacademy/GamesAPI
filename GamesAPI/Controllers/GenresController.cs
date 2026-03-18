using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GenreResponse>> CreateGenreRequest([FromBody] CreateGenreRequest request)
        {

            var response = await _genreService.CreateGenreAsync(request);

            return CreatedAtAction(nameof(GetGenreById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreResponse>>> GetGenres()
        {
            var genres = await _genreService.GetGenresAsync();
            return Ok(genres);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResponse<GenreResponse>>> GetPagedGenres(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            var response = await _genreService.GetPagedGenresAsync(page, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        public async Task<ActionResult<GenreResponse>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);

            return Ok(genre);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GenreResponse>> UpdateGenre(int id, [FromBody] UpdateGenreRequest request)
        {
            var response = await _genreService.UpdateGenreAsync(id, request);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreResponse>> DeleteGenre(int id)
        {
            var response = await _genreService.DeleteGenreAsync(id);

            return NoContent();
        }
    }
}
