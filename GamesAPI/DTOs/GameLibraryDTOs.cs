using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs
{
    public record GameLibraryResponse(
        bool IsFavourite,
        decimal HoursPlayed,
        int GameId,
        int LibraryId
    );

    public record CreateGameLibraryRequest(
        bool IsFavourite,
        [Range(0, 100000, ErrorMessage = "Hours must be zero orgreater than zero.")] decimal HoursPlayed,
        int GameId,
        int LibraryId
    );

    public record UpdateGameLibraryRequest(
        bool IsFavourite,
        decimal HoursPlayed
    );
}
