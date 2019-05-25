using System;
using System.Collections.Generic;

namespace Main
{
     public abstract class Group : IGroupActions
     {
          protected int m;
          protected List<int> mNumbersList;

          public Group(int m)
          {
               if(m < 1)
               {
                    Console.WriteLine($"Set with group base <{m}> is not valid");
               }
               else
               {
                    this.m = m;
                    mNumbersList = new List<int>(m);
                    UpdateNumbersList();
               }
          }

          public abstract void UpdateNumbersList();

          public virtual void PrintNumbersAndTheirInverse()
          {
               Console.WriteLine($"Z{m}");
               Console.WriteLine("Number:         Negative:");
               foreach (int num in mNumbersList)
               {
                    Console.WriteLine($"   {num}               {m-num}");
               }
          }

          public int Mod
          {
               get
               {
                    return m;
               }
          }
     }
}
