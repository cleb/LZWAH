using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    class LZWNode
    {
        public byte value;
        public long identifier;
        Dictionary<byte, LZWNode> children;
        public LZWNode(byte value, long identifier)
        {
            this.value = value;
            this.identifier = identifier;
            this.children = new Dictionary<byte, LZWNode>();
        }
        public void AddChild(LZWNode child)
        {
            this.children[child.value] = child;
        }
        public bool ContainsValue(byte value)
        {
            return this.children.ContainsKey(value);
        }
        public LZWNode GetChild(byte value)
        {
            return this.children[value];
        }
    }
}
