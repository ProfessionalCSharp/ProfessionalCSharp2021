using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace DynamicFileReader
{
    public class DynamicFileHelper
    {
        public IEnumerable<dynamic> ParseFile(string fileName)
        {
            List<dynamic> retList = new();
            StreamReader? reader = OpenFile(fileName);
            if (reader != null)
            {
                string[] headerLine = reader.ReadLine()?.Split(',').Select(s => s.Trim()).ToArray() ?? throw new InvalidOperationException("reader.ReadLine returned null");
                while (reader.Peek() > 0)
                {
                    string[] dataLine = reader.ReadLine()?.Split(',') ?? throw new InvalidOperationException("reader.Readline returned null");
                    dynamic dynamicEntity = new ExpandoObject();
                    for (int i = 0; i < headerLine.Length; i++)
                    {
                        ((IDictionary<string, object>)dynamicEntity).Add(headerLine[i], dataLine[i]);
                    }
                    retList.Add(dynamicEntity);
                }
            }
            return retList;
        }

        private StreamReader? OpenFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new StreamReader(File.OpenRead(fileName));
            }
            return null;
        }
    }
}
