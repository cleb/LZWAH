using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    public class LZW
    {
        //backend for further encoding (range / huffmann,...)
        public IEncoder encoder;

        private LZWNode root;

        Dictionary<long,LZWNode> allNodes;

        long nextNodeValue = 256;

        long valueToDecode = -1;

        Stack<byte> buffer;

        LZWNode currentNode;

        public LZW(IEncoder encoder)
        {
            this.encoder = encoder;
            this.root = new LZWNode(0,0);
            this.buffer = new Stack<byte>();
            allNodes = new Dictionary<long, LZWNode>();

            for (int i = 0;i<=255;i++)
            {
                LZWNode child = new LZWNode((byte)i, i);
                root.AddChild(child);
                allNodes[i] = child;
            }
            currentNode = this.root;
           
        }

        public void Encode(byte value)
        {
            if (currentNode.ContainsValue(value))
            {
                currentNode = currentNode.GetChild(value);
            }
            else
            {
                LZWNode child = new LZWNode(value, nextNodeValue);
                currentNode.AddChild(child);
                allNodes[nextNodeValue] = child;
                nextNodeValue++;
                encoder.Encode(currentNode.identifier);
                currentNode = root.GetChild(value);
            }
        }    
        public void EncoderFinalize()
        {
            encoder.Encode(currentNode.identifier);
        }
        public byte Decode()
        {
            if(this.buffer.Count == 0)
            {
                DecodeNewValue();
            }
            return this.buffer.Pop();
        }

        private void DecodeNewValue()
        {
            Stack<byte> traceBuffer = new Stack<byte>();
            valueToDecode = encoder.Decode();
            LZWNode traceNode = allNodes[valueToDecode];
            byte lastValue = traceNode.value;
            byte firstValue = 0;
            while (traceNode.parent != null)
            {
                buffer.Push(traceNode.value);
                traceBuffer.Push(traceNode.value);
                firstValue = traceNode.value;
                traceNode = traceNode.parent;
                
            }
            if (!currentNode.ContainsValue(firstValue))
            {
                LZWNode child = new LZWNode(firstValue, nextNodeValue);
                currentNode.AddChild(child);
                allNodes[nextNodeValue] = child;
                currentNode = allNodes[valueToDecode];
                nextNodeValue++;
            } else
            {
                currentNode = currentNode.GetChild(firstValue);
            }
        }
    }
}
