using System.Text.Json.Serialization;

namespace UralHiking.Models;

public sealed class GearItem
{
    [JsonIgnore] public int Id { get; set; }
    [JsonPropertyName("text")] public string Text { get; set; } = null!;
    [JsonPropertyName("url")] public string Url { get; set; } = null!;
    [JsonIgnore] public ICollection<HikingRoute> HikingRoutes { get; set; } = [];
}