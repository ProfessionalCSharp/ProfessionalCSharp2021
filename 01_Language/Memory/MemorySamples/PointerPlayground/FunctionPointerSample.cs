using System;

namespace PointerPlayground
{
    unsafe class FunctionPointerSample
    {
        public static void Calc(delegate* managed<int, int, int> func)
        {
            int result = func(42, 11);
            Console.WriteLine($"function pointer result: {result}");
        }
    }
}
