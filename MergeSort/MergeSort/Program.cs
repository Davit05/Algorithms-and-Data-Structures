using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        public static int[] MSort(int[] arr, bool b = false)
        {
            if (arr.Length == 1)
                return arr;

            int[] left = arr.Take(arr.Length / 2).ToArray();
            int[] right = arr.Skip(arr.Length / 2).ToArray();
            return Merge(MSort(left,b), MSort(right,b), b);
        }

        private static int[] Merge(int[] left, int[] right, bool descending)
        {
            int[] result = new int[left.Length + right.Length];
            int ind1 = 0; int ind2 = 0;

            for (int i = 0; i < result.Length; i++)
            {
                if (ind1 >= left.Length)
                {
                    result[i] = right[ind2++];
                    continue;
                }

                if (ind2 >= right.Length)
                {
                    result[i] = left[ind1++];
                    continue;
                }


                if (!descending)
                {
                    if (left[ind1] > right[ind2])
                        result[i] = right[ind2++];
                    else
                        result[i] = left[ind1++];
                }
                else
                {
                    if (left[ind1] > right[ind2])

                        result[i] = left[ind1++];
                    else
                        result[i] = right[ind2++];
                }
            }


            return result;
        }
        static void Main(string[] args)
        {
            int[] arr = new int[] { 4, 5, 7,3,15 };
           MSort(arr);

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

        }
    }
}
