using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Services;

namespace GamesAPI.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibrariesController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost]

        public async Task<ActionResult<LibraryResponse>> CreateLibraryRequest([FromBody] CreateLibraryRequest request)
        {
            var response = await _libraryService.CreateLibraryAsync(request);
                       
            return CreatedAtAction(nameof(GetLibraryById), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<LibraryResponse>> GetLibraries()
        {
            var responseList = await _libraryService.GetLibrariesAsync();

            return Ok(responseList);
        }

        [HttpGet("{id}", Name = "GetLibraryById")]
        public async Task<ActionResult<LibraryResponse>> GetLibraryById(int id)
        {
            var library = await _libraryService.GetLibraryByIdAsync(id);

            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<LibraryResponse>> UpdateLibrary(int id, [FromBody] UpdateLibraryRequest request)
        {
            var response = await _libraryService.UpdateLibraryAsync(id, request);

            if (response == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LibraryResponse>> DeleteLibrary(int id)
        {
            var library = await _libraryService.DeleteLibraryAsync(id);

            if (library == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
