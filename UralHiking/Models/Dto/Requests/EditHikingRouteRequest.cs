namespace UralHiking.Models.Dto.Requests;

public sealed record EditHikingRouteRequest(
    string? Name = null,
    string? ShortDescription = null,
    string? Description = null,
    int? DistanceMeters = null,
    int? DurationMinutes = null,
    int? AscentMeters = null,
    double? Rating = null,
    int? ReviewCount = null,
    string? PhotoUrl = null,
    string? LocationName = null,
    string? Difficulty = null,
    GearItemDto[]? GearItems = null,
    CoordinateDto[]? Coordinates = null
);