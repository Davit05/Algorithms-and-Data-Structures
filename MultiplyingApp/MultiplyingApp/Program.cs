using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MultiplyingApp
{
    class Program
    {

        static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            int lengthX = x.ToString().Length;
            int lengthY = y.ToString().Length;

            BigInteger split = (BigInteger)Math.Max(Math.Pow(10, lengthX / 2), Math.Pow(10, lengthY / 2));
            
            if (split == 1)
                return x * y;
            BigInteger a = x / split;
            BigInteger b = x % split;
            BigInteger c = y / split;
            BigInteger d = y % split;

            BigInteger ac = Multiply(a, c);
            BigInteger bd = Multiply(b, d);
            BigInteger aPlusb_cPlusd = Multiply(a + b, c + d);
            BigInteger adPlusbc = aPlusb_cPlusd - ac - bd;

            return split*split * ac + split * adPlusbc + bd;


        }
        static void Main(string[] args)
        {
            BigInteger b = Multiply(123454450, 567);
            Console.WriteLine(b);
        }
    }
}
