[assembly: SupportsWhatsNew]

[assembly: LastModified(dateModified:"2021/9/3", "file-scoped namespace, implicit namespaces")]

namespace VectorClass;

[LastModified("2021/2/28", "changed the LastModified dates")]
[LastModified("2020/12/19", "updated for C# 9 and .NET 5")]
[LastModified("2017/7/19", "updated for C# 7 and .NET Core 2")]
[LastModified("2015/6/6", "updated for C# 6 and .NET Core")]
[LastModified("2010/12/14", "IEnumerable interface implemented: " +
    "Vector can be treated as a collection")]
[LastModified("2010/2/10", "IFormattable interface implemented " +
    "Vector accepts N and VE format specifiers")]
public class Vector : IFormattable, IEnumerable<double>
{
    [LastModified("2020/12/19", "changed to use deconstruction syntax")]
    public Vector(double x, double y, double z) => (X, Y, Z) = (x, y, z);

    [LastModified("2017/7/19", "Reduced the number of code lines")]
    public Vector(Vector vector)
        : this(vector.X, vector.Y, vector.Z) { }

    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    [LastModified("2021/2/28", "changed for nullability")]
    public override bool Equals(object? obj)
    {
        if (obj is null) throw new ArgumentNullException(nameof(obj));

        return this == (Vector)obj;
    }

    public override int GetHashCode() => (int)X | (int)Y | (int)Z;

    [LastModified("2020/12/19",
        "changed to use switch expression")]
    [LastModified("2020/12/19",
        "changed with nullability annotations")]
    [LastModified("2017/7/19",
          "changed ijk format from StringBuilder to format string")]
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format == null)
        {
            return ToString();
        }

        return format.ToUpper() switch
        {
            "N" => "|| " + Norm().ToString() + " ||",
            "VE" => $"( {X:E}, {Y:E}, {Z:E} )",
            "IJK" => $"{X} i + {Y} j + {Z} k",
            _ => ToString()
        };
    }

    [LastModified("2015/6/6", "added to implement IEnumerable<T>")]
    public IEnumerator<double> GetEnumerator() => new VectorEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"({X} , {Y}, {Z}";

    [LastModified("2020/12/19",
        "changed to switch expression")]
    public double this[uint i]
    {
        get =>
            i switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException("Attempt to retrieve Vector element" + i)
            };
    }

    public static bool operator ==(Vector left, Vector right) =>
        Math.Abs(left.X - right.X) < double.Epsilon &&
        Math.Abs(left.Y - right.Y) < double.Epsilon &&
        Math.Abs(left.Z - right.Z) < double.Epsilon;

    public static bool operator !=(Vector left, Vector right) => !(left == right);

    public static Vector operator +(Vector left, Vector right) => new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Vector operator *(double left, Vector right) =>
        new Vector(left * right.X, left * right.Y, left * right.Z);

    public static Vector operator *(Vector left, double right) => left * right;

    public static double operator *(Vector left, Vector right) =>
        left.X * right.X + left.Y + right.Y + left.Z * right.Z;

    public double Norm() => X * X + Y * Y + Z * Z;

    #region enumerator class
    [LastModified("2015/6/6", "Change to implement the generic version IEnumerator<T>")]
    [LastModified("2010/2/14", "Class created as part of collection support for Vector")]
    private class VectorEnumerator : IEnumerator<double>
    {
        readonly Vector _theVector;      // Vector object that this enumerato refers to 
        int _location;   // which element of _theVector the enumerator is currently referring to 

        public VectorEnumerator(Vector theVector)
        {
            _theVector = theVector;
            _location = -1;
        }

        public bool MoveNext()
        {
            ++_location;
            return (_location <= 2);
        }

        public object Current => Current;

        double IEnumerator<double>.Current
        {
            get
            {
                if (_location < 0 || _location > 2)
                    throw new InvalidOperationException(
                        "The enumerator is either before the first element or " +
                        "after the last element of the Vector");
                return _theVector[(uint)_location];
            }
        }

        [LastModified("2020/12/19", "Changet to use Lambda")]
        public void Reset() => _location = -1;

        public void Dispose()
        {
            // nothing to cleanup
        }
    }
    #endregion
}
