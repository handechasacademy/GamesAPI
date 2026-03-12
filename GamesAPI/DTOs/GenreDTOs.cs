using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs
{
    public record GenreResponse(
        int Id,
        string Name
    );

    public record CreateGenreRequest(
        [Required(ErrorMessage = "Genre name is obligatory.")] [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre name needs to be between 2 and 50 characters.")] string Name
    );

    public record UpdateGenreRequest(
        string Name
    );
}
