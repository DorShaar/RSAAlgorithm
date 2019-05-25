using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
     class AddGroup : Group
     {
          public AddGroup(int m) : base(m) { }

          public override void UpdateNumbersList()
          {
               for (int i = 0; i < m; ++i)
               {
                    mNumbersList.Add(i);
               }
          }

          public int GetNegativeNumber(int num)
          {
               num = num <= 0 ? -num : num;
               return m - num;
          }
     }
}
