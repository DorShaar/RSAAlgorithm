using System;
using System.Collections.Generic;

namespace Main
{
     public class MultGroup : Group
     {
          private List<int> mInverseNumbers;
          
          public MultGroup(int m) : base(m)
          {
               mInverseNumbers = new List<int>(m);
               UpdateInverseNumbersList();
          }

          public override void UpdateNumbersList()
          {
               for (int i = 0; i < m; ++i)
               {
                    if (CalcUtilities.GCD(i,m) == 1)
                    {
                         mNumbersList.Add(i);
                    }
               }
          }

          public void UpdateInverseNumbersList()
          {
               foreach(int num in mNumbersList)
               {
                    mInverseNumbers.Add(CalcUtilities.FindInverseNumber(num, mNumbersList, m));
               }
          }

          public override void PrintNumbersAndTheirInverse()
          {
               Console.WriteLine("Z*{0}:", m);
               Console.WriteLine("Number:         Inverse:");
               for(int i = 0; i < mNumbersList.Count; ++i)
               {
                    Console.WriteLine("   {0}               {1}", mNumbersList[i], mInverseNumbers[i]);
               }
          }

          public int GetInverseNumber(int number)
          {
               return CalcUtilities.FindInverseNumber(number, mNumbersList, m);
          }
     }
}
