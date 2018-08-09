using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapApp
{
    class MinHeap<T>:IEnumerable where T : IComparable
    {
        private List<T> List { get; set; }
        public MinHeap()
        {
            List = new List<T>();
        }

        public void Add(T value)
        {
            List.Add(value);
            int index = List.Count - 1;
            int parent = (index - 1) / 2;
            while (List[index].CompareTo(List[parent]) < 0)
            {
                T temp = List[index];
                List[index] = List[parent];
                List[parent] = temp;
                index = parent;
                parent = (index-1)/ 2;
            }
        }

        public T Peek() => List[0];

        public T Pop()
        {
            T result = List[0];

            List[0] = List[List.Count - 1];
            List.RemoveAt(List.Count - 1);

            int index = 0;
            T temp;
            while (true)
            {
                int child1 = 2 * index + 1;
                int child2 = 2 * index + 2;

                if (child1 > List.Count - 1)
                    return result;

                if(child2 > List.Count - 1)
                {
                    if (List[index].CompareTo(List[child1]) > 0)
                    {
                        temp = List[index];
                        List[index] = List[child1];
                        List[child1] = temp;
                    }
                    return result;
                }

                int child = 0;
                if (List[child1].CompareTo(List[child2]) <= 0)
                    child = child1;
                else
                    child = child2;

                if (List[index].CompareTo(List[child]) > 0)
                {
                    temp = List[index];
                    List[index] = List[child];
                    List[child] = temp;
                }
                else
                    return result;

                index = child;
            }
        }


        public IEnumerator GetEnumerator()
        {
            return List.GetEnumerator();
        }
    }
}
