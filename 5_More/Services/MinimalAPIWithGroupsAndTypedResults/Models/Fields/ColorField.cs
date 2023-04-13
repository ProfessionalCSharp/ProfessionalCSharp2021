using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public partial record ColorField(string Color) : IParsable<ColorField>
{
    public override string ToString() => Color;
}
