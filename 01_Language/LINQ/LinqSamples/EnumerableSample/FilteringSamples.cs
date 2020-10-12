using DataLib;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;

namespace EnumerableSample
{
    public class FilteringSamples
    {
        internal static void Register(RootCommand command)
        {
            var linqSamplesCommand = new Command("filter");


            linqSamplesCommand.Handler = CommandHandler.Create(() =>
            {

            });


            command.AddCommand(linqSamplesCommand);
        }

        //internal static void Register(CommandLineApplication app)
        //{
        //    app.Command("filter", cmd =>
        //    {
        //        var invokeMethodOption = new CommandOption("-m", CommandOptionType.NoValue);
        //        var indexOption = new CommandOption("-i", CommandOptionType.NoValue);
        //        var typeOption = new CommandOption("-t", CommandOptionType.NoValue);
        //        cmd.Options.AddRange(new[] { invokeMethodOption, indexOption, typeOption });
        //        cmd.Description = "filter -[m|i|t]";
        //        cmd.OnExecute(() =>
        //        {
        //            if (invokeMethodOption.HasValue())
        //            {
        //                FilteringWithMethods();
        //            }
        //            else if (indexOption.HasValue())
        //            {
        //                FilteringWithIndex();
        //            }
        //            else if (typeOption.HasValue())
        //            {
        //                TypeFiltering();
        //            }
        //            else
        //            {
        //                Filtering();
        //            }
        //            return 0;
        //        });
        //    });
        //}

        public static void Filtering()
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                         select r;

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void FilteringWithIndex()
        {
            var racers = Formula1.GetChampions()
                .Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void FilteringWithMethods()
        {
            var racers = Formula1.GetChampions()
                .Where(r => r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria"));

            foreach (var r in racers)
            {
                Console.WriteLine($"{r:A}");
            }
        }

        public static void TypeFiltering()
        {
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var s in query)
            {
                Console.WriteLine(s);
            }
        }
    }
}
