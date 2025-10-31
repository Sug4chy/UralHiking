using System.Text.Json.Serialization;

namespace UralHiking.Models.Dto;

public record CoordinateDto(
    [property: JsonPropertyName("longitude")] double Longitude,
    [property: JsonPropertyName("latitude")] double Latitude
);