using System.Text.Json.Serialization;

namespace JsonInheritance;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Game))]
internal partial class GamesContext : JsonSerializerContext
{
}
