using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LzwahCsharp
{
    public class Reader : IBitIo
    {
        private BinaryReader reader;
        private FileStream file;
        private byte buffer = 0;
        private int bufferLength = 0;
        public Reader(string fileName)
        {
            file = File.Open(fileName, FileMode.Open);
            reader = new BinaryReader(file);
        }
        public bool IsEndOfStream()
        {
            return file.Position == file.Length;
        }

        public bool ReadBit()
        {
            if(bufferLength == 0)
            {
                buffer = reader.ReadByte();
                bufferLength = 8;
            }
            bool ret = (buffer & 128) == 128;
            buffer <<= 1;
            bufferLength--;
            return ret;

        }

        public void WriteBit(bool bit)
        {
            throw new NotImplementedException();
        }
    }
}
