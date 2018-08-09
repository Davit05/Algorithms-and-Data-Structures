using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class Node<T> where T : IComparable
    {
        public List<T> Key { get; set; }
        public List<Node<T>> Child { get; set; }
        public Node<T> Parent; 
        
        public Node(Node<T> parent)
        {
            Key = new List<T>();
            Child = new List<Node<T>>();
            Parent = parent;
        }
    }
}
