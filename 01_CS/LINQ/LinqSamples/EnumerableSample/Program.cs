using System.CommandLine;
using EnumerableSample;

var rootCommand = new RootCommand("EnumerableSample");

RegisterCommands.Register(rootCommand, "linq", nameof(LinqSamples));
RegisterCommands.Register(rootCommand, "filter", nameof(FilterSamples));
RegisterCommands.Register(rootCommand, "group", nameof(GroupSamples));
RegisterCommands.Register(rootCommand, "compound", nameof(CompoundFromSamples));
RegisterCommands.Register(rootCommand, "join", nameof(JoinSamples));
RegisterCommands.Register(rootCommand, "sort", nameof(SortingSamples));
rootCommand.Invoke(args);
