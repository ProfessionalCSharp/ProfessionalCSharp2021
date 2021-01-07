using System;
using System.Collections;
using System.IO;
using System.Resources;

CreateResource();
ReadResource();

const string ResourceFile = "Demo.resources";

void ReadResource()
{
    FileStream stream = File.OpenRead(ResourceFile);
    using ResourceReader reader = new(stream);
    foreach (DictionaryEntry resource in reader)
    {
        Console.WriteLine($"{resource.Key} {resource.Value}");
    }
}

void CreateResource()
{
    FileStream stream = File.OpenWrite(ResourceFile);

    using ResourceWriter writer = new(stream);

    writer.AddResource("Title", "Professional C#");
    writer.AddResource("Author", "Christian Nagel");
    writer.AddResource("Publisher", "Wrox Press");

    writer.Generate();
}
