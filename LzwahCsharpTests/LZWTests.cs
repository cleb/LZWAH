using Microsoft.VisualStudio.TestTools.UnitTesting;
using LzwahCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp.Tests
{
    class MockEncoder : IEncoder
    {
        public List<long> values;
        public MockEncoder()
        {
            this.values = new List<long>();
        }
        public void Encode(long value)
        {
            values.Add(value);
        }
        public long Decode()
        {
            return values[values.Count()-1];
        }
    }
    [TestClass()]
    public class LZWTests
    {
        [TestMethod()]
        public void EncodeTest()
        {
            MockEncoder encoder = new MockEncoder();
            LZW lzw = new LZW(encoder);
            lzw.Encode((42));
            lzw.Encode((43));
            lzw.Encode((42));
            lzw.Encode((43));
            lzw.Encode((44));
            Assert.AreEqual(encoder.values[0], 42);
            Assert.AreEqual(encoder.values[1], 43);
            Assert.AreEqual(encoder.values[2], 256);


        }
    }
}