using LzwahCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharpTests
{
    class MockBitWriter : IBitIo
    {
        public List<Boolean> values;
        public int index;
        public MockBitWriter()
        {
            values = new List<Boolean>();
            index = 0;
        }
        public Boolean ReadBit()
        {
            Boolean ret = values[index];
            index++;
            return ret;
        }
        public void WriteBit(Boolean bit)
        {
            values.Add(bit);
        }
        public void Clear()
        {
            values.Clear();
        }
        public bool IsEndOfStream()
        {
            return values.Count() == index;
        }

    }
}
