using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BinaryTreeApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Random rd = new Random();
            BinaryTree<int> tree = new BinaryTree<int>();

            for (int i = 0; i < 20; i++)
            {
                tree.AddRec(rd.Next(10, 100));
            }

            tree.PrintIncOrder();
        }
    }
}
