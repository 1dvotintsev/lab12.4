using CustomLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12._4
{
    public class MyCollection<T> : Tree<T>, IEnumerable<T> where T : IInit, ICloneable, IComparable, new()
    {
        public static List<MyCollection<T>> collections = new List<MyCollection<T>>();
        public MyCollection(): base() 
        {
            collections.Add(this);
        }
             
        public MyCollection(int length) : base(length) 
        {
            collections.Add(this);
        }

        public MyCollection(MyCollection<T> other) 
        {
            //count = other.count;
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

        private IEnumerable<T> InOrder(Node<T>? node)
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
    }
}
