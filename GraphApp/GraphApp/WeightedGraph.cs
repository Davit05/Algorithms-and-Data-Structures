using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphApp
{
    class WeightedGraph<T> : IEnumerable
    {

        static Random rd = new Random();
        public List<WeightedNode<T>> Vertices { get; set; }
        public int Count { get; set; }
        public WeightedGraph()
        {
            Vertices = new List<WeightedNode<T>>();
            Count = 0;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public bool SearchVertice(T vertice)
        {
            if (IsEmpty())
                return false;

            foreach (var item in Vertices)
            {
                if (item.Value.Equals(vertice))
                    return true;
            }
            return false;
        }

        public void AddNode(T value)
        {
            if (SearchVertice(value))
                return;
            Vertices.Add(new WeightedNode<T>(value));
            Count++;
        }

        public void Connect(T v1, T v2, int weight)
        {
            WeightedNode<T> node1 = null;
            WeightedNode<T> node2 = null;

            foreach (var item in Vertices)
            {
                if (v1.Equals(item.Value))
                    node1 = item;
                if (v2.Equals(item.Value))
                    node2 = item;
            }

            if (node1 == null)
            {
                node1 = new WeightedNode<T>(v1);
                Vertices.Add(node1);
                Count++;
            }
            if (node2 == null)
            {
                node2 = new WeightedNode<T>(v2);
                Vertices.Add(node2);
                Count++;
            }

            WeightedEdge<T> node1Edge = new WeightedEdge<T>(weight, node2);
            WeightedEdge<T> node2Edge = new WeightedEdge<T>(weight, node1);

            node1.Edges.Add(node1Edge);
            node2.Edges.Add(node2Edge);
        }

        public int Dijkstra(T start, T finish)
        {
            WeightedNode<T> startNode = null;
            WeightedNode<T> finishNode = null;
            int result;
            foreach (var item in Vertices)
            {
                if (start.Equals(item.Value))
                    startNode = item;
                if (finish.Equals(item.Value))
                    finishNode = item;
            }
            
            startNode.IndexValue = 0;
            Dijkstra(startNode, finishNode);
            
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (!Vertices[i].IsSeen && !Vertices[i].Value.Equals(finishNode.Value))
                    Dijkstra(Vertices[i], finishNode);
            }

            result = (int)finishNode.IndexValue;

            foreach (var item in Vertices)
            {
                item.IsSeen = false;
                item.IndexValue = null;
            }

            return result;
        }

        private void Dijkstra(WeightedNode<T> startNode, WeightedNode<T> finishNode)
        {
            startNode.IsSeen = true;

            foreach (var item in startNode.Edges)
            {
                if(item.Node.IndexValue == null)
                {
                    item.Node.IndexValue = startNode.IndexValue + item.Weight;
                   // item.Node.IsSeen = false;
                }
                else if (startNode.IndexValue + item.Weight < item.Node.IndexValue)
                {
                    item.Node.IndexValue = startNode.IndexValue + item.Weight;
                    //item.Node.IsSeen = false;
                }
            }

            WeightedNode<T> temp = null;

            for (int i = 0; i < startNode.Edges.Count; i++)
            {
                if (!startNode.Edges[i].Node.IsSeen && !startNode.Edges[i].Node.Value.Equals(finishNode.Value))
                {
                    temp = startNode.Edges[i].Node;
                    for (int j = i + 1; j < startNode.Edges.Count; j++)
                    {
                        if (!startNode.Edges[j].Node.IsSeen && !startNode.Edges[j].Node.Value.Equals(finishNode.Value))
                            temp = startNode.Edges[j].Node;
                    }
                    break;
                }
            }

            if (temp != null)
                Dijkstra(temp, finishNode);
        }

        public int Prim(T source)
        {
            WeightedNode<T> wn = null;

            foreach (var item in Vertices)
            {
                if (item.Value.Equals(source))
                {
                    wn = item;
                    break;
                }
            }

            List<WeightedEdge<T>> listEdge = new List<WeightedEdge<T>>();
            int count = 0;

            return Prim(wn, listEdge, ref count);

        }
        private int Prim(WeightedNode<T> source, List<WeightedEdge<T>> listEdge, ref int count)
        {
            source.IsSeen = true;

            foreach (var item in source.Edges)
            {
                if(!item.Node.IsSeen)
                    listEdge.Add(item);
            }

            if (listEdge.Count == 0)
                return 0;

            WeightedEdge<T> tempEdge = listEdge[0]; 
            WeightedNode<T> tempNode = listEdge[0].Node; 
            int tempCount = 0;

            for (int i = 0; i < listEdge.Count; i++)
            {
                if (!listEdge[i].Node.IsSeen && listEdge[i].Weight <= tempEdge.Weight)
                {
                    tempEdge = listEdge[i];
                    tempNode = listEdge[i].Node;
                    tempCount = listEdge[i].Weight;
                }
                else if (listEdge[i].Node.IsSeen)
                {
                    listEdge.RemoveAt(i);
                }
            }

            listEdge.Remove(tempEdge);
            count += tempCount;
           
            if (tempNode != null)
                Prim(tempNode, listEdge, ref count);

            return count;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in Vertices)
            {
                yield return item.Value;
            }
        }
    }
}


