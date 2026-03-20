using GamesAPI.Models.Enums;

namespace GamesAPI.DTOs
{
    public record GameFilter(
        string? Genre,
        PlayType? PlayingType,
        Platform? GamePlatform,
        string? SearchTerm
        );
    public record GameLibraryFilter(
        bool? IsFavourite
        );
}
