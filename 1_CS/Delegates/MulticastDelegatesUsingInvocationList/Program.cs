static void One()
{
    Console.WriteLine("One");
    throw new Exception("Error in One");
}

static void Two() => Console.WriteLine("Two");

Action d1 = One;
d1 += Two;

Delegate[] delegates = d1.GetInvocationList();
foreach (Action d in delegates.Cast<Action>())
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
