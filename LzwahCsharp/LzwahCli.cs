using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    public class LzwahCli
    {
        CommandLineInterface cli;
        string userOutName;
        List<Action> pipeline;

        private void Encode(string name)
        {
            FileStream file = File.Open(name, FileMode.Open);
            String outName = userOutName != null ? userOutName : (name + ".lzwas");
            Writer writerIo = new Writer(outName);
            LZWAS encoder = new LZWAS(writerIo);
            while (file.Position != file.Length)
            {
                Byte input = (Byte)file.ReadByte();
                encoder.Encode(input);
            }
            encoder.EncoderFinalize();
            file.Close();
            writerIo.CloseStream();
        }
        private void Decode(string name)
        {
            Reader io = new Reader(name);
            LZWAS decoder = new LZWAS(io);
            FileStream outfile = File.Open(userOutName != null ? userOutName : name.Replace(".lzwas",""), FileMode.Create);
            while (!io.IsEndOfStream())
            {
                outfile.WriteByte(decoder.Decode());
            }
            Console.Write("done");
        }


        public LzwahCli()
        {
            pipeline = new List<Action>();
            cli = new CommandLineInterface();
            cli.SetFunction("-c", name => pipeline.Add(() => Encode(name)));
            cli.SetFunction("-d", name => pipeline.Add(() => Decode(name)));
            cli.SetFunction("-o", name => userOutName = name);
        }
        public void Execute(string[] args)
        {
            cli.ParseArgs(args);
            pipeline.ForEach(x => x());
        }
    }
}
