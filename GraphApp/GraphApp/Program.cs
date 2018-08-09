using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> g = new Graph<string>();

            g.AddNode("h");
            g.AddNode("f");
            g.AddNode("e");
            g.AddNode("t");
            g.AddNode("q");

            g.Connect("h", "f");
            g.Connect("h", "t");
            g.Connect("f", "e");
            g.Connect("h", "e");
            g.Connect("h", "q");
            Console.WriteLine();
            Graph<string> gc = (Graph<string>)g.Clone();




            //Console.ReadLine();
            //g.BFS(g.Vertices[0]);
            g.DFS(g.Vertices[0]);
            //Console.WriteLine();
            Console.WriteLine();
            ////gc.BFS(gc.Vertices[0]);

            gc.DFSStack(gc.Vertices[0]);


        }

        private static void WeightedGraph()
        {
            WeightedGraph<char> wg = new WeightedGraph<char>();
            wg.Connect('S', 'B', 1);
            wg.Connect('S', 'E', 2);
            wg.Connect('S', 'A', 3);
            wg.Connect('B', 'C', 1);
            wg.Connect('C', 'E', 8);
            wg.Connect('C', 'D', 4);
            wg.Connect('D', 'E', 3);
            wg.Connect('E', 'F', 8);
            wg.Connect('A', 'F', 5);
            wg.Connect('F', 'G', 5);
            wg.Connect('F', 'V', 8);
            wg.Connect('S', 'K', 8);
            wg.Connect('S', 'J', 8);
            wg.Connect('S', 'T', 8);
            wg.Connect('F', 'T', 1);
            wg.Connect('T', 'B', 3);



            for (int i = 0; i < wg.Vertices.Count; i++)
            {
                Console.WriteLine("S ->  " + wg.Vertices[i].Value + " " + wg.Dijkstra('S', wg.Vertices[i].Value));
                Console.WriteLine();
            }
        }

        private static void Graph()
        {
            Graph<int> g = new Graph<int>();

            g.AddNode(5);
            g.AddNode(34);
            g.AddNode(2);
            g.AddNode(0);
            g.AddNode(8);
            g.AddNode(7);
            g.AddNode(52);

            g.Connect(2, 0);
            g.Connect(2, 34);
            g.Connect(0, 34);
            g.Connect(0, 5);
            g.Connect(0, 8);
            g.Connect(34, 5);
            g.Connect(8, 7);
            g.Connect(8, 52);
            g.Connect(7, 52);
        }
    }
}
