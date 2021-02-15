using System;
using System.IO;

public class ColdCallFileReader : IDisposable
{
    private FileStream? _fileStream;
    private StreamReader? _streamReader;
    private uint _nPeopleToRing;
    private bool _isDisposed = false;
    private bool _isOpen = false;

    public void Open(string fileName)
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ColdCallFileReader));
        }

        _fileStream = new(fileName, FileMode.Open);
        _streamReader = new(_fileStream);

        try
        {
            string? firstLine = _streamReader.ReadLine();
            if (firstLine != null)
            {
                _nPeopleToRing = uint.Parse(firstLine);
                _isOpen = true;
            }
        }
        catch (FormatException ex)
        {
            throw new ColdCallFileFormatException($"First line isn't an integer {ex}");
        }
    }

    public void ProcessNextPerson()
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ColdCallFileReader));
        }

        if (!_isOpen)
        {
            throw new UnexpectedException("Attempted to access coldcall file that is not open");
        }

        try
        {
            string? name = _streamReader?.ReadLine();
            if (name is null)
            {
                throw new ColdCallFileFormatException("Not enough names");
            }
            if (name[0] is 'B')
            {
                throw new SalesSpyFoundException(name);
            }
            Console.WriteLine(name);
        }
        catch (SalesSpyFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
        }
    }

    public uint NPeopleToRing
    {
        get
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(ColdCallFileReader));
            }

            if (!_isOpen)
            {
                throw new UnexpectedException("Attempted to access cold–call file that is not open");
            }

            return _nPeopleToRing;
        }
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        _isOpen = false;

        _streamReader?.Dispose();
        _streamReader = null;
    }
}