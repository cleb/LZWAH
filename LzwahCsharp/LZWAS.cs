using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    public class LZWAS
    {
        private Shannon shannon;
        private LZW lzw;
        public LZWAS(IBitIo io)
        {
            shannon = new Shannon(io);
            lzw = new LZW(shannon);
        }
        public void Encode(byte value)
        {
            lzw.Encode(value);
        }
        public void EncoderFinalize()
        {
            lzw.EncoderFinalize();
        }
        public byte Decode()
        {
            return lzw.Decode();
        }

    }
}
