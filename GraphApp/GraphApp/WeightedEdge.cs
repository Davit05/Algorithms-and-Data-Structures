using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp
{
    class WeightedEdge<T>
    {
        public int Weight { get; set; }
        public WeightedNode<T> Node { get; set; }

        public WeightedEdge(int weight, WeightedNode<T> node)
        {
            Weight = weight;
            Node = node;
        }

    }
}
