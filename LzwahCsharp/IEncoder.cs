using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    public interface IEncoder
    {
        void Encode(Int64 value);
        long Decode();
        void AddValue(Int64 value);
    }
}
