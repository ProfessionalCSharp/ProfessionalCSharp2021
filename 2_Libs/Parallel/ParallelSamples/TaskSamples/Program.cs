namespace TaskSamples;

class Program
{
    private static readonly Command[] s_commands =
    {
            new Command("-p", nameof(TasksUsingThreadPool), TasksUsingThreadPool),
            new Command("-s", nameof(RunSynchronousTask), RunSynchronousTask),
            new Command("-l", nameof(LongRunningTask), LongRunningTask),
            new Command("-r", nameof(TaskWithResult), TaskWithResultDemo),
            new Command("-c", nameof(ContinuationTasks), ContinuationTasks),
            new Command("-pc", nameof(ParentAndChild), ParentAndChild),
        };

    static void Main(string[] args)
    {
        if (args.Length == 0 || args.Length > 1 || !s_commands.Select(c => c.Option).Contains(args[0]))
        {
            ShowUsage();
            return;
        }

        s_commands.Single(c => c.Option == args[0]).Action();

        Console.ReadLine();
    }

    private static void ShowUsage()
    {
        Console.WriteLine("Usage: TaskSamples [options]");
        Console.WriteLine();
        foreach (var command in s_commands)
        {
            Console.WriteLine($"{command.Option} {command.Text}");
        }
    }

    public static void ParentAndChild()
    {
        Task parent = new(ParentTask);
        parent.Start();
        Task.Delay(2000).Wait();
        Console.WriteLine(parent.Status);
        Task.Delay(4000).Wait();
        Console.WriteLine(parent.Status);
    }

    private static void ParentTask()
    {
        Console.WriteLine($"task id {Task.CurrentId}");
        Task child = new(ChildTask);
        child.Start();
        Task.Delay(1000).Wait();
        Console.WriteLine("parent started child");
    }

    private static void ChildTask()
    {
        Console.WriteLine("child");
        Task.Delay(5000).Wait();
        Console.WriteLine("child finished");
    }

    public static void ContinuationTasks()
    {
        Task t1 = new(DoOnFirst);
        Task t2 = t1.ContinueWith(DoOnSecond);
        Task t3 = t1.ContinueWith(DoOnSecond);
        Task t4 = t2.ContinueWith(DoOnSecond);
        t1.Start();
    }

    private static void DoOnFirst()
    {
        Console.WriteLine($"doing some task {Task.CurrentId}");
        Task.Delay(3000).Wait();
    }

    private static void DoOnSecond(Task t)
    {
        Console.WriteLine($"task {t.Id} finished");
        Console.WriteLine($"this task id {Task.CurrentId}");
        Console.WriteLine("do some cleanup");
        Task.Delay(3000).Wait();
    }

    public static void TaskWithResultDemo()
    {
        Task<(int Result, int Remainder)> t1 = new(TaskWithResult, (8, 3));
        t1.Start();
        Console.WriteLine(t1.Result);
        t1.Wait();
        Console.WriteLine($"result from task: {t1.Result.Result} {t1.Result.Remainder}");
    }

    private static (int Result, int Remainder) TaskWithResult(object? division)
    {
        if (division is ValueTuple<int, int> div)
        {
            (int x, int y) = div;
            int result = x / y;
            int remainder = x % y;
            Console.WriteLine("task creates a result...");

            return (result, remainder);
        }
        else
        {
            throw new ArgumentException($"{nameof(division)} needs to be a ValueTuiple<int, int>");
        }
    }

    public static void TasksUsingThreadPool()
    {
        TaskFactory tf = new();
        Task t1 = tf.StartNew(TaskMethod, "using a task factory");
        Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");
        Task t3 = new(TaskMethod, "using a task constructor and Start");
        t3.Start();
        Task t4 = Task.Run(() => TaskMethod("using the Run method"));
    }

    public static void RunSynchronousTask()
    {
        TaskMethod("just the main thread");
        Task t1 = new(TaskMethod, "run sync");
        t1.RunSynchronously();
    }

    public static void LongRunningTask()
    {
        Task t1 = new(TaskMethod, "long running",
          TaskCreationOptions.LongRunning);
        t1.Start();
    }

    public static void TaskMethod(object? o)
    {
        Log(o?.ToString() ?? string.Empty);
    }

    private static readonly object s_logLock = new();

    public static void Log(string title)
    {
        lock (s_logLock)
        {
            Console.WriteLine(title);
            Console.WriteLine($"Task id: {Task.CurrentId?.ToString() ?? "no task"}, thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine();
        }
    }
}
