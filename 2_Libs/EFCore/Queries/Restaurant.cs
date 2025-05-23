﻿public class Restaurant
{
    public Restaurant(string name, int id = default) => (_name, _id) = (name, id);

#pragma warning disable IDE0032 // Use auto property - _id and _name are used with EF Core mapping
    private readonly int _id;
    private readonly string _name;

    public string Name => _name;
#pragma warning restore IDE0032 // Use auto property

    public override string ToString() => $"{Name}, {_id}";
}
