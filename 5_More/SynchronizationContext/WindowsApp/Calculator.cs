namespace WindowsApp;

public class Calculator
{
    public async Task<int> AddAsync(int x, int y)
    {
        await Task.Delay(4000);
        return x + y;
    }

    public async Task<int> SubtractAsync(int x, int y)
    {
        await Task.Delay(3000);
        return x - y;
    }

    public int BlockingAdd(int x, int y)
    {
        Thread.Sleep(2000);
        return x + y;
    }
}
