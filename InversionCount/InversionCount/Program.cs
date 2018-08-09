using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        public static int InverseCount(int[] arr)
        {
            int count = 0;
            MSort(arr, ref count);
            return count;
        }
        private static int[] MSort(int[] arr,ref int count)
        {
            if (arr.Length == 1)
                return arr;

            int[] left = arr.Take(arr.Length / 2).ToArray();
            int[] right = arr.Skip(arr.Length / 2).ToArray();
            return Merge(MSort(left,ref count), MSort(right, ref count), ref count);
        }

        private static int[] Merge(int[] left, int[] right, ref int count)
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

                if (left[ind1] > right[ind2])
                {
                    count += left.Length - ind1;
                    result[i] = right[ind2++];
                }
                else
                {
                    result[i] = left[ind1++];
                }
            }


            return result;
        }
        static void Main(string[] args)
        {
            int[] arr = new int[] { 33,1, 25, 4, 55, 7, 3, 15 };
            int count = InverseCount(arr);

          
                Console.WriteLine(count);
            

        }
    }
}
