using System;
using System.Diagnostics;

namespace ThreadingIssues
{
    public class StateObject
    {
        private int _state = 5;
        // private object _sync = new object();

        public bool ChangeState(int loop)
        {
            //            lock (sync)
            {
                if (_state == 5)
                {
                    _state++;
                    if (_state != 6)
                    {
                        Console.WriteLine($"Race condition occured after {loop} loops");
                        // Trace.Fail($"race condition at {loop}");
                        return false;
                    }
                }
                _state = 5;
                return true;
            }
        }
    }
}
