using System.Text.Json.Serialization;
using UralHiking.Models.Enums;

namespace UralHiking.Models;

public sealed class HikingRoute
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DistanceMeters { get; set; }
    public int DurationMinutes { get; set; }

    [JsonIgnore]
    public HikingRouteDifficulty DifficultyInternal { get; set; }
    public string Difficulty => DifficultyInternal.ToString().ToUpper();
    public int AscentMeters { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public string PhotoUrl { get; set; } = null!;
    public string LocationName { get; set; } = null!;
    public ICollection<GearItem> GearItems { get; set; } = [];
    public ICollection<Coordinate> Coordinates { get; set; } = [];
}