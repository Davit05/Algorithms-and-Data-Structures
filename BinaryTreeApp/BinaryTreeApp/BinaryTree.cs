using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeApp
{
    class BinaryTree<T> : IEnumerable where T : IComparable
    {
        public Node<T> head;
        public int Count { get; set; }
        private List<T> data;

        public BinaryTree()
        {
            head = null;
            Count = 0;
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public void Add(T data)
        {
            if (IsEmpty)
            {
                head = new Node<T>(data);
            }
            else
            {
                Node<T> current = head;

                while (true)
                {
                    if (current.Data.CompareTo(data) <= 0)
                    {
                        if (current.Right == null)
                        {
                            current.Right = new Node<T>(data);
                            break;
                        }
                        current = current.Right;
                    }
                    else
                    {
                        if (current.Left == null)
                        {
                            current.Left = new Node<T>(data);
                            break;
                        }
                        current = current.Left;
                    }
                }
            }
            Count++;
        }

        public void AddRec(T data)
        {
            AddRec(ref head, data);

        }
        private void AddRec(ref Node<T> current, T data)
        {
            if (current == null)
            {
                current = new Node<T>(data);
            }
            else if (current.Data.CompareTo(data) == 0 || current.Data.CompareTo(data) == -1)
            {
                if (current.Right == null)
                {
                    current.Right = new Node<T>(data);
                    return;
                }
                AddRec(ref current.right, data);
            }
            else
            {
                if (current.Left == null)
                {
                    current.Left = new Node<T>(data);
                    return;
                }
                AddRec(ref current.left, data);
            }
            Count++;
        }

        public void Remove(T data)
        {
            if (IsEmpty)
            {
                Console.WriteLine("Empty");
                return;
            }
            if (!Contains(data))
            {
                Console.WriteLine("Data doesn't exist");
            }
            Remove(ref head, data);
            Count--;
        }
        private void Remove(ref Node<T> n, T data)
        {
            if (n.Data.CompareTo(data) == 0)
            {
                Remove(ref n);
            }
            else if (n.Data.CompareTo(data) == -1)
            {
                Remove(ref n.right, data);
            }
            else
            {
                Remove(ref n.left, data);
            }
        }
        private void Remove(ref Node<T> current)
        {
            if (current.Right == null && current.Left == null)
            {
                current = null;
            }
            else if (current.Right != null & current.Left == null)
            {
                current = current.Right;
            }
            else if (current.Left != null & current.Right == null)
            {
                current = current.Left;
            }
            else
            {
                current.Data = RemoveLeftMax(current);
            }
        }
        private T RemoveLeftMax(Node<T> current)
        {
            T n;
            if (current.Left.Right == null)
            {
                n = current.Left.Data;
                current.Left = current.Left.Left;
                return n;
            }
            current = current.Left;
            while (true)
            {
                if (current.Right.Right == null)
                {
                    n = current.Right.Data;
                    current.Right = current.Right.Left;
                    break;
                }
                current = current.Right;
            }
            return n;
        }

        public bool Contains(T data)
        {
            return Contains(head, data);
        }
        private bool Contains(Node<T> n, T data)
        {
            if (n.Data.CompareTo(data) == 0)
                return true;
            if (n.Data.CompareTo(data) == -1)
            {
                if (n.Right == null)
                    return false;
                return Contains(n.Right, data);
            }
            else
            {
                if (n.Left == null)
                    return false;
                return Contains(n.Left, data);

            }
        }

        public void Print()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Empty");
                return;
            }
            Console.WriteLine(head.Data);
            Print(head);
            Console.WriteLine();
        }
        private void Print(Node<T> tree)
        {
            if (tree.Right != null)
            {
                Console.WriteLine(tree.Right.Data);
                Print(tree.Right);
            }
            if (tree.Left != null)
            {
                Console.WriteLine(tree.Left.Data);
                Print(tree.Left);
            }
        }

        public void PrintIncOrder()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Empty");
                return;
            }
            PrintIncOrder(head);
            Console.WriteLine();
        }
        private void PrintIncOrder(Node<T> cur)
        {
            if (cur.Left != null)
                PrintIncOrder(cur.Left);
            Console.Write($"{cur.Data} ");
            if (cur.Right != null)
                PrintIncOrder(cur.Right);
        }

        private void CreateDataList(Node<T> cur)
        {
            if (cur == null)
                return;

            data.Add(cur.Data);

            CreateDataList(cur.Left);
            CreateDataList(cur.Right);
        }
        public IEnumerator GetEnumerator()
        {
            data = new List<T>();
            CreateDataList(head);

            return data.GetEnumerator();
        }

    }
}
