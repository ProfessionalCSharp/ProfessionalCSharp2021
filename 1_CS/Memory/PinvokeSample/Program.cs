if (args.Length != 2)
{
    Console.WriteLine("usage: PInvokeSample existingfilename newfilename");
    return;
}
try
{
    FileUtility.CreateHardLink(args[0], args[1]);
}
catch (IOException ex)
{
    Console.WriteLine(ex.Message);
}
