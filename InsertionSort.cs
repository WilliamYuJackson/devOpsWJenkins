using System;
using System.Collections.Generic;

namespace Vector
{
    public class InsertionSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            for (int i = 1; i < sequence.Length; i++)
            {
                K key = sequence[i];
                int j = i - 1;

                while (j >= 0 && comparer.Compare(sequence[j], key) > 0)
                {
                    sequence[j + 1] = sequence[j];
                    j--;
                }

                sequence[j + 1] = key;
            }
        }
    }
}
