using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    public interface IBitIo
    {
        Boolean ReadBit();
        void WriteBit(Boolean bit);
        Boolean IsEndOfStream();
    }
}
