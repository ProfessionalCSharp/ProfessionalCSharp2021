# C# 12 and .NET 8 Updates

## Collection expressions

### Chapter 6, Arrays

Collection expressions are a new feature in C# 12. Collection expressions are used to initialize collections. The following code sample shows how to use collection expressions with arrays.

```csharp
int[] numbers1 = { 1, 2, 3, 4, 5 }; // array initializer
int[] numbers2 = [1, 2, 3, 4, 5]; // collection expression
```

### Chapter 8, Collections

Instead of

```csharp
    ImmutableArray<string> arr = ImmutableArray.Create<string>("one", "two", "three", "four", "five");

```

We can do this:

```csharp
    ImmutableArray<string> arr = ["one", "two", "three", "four", "five"];
```

## Spread operator

### Chapter 8, Collections

The spread operator is used to add elements of a collection to another collection. The following code sample shows how to use the spread operator with arrays.

Instead of this:

```csharp
    ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();
````

We can do this:

```csharp
    ImmutableList<Account> immutableAccounts = [.. accounts];
````

## Primary constructor

### Chapter 8, Collections

The primary constructor is used to initialize members of a class. The following code sample shows how to use the primary constructor with the `ImmutableList<T>` class.

Instead of this:

```csharp
public class RacerComparer : IComparer<Racer>
{
    private CompareType _compareType;
    public RacerComparer(CompareType compareType) =>
      _compareType = compareType;
```

We can do this:

```csharp
public class RacerComparer(CompareType compareType) : IComparer<Racer>
{
    private CompareType _compareType = compareType;
```

Or this (and use the compareType as parameter, access it when needed):

```csharp
public class RacerComparer(CompareType compareType) : IComparer<Racer>
{
```