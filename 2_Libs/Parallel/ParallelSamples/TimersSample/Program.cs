static void TimeAction(object? o) =>
    Console.WriteLine($"System.Threading.Timer {DateTime.Now:T}");

using Timer t1 = new(
    TimeAction, 
    null, 
    dueTime: TimeSpan.FromSeconds(2),
    period: TimeSpan.FromSeconds(3));

await Task.Delay(15000);
