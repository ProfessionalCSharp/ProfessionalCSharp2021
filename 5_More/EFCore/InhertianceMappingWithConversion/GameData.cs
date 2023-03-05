using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public record class GameData(Guid GameId, string GameType, string PlayerName)
{
    public ICollection<MoveData> Moves { get;  } = new List<MoveData>();
}

public abstract record class MoveData(Guid GameId, Guid MoveId, int MoveNumber);
public record class MoveData<TField>(Guid GameId, Guid MoveId, int MoveNumber)
    : MoveData(GameId, MoveId, MoveNumber)
{
    public required ICollection<TField> Fields { get; init; }
}

public record ColorField(string Color) : IParsable<ColorField>
{
    public override string ToString() => Color;

    public static ColorField Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, null, out ColorField? color))
        {
            return color;   
        }
        else
        {
            throw new ArgumentException($"Cannot parse value {s}", nameof(s));
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ColorField result)
    {
        if (s is null)
        {
            result = null;
            return false;
        }
        result = new ColorField(s);
        return true;
    }
}
public record ShapeAndColorField(string Shape, string Color) : IParsable<ShapeAndColorField>
{
    public override string ToString() => $"{Shape};{Color}";

    public static ShapeAndColorField Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out ShapeAndColorField? shape))
        {
            return shape;
        }
        else
        {
            throw new ArgumentException($"Cannot parse value {s}", nameof(s));
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ShapeAndColorField result)
    {
        result = null;
        if (s is null)
        {
            return false;
        }
        string[] parts = s.Split(';');
        if (parts.Length != 2)
        {
            return false;
        }
        result = new ShapeAndColorField(parts[0], parts[1]);
        return true;
    }
}
