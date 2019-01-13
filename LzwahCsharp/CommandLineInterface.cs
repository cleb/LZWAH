using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    class CommandLineInterface
    {
        private Dictionary<String, Action<string>> functions;
        public CommandLineInterface()
        {
            functions = new Dictionary<string, Action<string>>();
        }
        public void SetFunction(String identifier, Action<string> function)
        {
            functions[identifier] = function;
        }
        public void ParseArgs(String[] args)
        {
            if(args.Length < 2)
            {
                throw new ArgumentException("Not enough command line arguments");
            }
            for(int i = 0; i< args.Length; i+=2)
            {
                if (functions.ContainsKey(args[i]))
                {
                    functions[args[i]](args[i+1]);
                }
            }
        }
    }
}
