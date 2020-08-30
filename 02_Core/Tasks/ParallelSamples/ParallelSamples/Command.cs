using System;

namespace ParallelSamples
{
    public record Command(string Option, string Text, Action Action) { }
}
