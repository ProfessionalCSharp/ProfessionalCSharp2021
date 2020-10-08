using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationSample
{
    [Generator]
    public class ChangeNotificationSourceGenerator : ISourceGenerator
    {
        public void Execute(SourceGeneratorContext context)
        {
            Console.WriteLine("EXEX");
        }

        public void Initialize(InitializationContext context)
        {
            
        }
    }
}
