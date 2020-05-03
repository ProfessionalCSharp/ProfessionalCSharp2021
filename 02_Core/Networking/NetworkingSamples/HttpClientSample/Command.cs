using System;
using System.Threading.Tasks;

namespace HttpClientSample
{
    internal class Command
    {
        public Command(string option, string text, Func<Task> asyncAction)
            => (Option, Text, ActionAsync) = (option, text, asyncAction);

        public string Option { get; }
        public string Text { get; }
        public Func<Task> ActionAsync { get; }
    }
}
