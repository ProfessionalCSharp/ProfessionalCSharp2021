using System.CommandLine;
using EnumerableSample;

var rootCommand = new RootCommand("EnumerableSample");

RegisterCommandHandler.Register(rootCommand, "linq", nameof(LinqSamples));
RegisterCommandHandler.Register(rootCommand, "filter", nameof(FilterSamples));
RegisterCommandHandler.Register(rootCommand, "grouping", nameof(GroupSamples));
RegisterCommandHandler.Register(rootCommand, "compound", nameof(CompoundFromSamples));
RegisterCommandHandler.Register(rootCommand, "join", nameof(JoinSamples));
RegisterCommandHandler.Register(rootCommand, "sort", nameof(SortingSamples));
rootCommand.Invoke(args);
