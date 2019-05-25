using System;

namespace Main
{
     class MathOps
     {
          /// <summary>
          /// Raises <paramref name="num"/> to the power of <paramref name="power"/> mod <paramref name="n"/>.
          /// (num^power)mod(n);
          /// </summary>
          /// <param name="num"></param>
          /// <param name="power"></param>
          /// <returns></returns>
          public static long PowerOf(int num, int power, int n)
          {
               long result;

               if (power == 0)
               {
                    result = 1;
               }
               else
               {
                    if (power%2 == 0) // <power> is even.
                    {
                         result = (long)(Math.Pow(PowerOf(num, power / 2, n), 2));
                         result %= n;
                    }
                    else // <power> is odd.
                    {
                         long subResult = (long)(Math.Pow(PowerOf(num, (power - 1) / 2, n), 2));
                         subResult %= n;
                         result = num * subResult;
                         result %= n;
                    }
               }

               return result;
          }
     }
}
