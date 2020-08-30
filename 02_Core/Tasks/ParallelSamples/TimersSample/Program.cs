using System;
using System.Threading;
using System.Threading.Tasks;

void TimeAction(object? o) =>
    Console.WriteLine($"System.Threading.Timer {DateTime.Now:T}");

using var t1 = new Timer(
    TimeAction, 
    null, 
    dueTime: TimeSpan.FromSeconds(2),
    period: TimeSpan.FromSeconds(3));

Task.Delay(15000).Wait();
