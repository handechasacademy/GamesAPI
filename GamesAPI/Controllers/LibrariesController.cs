using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Authorize]
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

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResponse<LibraryResponse>>> GetPagedLibraries(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            var response = await _libraryService.GetPagedLibrariesAsync(page, pageSize);
            return Ok(response);
        }


        [HttpGet("{id}", Name = "GetLibraryById")]
        public async Task<ActionResult<LibraryResponse>> GetLibraryById(int id)
        {
            var library = await _libraryService.GetLibraryByIdAsync(id);

            return Ok(library);
        }

        [Authorize]
        [HttpPut("{id}")]

        public async Task<ActionResult<LibraryResponse>> UpdateLibrary(int id, [FromBody] UpdateLibraryRequest request)
        {
            var response = await _libraryService.UpdateLibraryAsync(id, request);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<LibraryResponse>> DeleteLibrary(int id)
        {
            var library = await _libraryService.DeleteLibraryAsync(id);

            return NoContent();
        }
    }
}
