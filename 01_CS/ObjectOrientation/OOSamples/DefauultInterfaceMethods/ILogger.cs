using System;

public interface ILogger
{
    void Log(string message);
    public void Log(Exception ex) => Log(ex.Message);
}
