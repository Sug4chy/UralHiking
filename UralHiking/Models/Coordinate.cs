using System.Text.Json.Serialization;

namespace UralHiking.Models;

public sealed class Coordinate
{
    [JsonIgnore] public int Id { get; set; }
    [JsonPropertyName("longitude")] public double Longitude { get; set; }
    [JsonPropertyName("latitude")] public double Latitude { get; set; }
    [JsonIgnore] public int HikingRouteId { get; set; }
    [JsonIgnore] public HikingRoute HikingRoute { get; set; } = null!;
}