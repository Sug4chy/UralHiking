using System.Net.Mime;
using System.Text.Json.Serialization;

namespace UralHiking.Models.Dto;

public record GearItemDto(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("url")] string Url
);