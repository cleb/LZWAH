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
        public int index = 0;
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
            return values[index++];
        }
        public void AddValue(Int64 value)
        {

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

        [TestMethod()]
        public void DecodeTest()
        {
            MockEncoder encoder = new MockEncoder();
            encoder.values.Add(42);
            encoder.values.Add(43);
            encoder.values.Add(256);
            LZW lzw = new LZW(encoder);
            Assert.AreEqual(42, lzw.Decode());
            Assert.AreEqual(43, lzw.Decode());
            Assert.AreEqual(42, lzw.Decode());
            Assert.AreEqual(43, lzw.Decode());
        }

        [TestMethod()]
        public void EncodeDecodeTest()
        {
            MockEncoder encoder = new MockEncoder();
            LZW lzw = new LZW(encoder);
            foreach (byte character in "Lorem ipsum dolor sit amet consectetur adepiscig nullam")
            {
                lzw.Encode(character);
            }
            lzw.EncoderFinalize();
            LZW lzwDecoder = new LZW(encoder);
            
            String output = "";
            foreach (byte character in "Lorem ipsum dolor sit amet consectetur adepiscig nullam")
            {
                byte actual = lzwDecoder.Decode();
                output += (char)actual;
                Assert.AreEqual(character, actual);
            }
        }
    }
}