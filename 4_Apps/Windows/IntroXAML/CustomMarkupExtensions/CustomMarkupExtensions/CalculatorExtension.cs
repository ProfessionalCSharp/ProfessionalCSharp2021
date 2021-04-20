using Microsoft.UI.Xaml.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMarkupExtensions
{
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }


    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class CalculatorExtension : MarkupExtension
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Operation Operation { get; set; }

        protected override object ProvideValue() =>
            (Operation switch
            {
                Operation.Add => X + Y,
                Operation.Subtract => X - Y,
                Operation.Multiply => X * Y,
                Operation.Divide => X / Y,
                _ => throw new InvalidOperationException()
            }).ToString();
    }

}
