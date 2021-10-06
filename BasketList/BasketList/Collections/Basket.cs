using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketList.Comparers;

namespace BasketList.Collections
{
    public class Basket<T> : IList<T>
    {
        private T[] _array;
        private int _count;
        private IComparer<T> _comparer;

        public Basket()
        {
            _count = 0;
            _array = new T[1];
            _comparer = new DefaultComparer<T>();
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _array.IsReadOnly;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index > _count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                return _array[index];
            }
            set
            {
                if (index > _count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                _array[index] = value;
            }
        }

        public void Add(T item)
        {
            CheckSpace();

            _array[_count++] = item;
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < _array.Length; i++)
            {
                if (_array[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > _count)
            {
                throw new ArgumentOutOfRangeException();
            }

            CheckSpace();

            for (var i = _count++; i >= 0; i--)
            {
                if (i > index)
                {
                    _array[i] = _array[i - 1];
                }
                else if (i == index)
                {
                    _array[i] = item;
                }
                else
                {
                    _array[i] = _array[i];
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (index > _count)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = index; i < _count - 1; i++)
            {
                _array[i] = _array[i + 1];
            }

            _array[--_count] = default(T);
        }

        public void Clear()
        {
            for (var i = 0; i < _count; i++)
            {
                _array[i] = default(T);
            }

            _count = 0;
        }

        public bool Contains(T item)
        {
            return _array.Contains(item);
        }

        public void AddRange(T[] array)
        {
            CopyTo(array, _count);
        }

        public void AddRange(IList<T> array)
        {
            CheckSpace(array.Count + _count);

            for (var i = _count; i < array.Count + _count; i++)
            {
                _array[i] = array[i - _count];
            }

            _count += array.Count;
        }

        public void CopyTo(T[] array, int index)
        {
            if (index > _count)
            {
                throw new ArgumentOutOfRangeException();
            }

            CheckSpace(array.Length + index);

            for (var i = array.Length + _count; i >= 0; i--)
            {
                if (i >= index + array.Length)
                {
                    _array[i] = _array[i - array.Length];
                }
                else if (i < index + array.Length && i >= index)
                {
                    _array[i] = array[i - index];
                }
            }

            _count += array.Length;
        }

        public bool Remove(T item)
        {
            var founded = false;

            for (var i = 0; i < _count - 1; i++)
            {
                if (_array[i].Equals(item))
                {
                    founded = true;
                }

                if (founded)
                {
                    _array[i] = _array[i + 1];
                }
            }

            if (founded)
            {
                _array[--_count] = default(T);
            }

            return founded;
        }

        public void Sort()
        {
            for (var i = 0; i < _count; i++)
            {
                for (var j = i; j < _count; j++)
                {
                    if (_comparer.Compare(_array[i], _array[j]) == 1)
                    {
                        (_array[i], _array[j]) = (_array[j], _array[i]);
                    }
                }
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _array.GetEnumerator();
        }

        private void CheckSpace(int amount = 0)
        {
            if (_array.Length <= _count + amount)
            {
                var temp = new T[_array.Length * 2];
                for (var i = 0; i < _array.Length; i++)
                {
                    temp[i] = _array[i];
                }

                _array = temp;
            }

            if (_array.Length <= _count + amount)
            {
                CheckSpace(amount);
            }
        }
    }
}
