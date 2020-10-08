using System;

var test = new MethodOverloads();
test.Foo(33);
test.Foo("abc");
test.Foo("abc", 42);
test.Foo(33, "abc");
test.Bar(44);

public class MethodOverloads
{
    public void Foo<T>(T obj)
        where T : notnull =>
        Console.WriteLine($"Foo<T>(T obj), obj type: {obj.GetType().Name}");

    public void Foo(int x) =>
        Console.WriteLine("Foo(int x)");

    public void Foo<T1, T2>(T1 obj1, T2 obj2)
        where T1 : notnull
        where T2: notnull => 
        Console.WriteLine($"Foo<T1, T2>(T1 obj1, T2 obj2); {obj1.GetType().Name} {obj2.GetType().Name}");

    public void Foo<T>(int obj1, T obj2)
        where T : notnull =>
        Console.WriteLine($"Foo<T>(int obj1, T obj2); {obj1} {obj2.GetType().Name}");

    public void Bar<T>(T obj)
        where T : notnull =>
        Foo(obj);
}


