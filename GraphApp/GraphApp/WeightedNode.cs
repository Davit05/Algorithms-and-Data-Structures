using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp
{
    class WeightedNode<T>
    {
        public T Value { get; set; }
        public int? IndexValue { get; set; }
        public List<WeightedEdge<T>> Edges { get; set; }
        public bool IsSeen { get; set; }
        public WeightedNode(T value)
        {
            Value = value;
            Edges = new List<WeightedEdge<T>>();
            IsSeen = false;
            IndexValue = null;
        }

    }
}
