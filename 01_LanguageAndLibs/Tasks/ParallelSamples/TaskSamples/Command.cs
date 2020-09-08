using System;

namespace TaskSamples
{
    public record Command(string Option, string Text, Action Action) { }
}
