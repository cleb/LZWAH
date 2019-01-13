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
            var cli = new LzwahCli();
            cli.Execute(args);
        }
    }
}
