using System;
using System.Threading.Tasks;

namespace HttpClientSample
{
    internal record Command(string Option, string Text, Func<Task> ActionAsync)
    {
    }
}
