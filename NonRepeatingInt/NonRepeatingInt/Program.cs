using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonRepeatingInt
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 2, 5, 2, 6, 4, 5, 7, 8, 9, 1, 23, 3, 5, 4, 7, 9, 8, 7, 1, 1, 2, 4, 6, 6, 6, 111 };
            NonRep(arr);
        }
        public static void NonRep(int[] arr)
        {
            HashSet<int> non = new HashSet<int>();
            HashSet<int> rep = new HashSet<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (non.Contains(arr[i]))
                    rep.Add(arr[i]);
                else
                    non.Add(arr[i]);
            }
            foreach (var item in non)
            {
                if (!rep.Contains(item))
                    Console.WriteLine(item);
            }
            
        }
    }
}
