using System;

namespace UnitTestingSamples
{
    public class StringSample
    {
        public StringSample(string init)
        {
            if (init == null)
                throw new ArgumentNullException(nameof(init));
            _init = init;
        }

        private string _init;
        public string GetStringDemo(string? first, string? second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (string.IsNullOrEmpty(first))
            {
                throw new ArgumentException("empty string is allowed", first);
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            if (second.Length > first.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(second),
                  "must be shorter than second");
            }

            int startIndex = first.IndexOf(second);
            if (startIndex < 0)
            {
                return $"{first} not found in {second}";
            }
            else if (startIndex < 5)
            {
                string result = first.Remove(startIndex, second.Length);
                return $"removed {second} from {first}: {result}";
            }
            else
            {
                return _init.ToUpperInvariant();
            }
        }
    }
}
