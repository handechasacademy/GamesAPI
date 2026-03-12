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

        public ActionResult<LibraryResponse> CreateLibraryRequest([FromBody] CreateLibraryRequest request)
        {
            var response = _libraryService.CreateLibrary(request);
                       
            return CreatedAtAction(nameof(GetLibraryById), new { id = response.Id }, response);
        }

        [HttpGet]
        public ActionResult<LibraryResponse> GetLibraries()
        {
            var responseList = _libraryService.GetLibraries();

            return Ok(responseList);
        }

        [HttpGet("{id}", Name = "GetLibraryById")]
        public ActionResult<LibraryResponse> GetLibraryById(int id)
        {
            var library = _libraryService.GetLibraryById(id);

            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        [HttpPut("{id}")]

        public ActionResult<LibraryResponse> UpdateLibrary(int id, [FromBody] UpdateLibraryRequest request)
        {
            var response = _libraryService.UpdateLibrary(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<LibraryResponse> DeleteLibrary(int id)
        {
            var library = _libraryService.DeleteLibrary(id);

            if (library == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
