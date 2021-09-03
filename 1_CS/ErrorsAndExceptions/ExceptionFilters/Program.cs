try
{
    ThrowWithErrorCode(405);
}
catch (MyCustomException ex) when (ex.ErrorCode == 405)
{
    Console.WriteLine($"Exception caught with filter {ex.Message} and {ex.ErrorCode}");
}
catch (MyCustomException ex)
{
    Console.WriteLine($"Exception caught {ex.Message} and {ex.ErrorCode}");
}

Console.ReadLine();

void ThrowWithErrorCode(int code)
{
    throw new MyCustomException("Error in Foo") { ErrorCode = code };
}
