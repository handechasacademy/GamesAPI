using System.ComponentModel.DataAnnotations;
using GamesAPI.Models;
using GamesAPI.Models.Enums;

namespace GamesAPI.DTOs
{
    

    public record GameResponse (
        int Id,
        string Name,
        DateOnly ReleaseDate,
        string Publisher,
        decimal Price,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        PlayType PlayingType,
        Platform GamePlatform,
        int GenreId
    );

    public record CreateGameRequest(
        [Required(ErrorMessage = "Game Title is obligatory.")][StringLength(100, MinimumLength = 2, ErrorMessage = "Title needs to be between 2 and 100 characters.")] string Name,
        [Required(ErrorMessage = "Publisher name is obligatory.")][StringLength(100, MinimumLength = 2, ErrorMessage = "Publisher Name needs to be between 2 and 100 characters")] string Publisher,
        [Required][Range(0.01, 100000, ErrorMessage = "Price must be greater than zero.")] decimal Price,
        DateOnly ReleaseDate,
        PlayType PlayingType,
        Platform GamePlatform,
        int GenreId
    );

    public record UpdateGameRequest(
        string Name,
        string Publisher,
        decimal Price,
        DateOnly ReleaseDate,
        PlayType PlayingType,
        Platform GamePlatform,
        int GenreId
    );
}
