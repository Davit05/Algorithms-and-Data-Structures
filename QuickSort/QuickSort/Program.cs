using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        public static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int l, int r)
        {
            if (r <= l)
                return;

            int pivot = new Random().Next(l, r);
            int temp = arr[pivot];
            arr[pivot] = arr[l];
            arr[l] = temp;

            int i = l + 1;

            for (int j = l + 1; j <= r; j++)
            {
                if (arr[l] > arr[j])
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                }
            }

            temp = arr[i - 1];
            arr[i - 1] = arr[l];
            arr[l] = temp;
            int pivotIndexLeft = i - 2;
            int pivotIndexRight = i;

            QuickSort(arr, l, pivotIndexLeft);
            QuickSort(arr, pivotIndexRight, r);

        }
        static void Main(string[] args)
        {
            int[] arr = new int[] { 15, 3, 8, 4, 7, 3, 1, 25, 4, 75, 56, 5, 47, 0, 34 };
            int length = arr.Length;
            QuickSort(arr);
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");
        }
    }
}
