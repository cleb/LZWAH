using Microsoft.VisualStudio.TestTools.UnitTesting;
using LzwahCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp.Tests
{
    class MockBitWriter : IBitIo
    {
        public List<Boolean> values;
        public int index;
        public MockBitWriter()
        {
            values = new List<Boolean>();
            index = 0;
        }
        public Boolean ReadBit()
        {
            Boolean ret = values[index];
            index++;
            return ret;
        }
        public void WriteBit(Boolean bit)
        {
            values.Add(bit);
        }
        public void Clear()
        {
            values.Clear();
        }
        public bool IsEndOfStream()
        {
            return values.Count() == index;
        }

    }
    [TestClass()]
    public class ShannonTests
    {
        [TestMethod()]
        public void EncodeTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetSumOfProbabilitiesTest()
        {
            Shannon shannon = new Shannon(new MockBitWriter());
            Assert.AreEqual(shannon.GetSumOfProbabilities(), 256);
        }

        [TestMethod()]
        public void GetHalfIndexTest()
        {
            Shannon shannon = new Shannon(new MockBitWriter());
            Assert.AreEqual(shannon.GetHalfIndex(127, 0, 255), 127);
        }

        [TestMethod()]
        public void FindValueTest()
        {
            Shannon shannon = new Shannon(new MockBitWriter());
            for (int i = 0; i < 256; i++)
            {
                Assert.AreEqual(shannon.FindValue(i), i);
            }

        }

        [TestMethod()]
        public void EncodeTest1()
        {
            MockBitWriter io = new MockBitWriter();
            Shannon shannon = new Shannon(io);
            shannon.Encode(5);
            Assert.AreEqual(io.values.Count(), 8);
            Assert.AreEqual(io.values[0], false);
            Assert.AreEqual(io.values[1], false);
            Assert.AreEqual(io.values[2], false);
            Assert.AreEqual(io.values[3], false);
            Assert.AreEqual(io.values[4], false);
            Assert.AreEqual(io.values[5], true);
            Assert.AreEqual(io.values[6], false);
            Assert.AreEqual(io.values[7], true);
            io.Clear();
            shannon.Encode(5);
            Assert.AreEqual(io.values.Count(), 7);
            Assert.AreEqual(io.values[0], false);
            Assert.AreEqual(io.values[1], false);
            Assert.AreEqual(io.values[2], false);
            Assert.AreEqual(io.values[3], false);
            Assert.AreEqual(io.values[4], false);
            Assert.AreEqual(io.values[5], false);
            Assert.AreEqual(io.values[6], false);
        }

        [TestMethod()]
        public void DecodeTest()
        {
            MockBitWriter io = new MockBitWriter();
            Shannon shannon = new Shannon(io);

            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(true);
            io.WriteBit(false);
            io.WriteBit(true);
            Assert.AreEqual(shannon.Decode(), 5);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            io.WriteBit(false);
            Assert.AreEqual(shannon.Decode(), 5);
        }

        [TestMethod()]
        public void EncodeDecodeTest()
        {
            MockBitWriter io = new MockBitWriter();
            Shannon encoder = new Shannon(io);
            foreach (char character in "Lorem ipsum dolor sit amet consectetur adepiscig nullam")
            {
                encoder.Encode(character);
            }
            Shannon decoder = new Shannon(io);
            foreach (char character in "Lorem ipsum dolor sit amet consectetur adepiscig nullam")
            {
                Assert.AreEqual(decoder.Decode(),character);
            }
        }
    }
}