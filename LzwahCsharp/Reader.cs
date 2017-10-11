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
            return reader.ReadBoolean();            
        }

        public void WriteBit(bool bit)
        {
            throw new NotImplementedException();
        }
    }
}
