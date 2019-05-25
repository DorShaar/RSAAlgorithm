using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
     class Program
     {
     }
}

/*
public static void Main(string[] args)
          {
               var set1 = new HashSet<int>();
               var set2 = new HashSet<int>();
               var set3 = new HashSet<int>();
               var set4 = new HashSet<int>();
               int b = 19897;
               int c1 = 780;
               int c2 = 8288;
               int c3 = 8371;
               int c4 = 8329;

               for (int i = 1; i <= b; ++i)
               {
                    if (MathOps.PowerOf(c1, i, b) == 11)
                    {
                         set1.Add(i);
                    }
               }

               for (int i = 1; i <= b; ++i)
               {
                    if (MathOps.PowerOf(c2, i, b) == 14)
                    {
                         set2.Add(i);
                    }
               }

               for (int i = 1; i <= b; ++i)
               {
                    if (MathOps.PowerOf(c3, i, b) == 15)
                    {
                         set3.Add(i);
                    }
               }

               for (int i = 1; i <= b; ++i)
               {
                    if (MathOps.PowerOf(c4, i, b) == 22)
                    {
                         set4.Add(i);
                    }
               }
               set1.IntersectWith(set2);
               set3.IntersectWith(set1);
               set4.IntersectWith(set3);
               foreach(var num in set4)
               {
                    if (MathOps.PowerOf(c4, num, b) == 22)
                    {
                         Console.WriteLine(num);
                    }
               }
               Console.ReadKey();
          }
*/

/*
 * Ex1:
 * public static void Main(string[] args)
          {
               Console.WriteLine("Please type the base of your groups");
               if (!Int32.TryParse(Console.ReadLine(), out int m))
               {
                    Console.WriteLine("Invalid input");
               }
               else
               {
                    Group addGroup = new AddGroup(m);
                    Group multGroup = new MultGroup(m);
                    addGroup.PrintNumbersAndTheirInverse();
                    multGroup.PrintNumbersAndTheirInverse();

                    string encryptedString = "LPWGPUS";
                    int keyA = 15;
                    int keyB = 20;
                    string decipheredString = Decipher.DecipherString
                         (encryptedString, keyA, keyB, (MultGroup)multGroup, (AddGroup)addGroup);
                    Console.WriteLine($"Deciphered string is: {decipheredString}");
               }

               Console.ReadKey();
          }
 */
