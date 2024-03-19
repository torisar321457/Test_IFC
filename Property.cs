using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_IFC
{
    internal class Property
    {
        public string Name { get; set; }
        public object? Value { get; set; }
        public Property(string name, object? value)
        {
            Name = name;
            Value = value;
        }
        public override string ToString() { return Name + "|" + Value?.ToString(); }

    }
}
