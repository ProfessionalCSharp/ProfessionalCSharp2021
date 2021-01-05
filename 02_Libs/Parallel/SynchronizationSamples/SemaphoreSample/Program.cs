using System;
using System.Threading;
using System.Threading.Tasks;

int taskCount = 6;
int semaphoreCount = 3;
SemaphoreSlim semaphore = new(semaphoreCount, semaphoreCount);
Task[] tasks = new Task[taskCount];

for (int i = 0; i < taskCount; i++)
{
    tasks[i] = Task.Run(() => TaskMain(semaphore));
}

Task.WaitAll(tasks);

Console.WriteLine("All tasks finished");


void TaskMain(SemaphoreSlim semaphore)
{
    bool isCompleted = false;
    while (!isCompleted)
    {
        if (semaphore.Wait(600))
        {
            try
            {
                Console.WriteLine($"Task {Task.CurrentId} locks the semaphore");
                Task.Delay(2000).Wait();
            }
            finally
            {
                Console.WriteLine($"Task {Task.CurrentId} releases the semaphore");
                semaphore.Release();
                isCompleted = true;
            }
        }
        else
        {
            Console.WriteLine($"Timeout for task {Task.CurrentId}; wait again");
        }
    }
}
