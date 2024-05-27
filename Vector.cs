using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vector
{
    public class Vector<T> where T : IComparable<T>
    {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extended the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 10;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array. 
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity { get; private set; } = 0;

        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[data.Length + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == data.Length) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }

        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }

        // TODO:********************************************************************************************
        // TODO: Your task is to implement all the remaining methods.
        

        public void Insert(int index, T item)
        {
            // If index is outside the range of the current Vector then this method should throw an IndexOutOfRangeException.
            if (index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException();
            }

            // If count = capacity, increase by reallocatiing internal array and copy to larger array
            if (Count == Capacity)
            {
                ExtendData(DEFAULT_CAPACITY);
            }

            // If index is equal to Count, item add the item to the end of the Vector<T>.
            if (index == Count)
            {
                data[index] = item;
            }
            // If index is somewhere internal to the vector then starting at the end
            else
            {
                // shuffle each element along until you reach the index location
                for (int i = Count; i > index; i--)
                {
                    data[i] = data[i - 1];
                }

                // place the item in the vector at that point.
                data[index] = item;
            }

            // Increment the Count
            Count++;

        }

        public void Clear()
        {
            // Set all elements to default value
            Array.Clear(data, 0, Count);

            // Reset the Count back to 0
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public bool Remove(T item)
        {
            // Find the index of the specified item using the IndexOf method
            int index = IndexOf(item);

            // If the item is not found, return false
            if (index == -1)
            {
                return false;
            }

            // Remove the element at the found index using the RemoveAt method
            RemoveAt(index);

            // Return true to indicate that the item was successfully removed
            return true;
        }

        public void RemoveAt(int index)
        {
            // Throw IndexOutOfRangeException if the index is invalid
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            // Shift elements to the left to fill the gap left by the removed element
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
            }

            // Set the last element to default value to remove the reference
            data[Count - 1] = default;

            // Reduce the Count by 1
            Count--;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            for (int i = 0; i < Count; i++)
            {
                sb.Append(data[i]);

                // if there are more elements add a comma (,) before you add the next element.
                if (i < Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("]");

            return sb.ToString();
        }




        public ISorter Sorter { set; get; } = new DefaultSorter();

        internal class DefaultSorter : ISorter
        {
            public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
            {
                if (comparer == null) comparer = Comparer<K>.Default;
                Array.Sort(sequence, comparer);
            }
        }

        public void Sort()
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            Sorter.Sort(data, null);
        }

        public void Sort(IComparer<T> comparer)
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            if (comparer == null) Sorter.Sort(data, null);
            else Sorter.Sort(data, comparer);
        }
    }
}
