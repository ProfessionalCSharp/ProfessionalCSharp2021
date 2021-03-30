using System;
using System.Threading;
using System.Threading.Tasks;

void TimeAction(object? o) =>
    Console.WriteLine($"System.Threading.Timer {DateTime.Now:T}");

using Timer t1 = new(
    TimeAction, 
    null, 
    dueTime: TimeSpan.FromSeconds(2),
    period: TimeSpan.FromSeconds(3));

Task.Delay(15000).Wait();
