using System.Threading.Tasks;

ReaderWriter rw = new();
TaskFactory taskFactory = new(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
Task[] tasks = new Task[6];
tasks[0] = taskFactory.StartNew(rw.WriterMethod, 1);
await Task.Delay(5);
tasks[1] = taskFactory.StartNew(rw.ReaderMethod, 1);
tasks[2] = taskFactory.StartNew(rw.ReaderMethod, 2);
tasks[3] = taskFactory.StartNew(rw.WriterMethod, 2);
tasks[4] = taskFactory.StartNew(rw.ReaderMethod, 3);
tasks[5] = taskFactory.StartNew(rw.ReaderMethod, 4);

Task.WaitAll(tasks);
