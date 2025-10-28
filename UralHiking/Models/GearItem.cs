using System.Text.Json.Serialization;

namespace UralHiking.Models;

public sealed class GearItem
{
    [JsonPropertyName("text")] public string Text { get; set; } = null!;
    [JsonPropertyName("url")] public string Url { get; set; } = null!;
}