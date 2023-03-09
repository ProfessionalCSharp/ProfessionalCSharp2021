using JsonInheritance;

using System.Text.Json;

// create the game instances

Game game1 = new(Guid.NewGuid(), "6x4", "player1");
game1.Moves.Add(new Move<ColorField>(game1.GameId, Guid.NewGuid(), 1)
{
    Fields = new[] { new ColorField("red"), new ColorField("blue"), new ColorField("green"), new ColorField("blue") }
});

Game game2 = new(Guid.NewGuid(), "5x5x4", "player2");
game2.Moves.Add(new Move<ShapeColorField>(game2.GameId, Guid.NewGuid(), 1)
{
    Fields = new[] { new ShapeColorField("circe", "red"), new ShapeColorField("square", "blue"), new ShapeColorField("circle", "green"), new ShapeColorField("rectangle", "yellow") }
});

// serialize to JSON

JsonSerializerOptions options = new()
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string json1 = JsonSerializer.Serialize(game1, options);
Console.WriteLine(json1);

string json2 = JsonSerializer.Serialize(game2, options);
Console.WriteLine(json2);

// deserialize from JSON

Game? game1b = JsonSerializer.Deserialize<Game>(json1, options);
Console.WriteLine(game1b);

Game? game2b = JsonSerializer.Deserialize<Game>(json2, options);
Console.WriteLine(game2b);
