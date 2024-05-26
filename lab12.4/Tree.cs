using CustomLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab12._4
{
    public class Id
    {
        public static int overallCount = 0;
        public int number;

        public Id()
        {
            this.number = overallCount;
            overallCount++;
        }
        public override string ToString()
        {
            return number.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj is IdNumber em)
                return this.number == em.number;
            return false;
        }

        public override int GetHashCode()
        {
            return number.GetHashCode();
        }
    }

    public class Tree<T> where T : IInit, ICloneable, IComparable, new()
    {
        protected Id id;
        public static List<Tree<T>> list = new List<Tree<T>>();
        public Node<T>? root = null;
        protected int count = 0;

        public string Id
        {
            get { return id.ToString(); }
        }

        public int Count { get { return count; } }

        public Tree()
        {
            count = 0;
            root = null;
            id = new Id();
            list.Add(this);
        }

        public Tree(int length) 
        {
            count = length;
            root = MakeTree(length, root);
            id = new Id();
            list.Add(this);
        }

        public Tree<T> Init(int length)
        {
            count = length;
            root = MakeTree(length, root);
            return this;
        }

        //ИСД
        Node<T>? MakeTree(int length, Node<T>? node)
        {
            T data = new T();
            data.RandomInit();
            Node<T> newItem = new Node<T>(data);
            if(length <= 0)
            {
                return null;
            }
            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Left = MakeTree(nl, newItem.Left);
            newItem.Right = MakeTree(nr, newItem.Right);
            return newItem;
        }

        public void Show(Node<T>? node, int spaces = 5)
        {
            if(node != null)
            {
                Show(node.Left, spaces + 5);
                for(int i = 0; i < spaces; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(node.Data);
                Show(node.Right, spaces + 5);
            }
            else 
            {
                //Console.WriteLine("Дерево пустое"); 
            }
            
        }

        public void ToArray(Node<T>? node, T[] array, ref int current) 
        {
            if(node != null)
            {
                ToArray(node.Left, array, ref current);
                array[current] = node.Data;
                current++;
                ToArray(node.Right, array, ref current);
            }
        }

        public Tree<T> ToSearchTree(Tree<T> tree)    //должно быть выделение памяти под новые элементы
        {
            T[] array = new T[tree.Count];
            int current = 0;
            ToArray(tree.root, array, ref current);
            int number = tree.Count;
            count = number;
            root = new Node<T>(array[0]);  //память под корень
            count = 1;
            for(int i = 1; i < number; i++)
            {
                this.AddNode((T)array[i].Clone());
            }
            return this;
        }

        public bool AddNode(T data)
        {
            Node<T> node = root;
            Node<T> parent = null;

            while (node != null)
            {
                int comparisonResult = node.Data.CompareTo(data);
                if (comparisonResult == 0)
                {
                    // Узел уже существует, нельзя добавить дубликат
                    return false;
                }
                else if (comparisonResult < 0)
                {
                    parent = node;
                    node = node.Right;
                }
                else
                {
                    parent = node;
                    node = node.Left;
                }
            }

            // Создаем новый узел
            Node<T> newNode = new Node<T>(data);

            // Если дерево пустое, новый узел становится корнем
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                // Вставляем новый узел на свое место
                if (parent.Data.CompareTo(data) < 0)
                {
                    parent.Right = newNode;
                }
                else
                {
                    parent.Left = newNode;
                }
            }

            count++;
            return true;
        }


        public void Clear()
        {
            this.root = null;
            count = 0;
        }

        public Node<T>? Find(T data)
        {
            return FindNode(root, data);
        }

        private Node<T>? FindNode(Node<T>? current, T data)
        {
            if (current == null)
                return null;

            if (current.Data.CompareTo(data) == 0)
                return current;

            Node<T>? foundNode = FindNode(current.Left, data);
            if (foundNode == null)
                foundNode = FindNode(current.Right, data);

            return foundNode;
        }

        // Функция удаления элемента из дерева
        public bool Remove(T data)
        {
            Node<T>? parent = null;
            Node<T>? current = root;

            // Поиск узла для удаления и его родителя
            while (current != null)
            {
                if (current.Data.CompareTo(data) == 0)
                    break;

                parent = current;
                if (data.CompareTo(current.Data) < 0)
                    current = current.Right;
                else                                    //поменял местами L и R
                    current = current.Left;
            }

            // Узел для удаления не найден
            if (current == null)
                return false;

            // Если у узла есть два потомка
            if (current.Left != null && current.Right != null)
            {
                // Находим наименьший узел в правом поддереве
                Node<T> minRight = current.Right;
                Node<T>? parentMinRight = current;

                while (minRight.Left != null)
                {
                    parentMinRight = minRight;
                    minRight = minRight.Left;
                }

                // Заменяем удаляемый узел на наименьший узел в правом поддереве
                current.Data = minRight.Data;

                // Переопределяем текущий узел и его родителя для последующего удаления
                current = minRight;
                parent = parentMinRight;
            }

            // Если у узла нет детей или только один ребенок
            Node<T>? child;
            if (current.Left != null)
                child = current.Left;
            else if (current.Right != null)
                child = current.Right;
            else
                child = null;

            // Удаляем узел из дерева
            if (parent == null)
                root = child;
            else if (parent.Left == current)
                parent.Left = child;
            else
                parent.Right = child;

            count--;
            return true;
        }

        public int Depth(Node<T> node)
        {
            Node<T> current = node;

            // Если узел пустой, возвращаем 0
            if (current == null)
                return 0;

            // Рекурсивно находим глубину для левого и правого поддеревьев
            int leftDepth = Depth(current.Left);
            int rightDepth = Depth(current.Right);

            // Возвращаем максимум из глубины левого и правого поддеревьев + 1 (текущий уровень)
            return Math.Max(leftDepth, rightDepth) + 1;
        }

    }
}
