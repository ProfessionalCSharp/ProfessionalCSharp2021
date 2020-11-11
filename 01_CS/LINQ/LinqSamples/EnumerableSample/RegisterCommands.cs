using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Reflection;

namespace EnumerableSample
{
    public static class RegisterCommandHandler
    {
        public static void Register(RootCommand rootCommand, string commandText, string className)
        {
            var command = new Command(commandText);

            MethodInfo[] methods = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name == className)
                .Single()
                .GetMethods()
                .Where(m => m.IsPublic && m.IsStatic)
                .ToArray();

            foreach (var method in methods)
            {
                command.AddCommand(new Command(method.Name.ToLower())
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
}
