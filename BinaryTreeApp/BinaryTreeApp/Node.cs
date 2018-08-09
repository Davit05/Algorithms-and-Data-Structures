using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeApp
{
    public class Node<T> where T : IComparable
    {
        public T Data { get; set; }
        public Node<T> left;
        public Node<T> right;

        public Node(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public Node<T> Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        public Node<T> Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

    }
}
