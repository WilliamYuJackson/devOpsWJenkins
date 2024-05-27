using System;
using System.Collections.Generic;
namespace Vector
{
    public class BubbleSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
                       
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                for (int j = 0; j < sequence.Length - i - 1; j++)
                {
                    if (comparer.Compare(sequence[j], sequence[j + 1]) > 0)
                    {
                        K temp = sequence[j]; // index j becomes temp
                        sequence[j] = sequence[j + 1]; // j + 1 becomes j
                        sequence[j + 1] = temp; // temp becomes j + 1 | swapped
                    }
                }
            }
        }
    }
}
