using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzwahCsharp
{
    public class Shannon : IEncoder
    {
        private IBitIo inputOutput;
        List<ShannonItem> values;
        public Shannon(IBitIo io)
        {
            inputOutput = io;
            values = new List<ShannonItem>();
            for(int i = 0; i < 256; i++)
            {
                values.Add(new ShannonItem(i, 1));
            }
        }
        public int GetHalfIndex(double sumHalf, int first, int last)
        {
            long currentSum = 0;
            for (int i = first; i <= last; i++)
            {
                ShannonItem item = values[i];
                currentSum += item.probability;
                if(currentSum > sumHalf)
                {
                    if(i == first)
                    {
                        return i + 1;
                    }
                    return i;
                }
            }
            throw new Exception("Half index not found");
        }
        public int FindValue(long value)
        {
            int first = 0;
            int last = values.Count() - 1;
            for(int i=first;i<= last; i++)
            {
                if(values[i].value == value)
                {
                    return i;
                }
            }
            throw new Exception("Value not found");
        }
        public void EncodeInRange(long value, int first, int last, double sum)
        {
            if (first >= last)
            {
                return;
            }

            double sumHalf = sum / 2;
            double currentSum = 0;
            for (int i=first; i<=last; i++)
            {
                ShannonItem item = values[i];
                currentSum += item.probability;
                if (item.value == value)
                {
                    int halfIndex = GetHalfIndex(sumHalf, first, last);
                    if (i >= halfIndex)
                    {
                        inputOutput.WriteBit(true);
                        EncodeInRange(value, halfIndex, last, sumHalf);
                        return;
                    }
                    else
                    {
                        inputOutput.WriteBit(false);
                        EncodeInRange(value, first, halfIndex - 1, sumHalf);
                        return;
                    }
                }
            }
        }
        public void Encode(Int64 value)
        {
            long sum = GetSumOfProbabilities();
            int first = 0;
            int last = values.Count() - 1;
            EncodeInRange(value,first, last, sum);
            values[FindValue(value)].probability++;
            values = values.OrderByDescending(item => item.probability).ToList();
        }
        public long GetSumOfProbabilities()
        {
            return values.Select(item => item.probability).Aggregate((carry, probability) => carry + probability);
        }

        public long DecodeInRange(int first, int last,double sumHalf)
        {
            if(first == last)
            {
                return values[first].value;
            }
            int halfIndex = GetHalfIndex(sumHalf, first, last);
            bool bit = inputOutput.ReadBit();
            if (bit)
            {
                return DecodeInRange(halfIndex, last, sumHalf / 2);
            }
            else
            {
                return DecodeInRange(first, halfIndex - 1, sumHalf / 2);
            }
            
        }

        public long Decode()
        {
            long sum = GetSumOfProbabilities();
            int first = 0;
            int last = values.Count() - 1;
            long ret = DecodeInRange(first, last,sum /2);
            values[FindValue(ret)].probability++;
            values = values.OrderByDescending(item => item.probability).ToList();
            return ret;


        }
        public void AddValue(Int64 value, long probability = 1)
        {
            values.Add(new ShannonItem(value, probability));
        }
        public long GetProbability(Int64 value)
        {
            return values[FindValue(value)].probability;
        }
    }
}
