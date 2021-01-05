using System;

namespace ThreadingIssues
{

    public class TaskWithDeadlock
    {
        public TaskWithDeadlock(StateObject s1, StateObject s2) => (_s1, _s2) = (s1, s2);

        private readonly StateObject _s1;
        private readonly StateObject _s2;
        
        public void Deadlock1()
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine("1 - waiting for s1");
                lock (_s1)
                {
                    Console.WriteLine("1 - s1 locked, waiting for s2");
                    lock (_s2)
                    {
                        Console.WriteLine("1 - s1 and s2 locked, now go on...");
                        _s1.ChangeState(i);
                        _s2.ChangeState(i++);
                        Console.WriteLine($"1 still running, i: {i}");
                    }
                }
            }
        }

        public void Deadlock2()
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine("2 - waiting for s2");

                lock (_s2)
                {
                    Console.WriteLine("2 - s2 locked, waiting for s1");
                    lock (_s1)
                    {
                        Console.WriteLine("2 - s1 and s2 locked, now go on...");
                        _s1.ChangeState(i);
                        _s2.ChangeState(i++);
                        Console.WriteLine($"2 still running, i: {i}");
                    }
                }
            }
        }
    }
}