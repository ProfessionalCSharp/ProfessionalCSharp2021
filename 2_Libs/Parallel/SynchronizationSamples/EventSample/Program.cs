﻿const int taskCount = 4;

ManualResetEventSlim[] mEvents = new ManualResetEventSlim[taskCount];
WaitHandle[] waitHandles = new WaitHandle[taskCount];
Calculator[] calcs = new Calculator[taskCount];

for (int i = 0; i < taskCount; i++)
{
    int i1 = i;
    mEvents[i] = new(false);
    waitHandles[i] = mEvents[i].WaitHandle;
    calcs[i] = new(mEvents[i]);
    _ = Task.Run(() => calcs[i1].Calculation(i1 + 1, i1 + 3));
}

for (int i = 0; i < taskCount; i++)
{
    //   int index = WaitHandle.WaitAny(mEvents.Select(e => e.WaitHandle).ToArray());
    int index = WaitHandle.WaitAny(waitHandles);
    if (index == WaitHandle.WaitTimeout)
    {
        Console.WriteLine("Timeout!!");
    }
    else
    {
        mEvents[index].Reset();
        Console.WriteLine($"finished task for {index}, result: {calcs[index].Result}");
    }
}

for (int i = 0; i < taskCount; i++)
{
    mEvents[i].Dispose();
    waitHandles[i].Dispose();
}
