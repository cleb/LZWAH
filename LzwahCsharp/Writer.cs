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
        private byte buffer = 0;
        private int bufferLength = 0;
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
            buffer <<= 1;
            if(bit)
            {
                buffer |= 1;
            }
            bufferLength++;
            if(bufferLength == 8)
            {
                writer.Write(buffer);
                bufferLength = 0;
            }
            
        }
        public void CloseStream()
        {
            while (bufferLength > 0)
            {
                WriteBit(false);
            }
            file.Close();
        }
    }
}
