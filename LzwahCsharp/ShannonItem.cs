using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    class ShannonItem
    {
        public Int64 value;
        public Int64 probability;

        public ShannonItem(long value, long probability)
        {
            this.value = value;
            this.probability = probability;
        }
    }
}
