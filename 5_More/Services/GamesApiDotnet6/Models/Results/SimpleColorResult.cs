using System.Diagnostics.CodeAnalysis;

namespace Codebreaker.Models;

public enum ResultInformation
{
    Incorrect,
    CorrectPositionAndColor,
    CorrectColor
}

public readonly record struct SimpleColorResult(ResultInformation[] Results);