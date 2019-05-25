using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA;
using System.Collections.Generic;

namespace RSATests.UnitTests
{
     [TestClass]
     public class BigNumTest
     {
          [TestMethod]
          public void CreateBigNumTest()
          {
               var bigNum = new BigNum();
               bool isGood = true;

               for (int i = 0; i < GlobalVariables.MAX_NUM; ++i)
               {
                    if(bigNum[i] != 0)
                    {
                         isGood = false;
                    }
               }

               if (isGood)
               {
                    isGood = (bigNum.MSB1Location == null);
               }

               Assert.IsTrue(isGood);
          }

          [TestMethod]
          public void ReadNumTest()
          {
               var bigNum = new BigNum();
               var bigNumToCompareWith = new BigNum();
               bool isGood = true;

               List<int> positionsOf1s = new List<int> { GlobalVariables.MAX_NUM - 1,
                                                         GlobalVariables.MAX_NUM - 3};
               bigNum.ReadNum("0101");
               bigNum.ReadNum(positionsOf1s);
               for (int i = 0; i < GlobalVariables.MAX_NUM; ++i)
               {
                    if (bigNum[i] != bigNumToCompareWith[i])
                    {
                         isGood = false;
                    }
               }

               Assert.IsTrue(isGood);
          }
     }
}
