using System.Text.Json.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Dtos;

public class ApplicationDto
{
    public string Name { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ApplicationType Type { get; set; }
}
