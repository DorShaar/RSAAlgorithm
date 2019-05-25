using System;
using System.Collections.Generic;

namespace Main
{
     class CalcUtilities
     {
          /// <summary>
          /// Calculates GCD - Greatest common Divider.
          /// </summary>
          /// <param name="num1"></param>
          /// <param name="num2"></param>
          /// <returns></returns>
          public static int GCD(int num1, int num2)
          {
               while((num1 != 0) && (num2 != 0))
               {
                    if (num1 > num2)
                    {
                         num1 %= num2;
                    }
                    else
                    {
                         num2 %= num1;
                    }
               }

               return num1 == 0 ? num2 : num1;
          }

          public static int FindInverseNumber(int num, List<int> numbersList, int m)
          {
               int inverseNumber = -1;
               foreach(int numberInList in numbersList)
               {
                    if((num * numberInList) % m == 1)
                    {
                         inverseNumber = numberInList;
                         break;
                    }
               }

               if (inverseNumber == -1)
               {
                    Console.WriteLine("Some un-expected error accured");
               }

               return inverseNumber;
          }

          public static int GetEnglishLetterIndex(char ch)
          {
               int letterIndex = -1;
               
               if(Char.IsLetter(ch))
               {
                    if (!Char.IsLower(ch))
                    {
                         ch = Char.ToLower(ch);
                    }

                    letterIndex = ch - 'a';
               }
               else
               {
                    Console.WriteLine("{0} is not a letter", ch);
               }

               return letterIndex;
          }

          public static char GetUpperEnglishLetterByIndex(int index)
          {
               char upperEnglishLetter = 'A';
               upperEnglishLetter += (char)index;
               return upperEnglishLetter;
          }
     }
}
