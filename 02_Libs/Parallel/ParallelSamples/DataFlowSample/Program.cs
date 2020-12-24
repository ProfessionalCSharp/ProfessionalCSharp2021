using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Dataflow;

namespace DataFlowSample
{
    class Program
    {
        static void Main()
        {
            ITargetBlock<string> target = SetupPipeline();
            target.Post(".");
            Console.ReadLine();
        }

        public static IEnumerable<string> GetFileNames(string path)
        {
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs"))
            {
                yield return fileName;
            }
        }

        public static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string? line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //WriteLine($"LoadLines {line}");
                        yield return line;
                    }
                }
            }
        }

        public static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                        yield return word;
                }
            }
        }

        public static ITargetBlock<string> SetupPipeline()
        {
            TransformBlock<string, IEnumerable<string>> fileNamesForPath = new(
              path => GetFileNames(path));

            TransformBlock<IEnumerable<string>, IEnumerable<string>> lines = new(
              fileNames => LoadLines(fileNames));

            TransformBlock<IEnumerable<string>, IEnumerable<string>> words = new(
              lines2 => GetWords(lines2));

            ActionBlock<IEnumerable<string>> display = new(
              coll =>
              {
                  foreach (var s in coll)
                  {
                      Console.WriteLine(s);
                  }
              });
       
            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            return fileNamesForPath;
        }
    }
}
