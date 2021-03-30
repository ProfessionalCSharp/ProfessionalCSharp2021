using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

StringBuilder OutputText = new();

// modify this line to retrieve details of any other data type
Type t = typeof(double);

AnalyzeType(t);
Console.WriteLine($"Analysis of type {t.Name}");
Console.WriteLine(OutputText.ToString());

Console.ReadLine();

void AnalyzeType(Type t)
{
    TypeInfo typeInfo = t.GetTypeInfo();
    AddToOutput($"Type Name: {t.Name}");
    AddToOutput($"Full Name: {t.FullName}");
    AddToOutput($"Namespace: {t.Namespace}");

    Type? tBase = typeInfo.BaseType;

    if (tBase != null)
    {
        AddToOutput($"Base Type: {tBase.Name}");
    }

    ShowMembers("constructors", t.GetConstructors());
    ShowMembers("methods", t.GetMethods());
    ShowMembers("properties", t.GetProperties());
    ShowMembers("fields", t.GetFields());
    ShowMembers("events", t.GetEvents());

    void ShowMembers(string title, IList<MemberInfo> members)
    {
        if (members.Count == 0) return;
        AddToOutput($"\npublic {title}:");
        var names = members.Select(m => m.Name).Distinct();
        AddToOutput(string.Join(" ", names));
    }

    void AddToOutput(string Text) =>
        OutputText.Append($"{Text}{Environment.NewLine}");
}
