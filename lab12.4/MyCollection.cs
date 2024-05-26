using CustomLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12._4
{
    public class MyCollection<T> : Tree<T>, IEnumerable<T>, ICollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public static List<MyCollection<T>> collections = new List<MyCollection<T>>();

        bool _isReadOnly;

        public bool IsReadOnly => _isReadOnly;


        public MyCollection(): base() 
        {
            collections.Add(this);
            _isReadOnly = false;
        }
             
        public MyCollection(int length) : base(length) 
        {
            collections.Add(this);
            _isReadOnly = false;
        }

        public MyCollection(MyCollection<T> other) 
        {
            count = 0;
            id = new Id();
            root = new Node<T>((T)other.root.Data.Clone());

            foreach(var item in other)
            {
                AddNode(item);
            }
            collections.Add(this);
        }

        public IEnumerator<T> GetEnumerator() => InOrder(root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> InOrder(Node<T>? node)
        {
            if (node != null)
            {
                foreach (var item in InOrder(node.Left))
                {
                    yield return item;
                }

                yield return node.Data;

                foreach (var item in InOrder(node.Right))
                {
                    yield return item;
                }
            }
        }

        public void Add(T item)
        {
            if (_isReadOnly)
                throw new Exception("Коллекция только для чтения");
            if (!AddNode(item))
            {
                throw new Exception("Элемент уже есть");
            }
        }

        public bool Contains(T item)
        {
            if (Find(item) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int current = 0;
            T[] tempArray = new T[Count];
            ToArray(root, tempArray, ref current);
            int count = 0;
            for(int i = arrayIndex; i < Count; i++)
            {
                array[count] = tempArray[i];
                count++;
            }
        }
    }
}
