using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeterministicSelection
{
    class Program
    {
        public static int DSelect(int[] arr, int element)
        {
            int median = GetMediansFromArr(arr);
            int pivot = Partitioning(arr, median);

            if (element == pivot + 1)
                return arr[pivot];
            else if (element > pivot + 1)
                return DSelect(arr.Skip(pivot + 1).ToArray(), element - pivot - 1);
            else
                return DSelect(arr.Take(pivot).ToArray(), element);
        }

        private static int GetMediansFromArr(int[] arr)
        {
            if (arr.Length <= 5)
            {
                Array.Sort(arr);
                return arr[arr.Length / 2];
            }
            int[] MediansArr = new int[(arr.Length + 4) / 5];
            int[] tempArr = new int[5];

            for (int i = 0, k = 0; i < arr.Length; i += 5, k++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (arr.Length - i < 5)
                    {
                        tempArr = arr.Skip(i).ToArray();
                        break;
                    }
                    else
                        tempArr[j] = arr[j + i];
                }

                Array.Sort(tempArr);
                MediansArr[k] = tempArr[tempArr.Length / 2];
            }
            return GetMediansFromArr(MediansArr);
        }

        private static int Partitioning(int[] arr, int pivot)
        {
            int temp = 0;
            int i;
            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] == pivot)
                {
                    temp = arr[i];
                    arr[i] = arr[0];
                    arr[0] = temp;
                    break;
                }
            }

            i = 0;
            for (int j = 1; j < arr.Length; j++)
            {
                if (arr[0] > arr[j])
                {
                    i++;
                    temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                }
            }

            temp = arr[i];
            arr[i] = arr[0];
            arr[0] = temp;

            return i;
        }

        static void Main(string[] args)
        {
            int[] arr = new int[] { 5, 4, 48, 33, 14, 25, 6, 8, 56, 67, 24, 5, 4, 48, 33, 14, 25, 6 };

            for (int i = 1; i <= arr.Length; i++)
            {
                Console.WriteLine(DSelect(arr, i));
            }
        }
    }
}
