using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

Category appetizers = new("Appetizers");
appetizers.Items.Add(new Item("Dungeon Crab Cocktail", "Classic coctail sauce", 27M));
appetizers.Items.Add(new Item("Almond Crusted Scallops", "Almonds, Parmesan, chive beurre blanc", 19M));

Category dinner = new("Dinner");
dinner.Items.Add(new Item("Grilled King Salmon", "Lemon chive buerre blanc", 49M));

Card card = new("The Restaurant");
card.Categories.Add(appetizers);
card.Categories.Add(dinner);

string json = SerializeJson(card);
DeserializeJson(json);
UseDom(json);
UseReader(json);
UseWriter();

string SerializeJson(Card card)
{
    Console.WriteLine(nameof(SerializeJson));
    JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true,
        // ReferenceHandler = ReferenceHandler.Preserve
    };
    string json = JsonSerializer.Serialize(card, options);
    Console.WriteLine(json);
    Console.WriteLine();
    return json;
}

static void DeserializeJson(string json)
{
    Console.WriteLine(nameof(DeserializeJson));
    JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
    };
    Card? card = JsonSerializer.Deserialize<Card>(json, options);
    Console.WriteLine();
}

void UseDom(string json)
{
    Console.WriteLine(nameof(UseDom));

    using JsonDocument document = JsonDocument.Parse(json);
    JsonElement element = document.RootElement.GetProperty("title");
    foreach (JsonElement category in document.RootElement.GetProperty("categories").EnumerateArray())
    {
        foreach (JsonElement item in category.GetProperty("items").EnumerateArray())
        {
            foreach (JsonProperty property in item.EnumerateObject())
            {
                Console.WriteLine($"{property.Name} {property.Value}");
            }
            Console.WriteLine($"{item.GetProperty("title")}");
        }
    }
}

void UseReader(string json)
{
    Console.WriteLine(nameof(UseReader));

    bool isNextPrice = false;
    bool isNextTitle = false;
    string? title = default;
    byte[] data = Encoding.UTF8.GetBytes(json);
    Utf8JsonReader reader = new(data);
    while (reader.Read())
    {

        if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "title")
        {
            isNextTitle = true;
        }
        if (reader.TokenType == JsonTokenType.String && isNextTitle)
        {
            title = reader.GetString();
            isNextTitle = false;
        }
        if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "price")
        {
            isNextPrice = true;
        }
        if (reader.TokenType == JsonTokenType.Number && isNextPrice && reader.TryGetDecimal(out decimal price))
        {
            Console.WriteLine($"{title}, price: {price:C}");
            isNextPrice = false;
        }
    }
    Console.WriteLine();
}

void UseWriter()
{
    Console.WriteLine(nameof(UseWriter));
    using MemoryStream stream = new();

    JsonWriterOptions options = new()
    {
        Indented = true
    };
    using (Utf8JsonWriter writer = new(stream, options))
    {
        writer.WriteStartArray();
        writer.WriteStartObject();
        writer.WriteStartObject("Book");
        writer.WriteString("Title", "Professional C# and .NET");
        writer.WriteString("Subtitle", "2021 Edition");
        writer.WriteEndObject();
        writer.WriteEndObject();
        writer.WriteStartObject();
        writer.WriteStartObject("Book");
        writer.WriteString("Title", "Professional C# 7 and .NET Core 2");
        writer.WriteString("Subtitle", "2018 Edition");
        writer.WriteEndObject();
        writer.WriteEndObject();
        writer.WriteEndArray();
    }
    string json = Encoding.UTF8.GetString(stream.ToArray());
    Console.WriteLine(json);
    Console.WriteLine();
}

public record Item(string Title, string Text, decimal Price);
public record Category(string Title)
{
    public IList<Item> Items { get; } = new List<Item>();
}
public record Card(string Title)
{
    public IList<Category> Categories { get; } = new List<Category>();
}
