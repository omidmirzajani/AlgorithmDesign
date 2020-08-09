using System;
using System.Collections.Generic;

namespace A3
{
    public class MinHeap
    {
        public readonly List<long>[] _elements;
        public long _size;

        public MinHeap(long s)
        {
            _elements = new List<long>[s];
        }

        private long GetLeftChildIndex(long elementIndex) => 2 * elementIndex + 1;
        private long GetRightChildIndex(long elementIndex) => 2 * elementIndex + 2;
        private long GetParentIndex(long elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(long elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(long elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(long elementIndex) => elementIndex == 0;

        private List<long> GetLeftChild(long elementIndex) => _elements[(int)GetLeftChildIndex(elementIndex)];
        private List<long> GetRightChild(long elementIndex) => _elements[(int)GetRightChildIndex(elementIndex)];
        private List<long> GetParent(long elementIndex) => _elements[(int)GetParentIndex(elementIndex)];

        private void Swap(long firstIndex, long secondIndex)
        {
            var temp = _elements[(int)firstIndex];
            _elements[(int)firstIndex] = _elements[(int)secondIndex];
            _elements[(int)secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public List<long> Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public List<long> Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[(int)_size - 1];

            _size--;
            ReCalculateDown();
            return result;
        }

        public void Add(List<long> element)
        {
            if (_size == _elements.Length)
                throw new NullReferenceException();

            _elements[_size] = element;
            _size++;

            ReCalculateUp();
        }

        private void ReCalculateDown()
        {
            long index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index)[0] < GetLeftChild(index)[0])
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[(int)smallerIndex][0] >= _elements[(int)index][0])
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        public void ReCalculateUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && _elements[(int)index][0] < GetParent(index)[0])
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}
