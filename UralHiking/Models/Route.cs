using UralHiking.Models.Enums;

namespace UralHiking.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public sealed class HikingRoute
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("short_description")]
    public string ShortDescription { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("distance_meters")]
    public int DistanceMeters { get; set; }

    [JsonPropertyName("duration_minutes")]
    public int DurationMinutes { get; set; }

    [JsonIgnore]
    public HikingRouteDifficulty DifficultyInternal { get; set; }

    [JsonPropertyName("difficulty")]
    public string Difficulty => DifficultyInternal.ToString().ToUpper();

    [JsonPropertyName("ascent_meters")]
    public int AscentMeters { get; set; }

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("review_count")]
    public int ReviewCount { get; set; }

    [JsonPropertyName("photo_url")]
    public string PhotoUrl { get; set; } = null!;

    [JsonPropertyName("location_name")]
    public string LocationName { get; set; } = null!;

    [JsonPropertyName("gear_items")]
    public ICollection<GearItem> GearItems { get; set; } = [];

    [JsonPropertyName("coordinates")]
    public ICollection<Coordinate> Coordinates { get; set; } = [];
}