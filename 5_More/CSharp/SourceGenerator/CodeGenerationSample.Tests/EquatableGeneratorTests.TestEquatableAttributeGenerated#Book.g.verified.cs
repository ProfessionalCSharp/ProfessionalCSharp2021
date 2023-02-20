//HintName: Book.g.cs
// <generated />

#nullable enable
using System;

namespace Test.Sample;

public partial class Book : IEquatable<Book>
{
    private static partial bool IsTheSame(Book? left, Book? right);

    public override bool Equals(object? obj) => this == obj as Book;

    public bool Equals(Book? other) => this == other;

    public static bool operator==(Book? left, Book? right) => 
        IsTheSame(left, right);

    public static bool operator!=(Book? left, Book? right) =>
        !(left == right);
}

#nullable restore