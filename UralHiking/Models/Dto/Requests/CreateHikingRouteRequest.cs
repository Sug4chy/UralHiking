namespace UralHiking.Models.Dto.Requests;

public sealed record CreateHikingRouteRequest(
    string Name,
    string ShortDescription,
    string Description,
    int DistanceMeters,
    int DurationMinutes,
    int AscentMeters,
    double Rating,
    int ReviewCount,
    string PhotoUrl,
    string LocationName,
    string Difficulty,
    GearItemDto[] GearItems,
    CoordinateDto[] Coordinates
);