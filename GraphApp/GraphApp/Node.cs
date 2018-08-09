using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp
{
    public class Node<T> : IEnumerable,ICloneable
    {
        public T Value { get; set; }
        public List<Node<T>> Roots { get; set; }
        public bool IsSeen { get; set; }
        public Node(T value)
        {
            Value = value;
            Roots = new List<Node<T>>();
            IsSeen = false;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in Roots)
            {
                yield return item.Value;
            }
        }

        public object Clone()
        {
            Type t = typeof(T);
            T cloneValue = default(T);

            ICloneable isCloneable = (ICloneable)t.GetInterface("ICloneable");
            dynamic d = Value;

            if (isCloneable != null)
                cloneValue = d.Clone();
            else
                cloneValue = Value;

            Node<T> clone = new Node<T>(cloneValue);

            return clone;
        }
    }
}
