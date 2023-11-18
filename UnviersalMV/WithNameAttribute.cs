using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnviersalMV
{
    [AttributeUsage(AttributeTargets.Property)]
    public class WithNameAttribute(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
