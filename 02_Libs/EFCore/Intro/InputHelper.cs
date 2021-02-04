using System;

class InputHelper
{
    public static bool TrueFalse(string question)
    {
        bool result;
        bool inputValid = false;
        do
        {
            Console.Write($"{question }");
            string? input = Console.ReadLine();
            inputValid = bool.TryParse(input, out result);
        } while (!inputValid);
        return result;
    }
}

