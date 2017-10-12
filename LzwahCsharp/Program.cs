using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LzwahCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream file = File.Open("C:\\Users\\jjuda\\test2.html",FileMode.Open);
            Writer writerIo = new Writer("C:\\Users\\jjuda\\test2.lzwas");
            LZWAS encoder = new LZWAS(writerIo);
            while(file.Position != file.Length)
            {
                Byte input = (Byte)file.ReadByte();
                encoder.Encode(input);
            }
            encoder.EncoderFinalize();
            file.Close();
            writerIo.CloseStream();

            Reader io = new Reader("C:\\Users\\jjuda\\test2.lzwas");
            LZWAS decoder = new LZWAS(io);
            FileStream outfile = File.Open("C:\\Users\\jjuda\\out2.html", FileMode.Create);
            while (!io.IsEndOfStream())
            {
                outfile.WriteByte(decoder.Decode());
            }
            Console.Write("done");
            
        }
    }
}
