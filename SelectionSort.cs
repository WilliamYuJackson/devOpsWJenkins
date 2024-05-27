using System;
using System.Collections.Generic;

namespace Vector
{
    public class SelectionSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;

            for (int i = 0; i < sequence.Length - 1; i++)
            {
                int minIndex = i; // searches for the minIndex
                for (int j = i + 1; j < sequence.Length; j++)
                {
                    if (comparer.Compare(sequence[j], sequence[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    K temp = sequence[i];
                    sequence[i] = sequence[minIndex];
                    sequence[minIndex] = temp;
                }
            }
        }
    }
}
