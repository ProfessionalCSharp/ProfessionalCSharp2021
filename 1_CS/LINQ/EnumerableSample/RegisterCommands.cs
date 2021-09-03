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
            command.AddCommand(new(method.Name.ToLower())
            {
                Handler = CommandHandler.Create(() =>
                {
                    MethodInfo m = method;
                    m.Invoke(null, null);
                })
            });
        }

        rootCommand.AddCommand(command);
    }
}
