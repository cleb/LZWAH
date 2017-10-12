using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LzwahCsharp;

namespace LzwahCsharpTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLWrirerReader()
        {
            Writer writer = new Writer("tmp.lzwah");
            bool[] testInput = new bool[] { false, true, false, true, false, true, false, false };
            foreach(bool bit in testInput)
            {
                writer.WriteBit(bit);
            }
            writer.CloseStream();

            Reader reader = new Reader("tmp.lzwah");
            foreach (bool bit in testInput)
            {
                bool inputBit = reader.ReadBit();
                Assert.AreEqual(bit, inputBit);
            }

        }
    }
}
