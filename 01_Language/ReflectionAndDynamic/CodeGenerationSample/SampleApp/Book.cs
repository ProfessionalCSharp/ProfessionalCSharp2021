using System.ComponentModel;
using CodeGenerationSample;

namespace SampleApp
{
    [PropertyWithNotification("Title", typeof(string))]
    [PropertyWithNotification("Publisher", typeof(string))]
    public partial class Book
    {


    }
}
