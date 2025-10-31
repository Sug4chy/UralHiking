using System.Text.Json.Serialization;

namespace UralHiking.Models.Dto.Requests;

public record CreateHikingRouteRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("short_description")] string ShortDescription,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("distance_meters")] int DistanceMeters,
    [property: JsonPropertyName("duration_minutes")] int DurationMinutes,
    [property: JsonPropertyName("ascent_meters")] int AscentMeters,
    [property: JsonPropertyName("rating")] double Rating,
    [property: JsonPropertyName("review_count")] int ReviewCount,
    [property: JsonPropertyName("photo_url")] string PhotoUrl,
    [property: JsonPropertyName("location_name")] string LocationName,
    [property: JsonPropertyName("difficulty")] string Difficulty,
    [property: JsonPropertyName("gear_items")] GearItemDto[] GearItems,
    [property: JsonPropertyName("coordinates")] CoordinateDto[] Coordinates
);