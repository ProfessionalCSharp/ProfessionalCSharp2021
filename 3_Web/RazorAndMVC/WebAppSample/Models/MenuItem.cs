using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSample.Models
{
    public record MenuItem(int Id, string Text, double Price, DateTime Date);

}
