using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index_ItemIsSame
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static bool IsSame(int[] arr)
        {
            return IsSame(arr, 0, arr.Length - 1);
        }

        private static bool IsSame(int[] arr, int first, int last)
        {
            int index = (first + last) / 2;

            if (arr[index] == index)
                return true;

            if (first == last)
                return false;

            if (arr[index] > index)
                return IsSame(arr, first, index - 1);
            else
                return IsSame(arr, index + 1, last);
        }

    }
}
