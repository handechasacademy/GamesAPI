using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GamesAPI.Data;
using GamesAPI.DTOs;
using GamesAPI.Models;

namespace GamesAPI.Controllers
{
    [Route("api/libraries")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibrariesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public IActionResult CreateLibraryRequest([FromBody] CreateLibraryRequest request)
        {
            var newLibrary = new Library
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Libraries.Add(newLibrary);
            _context.SaveChanges();

            var response = new LibraryResponse
            (
                newLibrary.Id,
                newLibrary.Name,
                newLibrary.Description,
                newLibrary.CreatedAt,
                newLibrary.UpdatedAt
            );
            return CreatedAtAction(nameof(GetLibraryById), new { id = newLibrary.Id }, response);
        }

        [HttpGet]
        public IActionResult GetLibraries()
        {
            var responseList = _context.Libraries
                .Select(l => new LibraryResponse(
                    l.Id,
                    l.Name,
                    l.Description,
                    l.CreatedAt,
                    l.UpdatedAt
                )).ToList();

            return Ok(responseList);
        }

        [HttpGet("{id}", Name = "GetLibraryById")]
        public IActionResult GetLibraryById(int id)
        {
            var library = _context.Libraries.FirstOrDefault(l => l.Id == id);

            if (library == null)
            {
                return NotFound();
            }

            var response = new LibraryResponse
            (
                library.Id,
                library.Name,
                library.Description,
                library.CreatedAt,
                library.UpdatedAt
            );

            return Ok(response);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateLibrary(int id, [FromBody] UpdateLibraryRequest request)
        {
            var library = _context.Libraries.FirstOrDefault(l => l.Id == id);

            if (library == null)
            {
                return NotFound();
            }

            library.Name = request.Name ?? library.Name;
            library.Description = request.Description ?? library.Description;
            library.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            var response = new LibraryResponse
            (
                library.Id,
                library.Name,
                library.Description,
                library.CreatedAt,
                library.UpdatedAt
            );

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLibrary(int id)
        {
            var library = _context.Libraries.FirstOrDefault(l => l.Id == id);

            if (library == null)
            {
                return NotFound();
            }

            _context.Libraries.Remove(library);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
