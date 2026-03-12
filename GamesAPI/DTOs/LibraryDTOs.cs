using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs
{
    public record LibraryResponse (
        int Id,
        string Name,
        string Description,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );

    public record CreateLibraryRequest(
        [Required(ErrorMessage = "Library name is obligatory.")] [StringLength(100, MinimumLength = 2, ErrorMessage = "Library name needs to be between 2 and 100 characters.")] string Name,
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")] string Description
    );

    public record UpdateLibraryRequest(
        string Name,
        string Description
    );
}
