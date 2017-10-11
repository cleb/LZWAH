using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LzwahCsharp
{
    public class Writer : IBitIo
    {
        private BinaryWriter writer;
        private FileStream file;
        public Writer(string fileName)
        {
            file = File.Open(fileName, FileMode.Create);
            writer = new BinaryWriter(file);
        }
        public bool IsEndOfStream()
        {
            throw new NotImplementedException();
        }

        public bool ReadBit()
        {
            throw new NotImplementedException();
        }

        public void WriteBit(bool bit)
        {
            writer.Write(bit);
        }
        public void CloseStream()
        {
            file.Close();
        }
    }
}
