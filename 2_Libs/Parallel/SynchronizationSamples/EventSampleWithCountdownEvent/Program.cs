﻿const int taskCount = 4;

using CountdownEvent cEvent = new(taskCount);
Calculator[] calcs = new Calculator[taskCount];

for (int i = 0; i < taskCount; i++)
{
    calcs[i] = new(cEvent);
    int i1 = i;
    _ = Task.Run(() => calcs[i1].Calculation(i1 + 1, i1 + 3));
}

cEvent.Wait();
Console.WriteLine("all finished");

for (int i = 0; i < taskCount; i++)
{
    Console.WriteLine($"task for {i}, result: {calcs[i].Result}");
}
