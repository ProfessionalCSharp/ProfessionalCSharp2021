using System.Collections;
using System.Resources;
using System.Text;

const string ResourceFile = "Demo.resources";

CreateResource();
ReadResource();

void ReadResource()
{
    FileStream stream = File.OpenRead(ResourceFile);
    using ResourceReader reader = new(stream);
    reader.GetResourceData("Title", out string resourceType, out byte[] data);
    string title = Encoding.UTF8.GetString(data);
    Console.WriteLine(title);

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
