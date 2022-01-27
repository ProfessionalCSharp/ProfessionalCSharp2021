namespace EnumerableSample;

public static class RegisterCommands
{
    public static void Register(RootCommand rootCommand, string commandText, string className)
    {
        Command command = new(commandText);

        MethodInfo[] methods = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Single(t => t.Name == className)
            .GetMethods()
            .Where(m => m.IsPublic && m.IsStatic)
            .ToArray();

        foreach (var method in methods)
        {
            Command featureCommand = new(method.Name.ToLower());
            featureCommand.SetHandler(() =>
            {
                MethodInfo m = method;
                m.Invoke(null, null);
            });
            command.AddCommand(featureCommand);
        }

        rootCommand.AddCommand(command);
    }
}
