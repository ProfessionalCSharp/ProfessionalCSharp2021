using System.CommandLine;

namespace EnumerableSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new RootCommand("EnumerableSample");

            RegisterCommandHandler.Register(rootCommand, "linq", nameof(LinqSamples));
            RegisterCommandHandler.Register(rootCommand, "filter", nameof(FilteringSamples));
            RegisterCommandHandler.Register(rootCommand, "grouping", nameof(GroupingSamples));
            RegisterCommandHandler.Register(rootCommand, "compound", nameof(CompoundFromSamples));
            RegisterCommandHandler.Register(rootCommand, "join", nameof(JoinSamples));
            RegisterCommandHandler.Register(rootCommand, "sort", nameof(SortingSamples));
            rootCommand.Invoke(args);
            //var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            //app.FullName = "LINQ Sample App";
            //LinqSamples.Register(app);
            //FilteringSamples.Register(app);
            //GroupingSamples.Register(app);
            //CompoundFromSamples.Register(app);
            //JoinSamples.Register(app);
            //SortingSamples.Register(app);

            //app.Command("help", cmd =>
            //{
            //    cmd.Description = "Get help for the application";
            //    CommandArgument commandArgument = cmd.Argument("<COMMAND>", "The command to get help for");
            //    cmd.OnExecute(() =>
            //    {
            //        app.ShowHelp(commandArgument.Value);
            //        return 0;
            //    });
            //});

            //app.OnExecute(() =>
            //{
            //    app.ShowHelp();
            //    return 0;
            //});

            //app.Execute(args);
        }
    }
}
