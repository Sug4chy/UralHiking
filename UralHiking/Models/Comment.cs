using System.Text.Json.Serialization;

namespace UralHiking.Models;

public sealed class Comment
{
    public int Id { get; set; }
    [JsonIgnore] public int HikingRouteId { get; set; }
    [JsonIgnore] public HikingRoute? HikingRoute { get; set; } = null!;
    public string UserLogin { get; set; }
    public string UserEmail { get; set; }
    public string Content { get; set; }
}