class Program
{
    unsafe public static void Main()
    {
        string? userInput;
        int size;
        do
        {
            Console.Write($"How big an array do you want? {Environment.NewLine}>");
            userInput = Console.ReadLine();
        } while (!int.TryParse(userInput, out size));

        long* pArray = stackalloc long[size];
        for (int i = 0; i < size; i++)
        {
            pArray[i] = i * i;
        }

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Element {i} = {*(pArray + i)}");
        }

        Console.ReadLine();
    }
}
