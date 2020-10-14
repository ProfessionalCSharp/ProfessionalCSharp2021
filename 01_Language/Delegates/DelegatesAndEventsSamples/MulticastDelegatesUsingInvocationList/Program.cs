using System;

static void One()
{
    Console.WriteLine("One");
    throw new Exception("Error in one");
}

static void Two()
{
    Console.WriteLine("Two");
}

Action d1 = One;
d1 += Two;
Delegate[] delegates = d1.GetInvocationList();
foreach (Action d in delegates)
{
    try
    {
        d();
    }
    catch (Exception)
    {
        Console.WriteLine("Exception caught");
    }
}
