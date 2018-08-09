using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree<int> tree = new BTree<int>(3);
            Random rd = new Random();
            for (int i = 0; i < 30; i++)
            {
                tree.Add(rd.Next(100, 1000));
                Console.ReadLine();
                tree.Print();

            }
            //Console.WriteLine("REMOVE");
            tree.Print();
            for (int i = 0; i < 30; i++)
            {
                Console.Write("Enter key for remove - ");
                tree.Remove(int.Parse(Console.ReadLine()));
                Console.WriteLine();
                tree.Print();
            }

        }
    }
}
