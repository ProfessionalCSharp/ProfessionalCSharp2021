using Books.Models;
using System;
using System.Collections.Generic;

namespace Books.Services
{
    public class SampleChapters
    {

        private string[] _sampleTitles = new[]
        {
            ".NET Applications and Tools",
            "Core C#",
            "Classes, Structs, Tuples, and Records",
            "Object-Oriented Programming with C#",
            "Operators and Casts",
            "Arrays",
            "Delegates, Lambdas, and Events",
            "Collections",
            "ADO.NET and Transactions"
        };

        private int[] _chapterNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 25 };

        private int[] _pageCounts = { 35, 42, 33, 20, 24, 38, 20, 32, 44 };

        public IEnumerable<BookChapter> GetSampleChapters()
        {
            List<BookChapter> chapters = new();
            for (int i = 0; i < 8; i++)
            {
                chapters.Add(new BookChapter(Guid.NewGuid(), _chapterNumbers[i], _sampleTitles[i], _pageCounts[i]));
            }
            return chapters;
        }
    }
}
