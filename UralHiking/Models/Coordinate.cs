using System.Text.Json.Serialization;

namespace UralHiking.Models;

public sealed class Coordinate
{
    [JsonIgnore] public int Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    [JsonIgnore] public int HikingRouteId { get; set; }
    [JsonIgnore] public HikingRoute HikingRoute { get; set; } = null!;
}