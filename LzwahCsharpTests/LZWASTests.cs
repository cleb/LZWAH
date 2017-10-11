using Microsoft.VisualStudio.TestTools.UnitTesting;
using LzwahCsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LzwahCsharpTests;

namespace LzwahCsharp.Tests
{
    [TestClass()]
    public class LZWASTests
    {
        private const string V = "Corpus omne perseverare in statu suo quiescendi vel movendi uniformiter in directum, nisi quatenus illud a viribus impressis cogitur statum suum mutare." +
                        "Mutationem motus proportionalem esse vi motrici impressae et fieri secundam lineam rectam qua vis illa imprimitur." +
                        "Actioni contrariam semper et aequalem esse reactionem; sive: corporum duorum actiones in se mutuo semper esse aequales et in partes contrarias dirigi.";

        [TestMethod()]
        public void LZWASTest()
        {
            MockBitWriter writer = new MockBitWriter();
            LZWAS encoder = new LZWAS(writer);
            LZWAS decoder = new LZWAS(writer);
            foreach (byte character in V)
            {
                encoder.Encode(character);
            }
            encoder.EncoderFinalize();
            string output = "";
            foreach (byte character in V)
            {
                byte actual = decoder.Decode();
                output += (char)actual;
                Assert.AreEqual(character, actual);
            }
        }

        [TestMethod()]
        public void SameCharacterTest()
        {
            MockBitWriter writer = new MockBitWriter();
            LZWAS encoder = new LZWAS(writer);
            LZWAS decoder = new LZWAS(writer);
            for(int i=0;i<10;i++)
            {
                encoder.Encode(42);
            }
            encoder.EncoderFinalize();
            
            for (int i = 0; i < 10; i++)
            {
                byte actual = decoder.Decode();
                Assert.AreEqual(42, actual);
            }
        }
    }
}