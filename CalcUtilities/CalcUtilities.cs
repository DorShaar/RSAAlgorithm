using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
     class CalcUtilities
     {
          private static void PrintZmGroup(int m)
          {

          }

          public static void PrintGroupInfo(int m)
          {
               if(m < 1)
               {
                    Console.WriteLine("Group with base <{0}> is not valid", m);
               }
               else
               {
                    PrintZmGroup(m);
               }
          }
     }
}
