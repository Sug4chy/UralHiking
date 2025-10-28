using System.Text.Json.Serialization;

namespace UralHiking.Models;

public sealed class Coordinate
{
    [JsonPropertyName("longitude")] public double Longitude { get; set; }
    [JsonPropertyName("latitude")] public double Latitude { get; set; }
}