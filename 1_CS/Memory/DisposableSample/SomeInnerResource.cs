﻿public class SomeInnerResource : IDisposable
{
    public SomeInnerResource() =>
        Console.WriteLine("simulation to allocate native memory");

    public void Foo()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        Console.WriteLine($"{nameof(SomeInnerResource)}.{nameof(Foo)}");
    }

    private bool isDisposed = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            Console.WriteLine("simulation to release native memory");
            // TODO: set large fields to null.

            isDisposed = true;
        }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    ~SomeInnerResource() => Dispose(false);

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(true);
        // TODO: uncomment the following line if the finalizer is overridden above.
        GC.SuppressFinalize(this);
    }
}
