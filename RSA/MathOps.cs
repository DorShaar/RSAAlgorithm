using System;
using System.Collections.Generic;

namespace RSA
{
     class MathOps
     {
          private static Random randomGenerator = new Random();

          private static int GenerateOneOrZero()
          {
               return randomGenerator.Next(0, 2);
          }

          public static void PrintNumberInPowersOfTwo(BigNum num)
          {
               List<int> positionsOf1s = num.WriteNum1sPosition();
               for (int i = 0; i < positionsOf1s.Count - 1; ++i)
               {
                    Console.Write($"2^{BigNum.MaxDigitsNumber - positionsOf1s[i] - 1} + ");
               }

               Console.WriteLine($"2^{BigNum.MaxDigitsNumber - positionsOf1s[positionsOf1s.Count - 1] - 1}");
          }
               
          /// <summary>
          /// Gets BigNum number.
          /// Finds the index of the MSB 1 digit, run from it to the LSB.
          /// It has boolean variable <isSmaller> that set to true when the first 1's digits is cleared
          /// (set to 0).
          /// Unless <isSmaller> = true, this method cannot set any bit.
          /// Use random (IsGeneratedToSet() method) to set or clear that bit.
          /// </summary>
          /// <param name="top"></param>
          /// <returns></returns>
          private static BigNum GenerateNumberFromOneToGivenTop(BigNum top)
          {
               BigNum generatedNumber = new BigNum();
               if (top.MSB1Location.HasValue)
               {
                    do
                    {
                         bool isSmaller = false;
                         List<int> PositionsOf1sList = new List<int>();
                         for (int i = top.MSB1Location.Value; i < BigNum.MaxDigitsNumber; ++i)
                         {
                              if (top[i] == 1)
                              {
                                   if (GenerateOneOrZero() == 0)
                                        isSmaller = true;
                                   else
                                        PositionsOf1sList.Add(i);
                              }
                              else
                              {
                                   if ((GenerateOneOrZero() == 1) && (isSmaller))
                                        PositionsOf1sList.Add(i);
                              }
                         }

                         generatedNumber.ReadNum(PositionsOf1sList);
                    } while ((generatedNumber.Compare(BigNum.Zero)) ||
                             (generatedNumber.Compare(top)));
               }

               return generatedNumber;
          }

          /// <summary>
          /// Gets a number m.
          /// Finds p >= 0 and k (k is odd), where m = 2^p * k.
          /// Return p and k by ref.
          /// 
          /// p = 0;
          /// if (!IsEven(m))
          /// {
          ///     k = m;
          /// }
          /// else // m is even.
          /// {
          ///     while(IsEven(m))
          ///     {
          ///          m /= 2;
          ///          p++;
          ///     }
          ///     k = m;
          /// }
          /// </summary>
          /// <param name=""></param>
          /// <param name=""></param>
          public static void FindExpressionOfGivenNumberByPowerOfTwoTimesOddNumber(BigNum m, BigNum p, BigNum k)
          {
               if(!m.IsEven())
               {
                    k.Set(m);
               }
               else
               {
                    while (m.IsEven())
                    {
                         DivNum(m, BigNum.Two, m, new BigNum());
                         p.AddNum(BigNum.One);
                    }

                    k.Set(m);
               }
          }

          /// <summary>
          /// Gets BigNum type number and an index.
          /// Returns the sub number if the number by the index.
          /// Example: number = 0..001101101 (length of 400 bits), index = 398.
          /// The returned number will be 0..000110110.
          /// </summary>
          /// <param name="divided"></param>
          /// <param name="indexPos"></param>
          /// <returns></returns>
          public static BigNum GetSubBigNumFromLeftToIndex(BigNum divided, int indexPos)
          {
               BigNum bigNumFromLeftToIndex = new BigNum();
               List<int> positionsOf1s = new List<int>();
               int initialIndexPos = indexPos;

               while (indexPos >= divided.MSB1Location)
               {
                    if (divided[indexPos] == 1)
                    {
                         positionsOf1s.Add(BigNum.MaxDigitsNumber - (initialIndexPos - indexPos) - 1);
                    }

                    indexPos--;
               }

               bigNumFromLeftToIndex.ReadNum(positionsOf1s);
               return bigNumFromLeftToIndex;
          }

          /// <summary>
          /// J.
          /// Gets two BigNum type numbers, divided and divider.
          /// Returns by reference the result - quotient and reminder.
          /// </summary>
          public static void DivNum(BigNum divided, BigNum divider, BigNum quotientRetrunByRef, BigNum reminderReturnByRef)
          {
               List<int> positionsOf1s = new List<int>();

               if (divided.MSB1Location.HasValue)
               {
                    int indexPos = divided.MSB1Location.Value + divider.Length -1;

                    BigNum subResult = new BigNum(divided);
                    while (indexPos < BigNum.MaxDigitsNumber)
                    {
                         BigNum numberFromLeftToIndex = GetSubBigNumFromLeftToIndex(subResult, indexPos);

                         if (numberFromLeftToIndex >= divider)
                         {
                              // Saving quotient's info.
                              positionsOf1s.Add(indexPos);

                              // Subtract.
                              int numberOfShifts = BigNum.MaxDigitsNumber - indexPos - 1;
                              BigNum numberToSubtract = new BigNum(divider);
                              numberToSubtract.ShiftLeft(numberOfShifts);
                              subResult.SubNum(numberToSubtract);
                         }

                         indexPos++;
                    }

                    quotientRetrunByRef.ReadNum(positionsOf1s);
                    reminderReturnByRef.Set(subResult);
               }
               else
               {
                    Console.WriteLine("Cannot divide by zero");
               }
          }

          /// <summary>
          /// K.
          /// Gets two BigNum type numbers a and m and returns by reference (d, x, y), also BigNum type numbers.
          /// d represents GCD(m,a). (Greatest common divider).
          /// Note: If d = 1, it means that x is the opposite number of m(mod a) and
          ///                               y is the opposite number of a(mod m).
          /// Proofe for Note: 1 = [m*x + a*y](mod m) ==> 1 = a*y.
          /// </summary>
          public static void ExtendedGCD(BigNum a, BigNum m, BigNum x, BigNum y, BigNum d)
          {
               BigNum r0 = new BigNum(m);
               BigNum x0 = new BigNum(BigNum.One);
               BigNum y0 = new BigNum(BigNum.Zero);

               BigNum r1 = new BigNum(a);
               BigNum x1 = new BigNum(BigNum.Zero);
               BigNum y1 = new BigNum(BigNum.One);

               BigNum r = new BigNum(BigNum.One); // Just to be able to enter the while loop.
               BigNum q = new BigNum();
               DivNum(r0, r1, q, r);

               while (!r.Compare(BigNum.Zero))
               {
                    // x = x0 - qx1.
                    BigNum qx1 = new BigNum(q);
                    qx1.MultNum(x1);
                    x.Set(x0);
                    x.SubNum(qx1);

                    // y = y0 - qy1.
                    BigNum qy1 = new BigNum(q);
                    qy1.MultNum(y1);
                    y.Set(y0);
                    y.SubNum(qy1);

                    r0.Set(r1);
                    x0.Set(x1);
                    y0.Set(y1);

                    r1.Set(r);
                    x1.Set(x);
                    y1.Set(y);

                    DivNum(r0, r1, q, r);
               }

               d.Set(r1);
               x.Set(x1);
               y.Set(y1);
          }

          /// <summary>
          /// L.
          /// Gets two BigNums, calculates the opposite number of a in mod m. ([a^-1][mod m]).
          /// Returns null if the a does not have an inverse mode m. (i.e: gcd(a, m) != 1).
          /// </summary>
          public static BigNum Inverse(BigNum a, BigNum m)
          {
               BigNum inverse = null;
               BigNum x = new BigNum();
               BigNum y = new BigNum();
               BigNum d = new BigNum(); // d will be gcd(a, m).

               ExtendedGCD(a, m, x, y, d);
               if (d.Compare(BigNum.One))
                    inverse = y;

               return inverse;
          }

          /// <summary>
          /// M.
          /// Computes x to the power of y at mod z.
          /// </summary>
          /// <returns></returns>
          public static BigNum X2PowerYModZ(BigNum x, BigNum y, BigNum z)
          {
               BigNum result = new BigNum();
               
               if(y.Compare(BigNum.Zero))
               {
                    result = new BigNum(BigNum.One);
               }
               else
               {
                    if (y.IsEven())
                    {
                         // x^y = [x^(y/2)]^2 mod z.
                         BigNum yDividedBy2 = new BigNum();
                         DivNum(y, BigNum.Two, yDividedBy2, result); // y = y/2.
                         result = X2PowerYModZ(x, yDividedBy2, z);
                         result.MultNum(new BigNum(result));
                         DivNum(result, z, new BigNum(), result);
                    }
                    else
                    {
                         BigNum yMinus1DividedBy2 = new BigNum(y);
                         yMinus1DividedBy2.SubNum(BigNum.One); // y = y-1.

                         DivNum(y, BigNum.Two, yMinus1DividedBy2, result); // y = y-1/2.
                         result = X2PowerYModZ(x, yMinus1DividedBy2, z);
                         result.MultNum(new BigNum(result));
                         DivNum(result, z, new BigNum(), result);
                         result.MultNum(x);
                         DivNum(result, z, new BigNum(), result);
                    }
               }

               return result;
          }

          public static bool MillerRabinAlgorithm(BigNum n, BigNum t, BigNum u)
          {
               bool isPrimeNumber = true;
               BigNum randomNumber = GenerateNumberFromOneToGivenTop(n);
               BigNum x0 = X2PowerYModZ(randomNumber, u, n);
               BigNum x1 = new BigNum();
               BigNum iterationNumber = new BigNum(BigNum.One);

               while (iterationNumber <= t) // Iterates t times.
               {
                    x1 = X2PowerYModZ(x0, BigNum.Two, n);
                    x0.Set(x1);
                    if ( (x1.Compare(BigNum.One)) &&
                         (!x0.Compare(BigNum.One)) && 
                         (!x0.Compare(BigNum.MinusOne)) )
                    {
                         isPrimeNumber = false;
                         break;
                    }

                    iterationNumber.AddNum(BigNum.One);
               }

               if (isPrimeNumber)
               {
                    if (!x1.Compare(BigNum.One))
                         isPrimeNumber = false;
               }

               return isPrimeNumber;
          }

          /// <summary>
          /// N.
          /// Runs Miller-Rabin algorithm k times.
          /// </summary>
          /// <param name="n"></param>
          /// <param name="k"></param>
          /// <returns></returns>
          public static bool IsPrime(BigNum n, int k)
          {
               bool isPrimeNumber = true;
               BigNum t = new BigNum();
               BigNum u = new BigNum();
               BigNum nMinusOne = new BigNum(n);

               nMinusOne.SubNum(BigNum.One);
               FindExpressionOfGivenNumberByPowerOfTwoTimesOddNumber(nMinusOne, t, u);

               for(int i = 0; i < k; ++i)
               {
                    if(!MillerRabinAlgorithm(n, new BigNum(t), u))
                    {
                         isPrimeNumber = false;
                         break;
                    }
               }

               return isPrimeNumber;
          }

          /// <summary>
          /// O.
          /// Generates random prime number in the length of MAX_NUN/4.
          /// </summary>
          public static BigNum GenPrime()
          {
               BigNum randomPrimeNumber = new BigNum();
               BigNum top = new BigNum();
               int k = 20;
               int tryNumber = 1;
               List<int> positionsOf1s = new List<int>();
               for (int i = 392; i < BigNum.MaxDigitsNumber; ++i) // TODO change back.
               {
                    positionsOf1s.Add(i);
               }

               top.ReadNum(positionsOf1s);
               Console.WriteLine("Generating random prime number...");
               do
               {
                    Console.WriteLine($"try number: {tryNumber++}");
                    randomPrimeNumber = GenerateNumberFromOneToGivenTop(top);
               } while (!IsPrime(randomPrimeNumber, k));

               return randomPrimeNumber;
          }

          /// <summary>
          /// Gets BigNum <paramref name="b"/>.
          /// Generates p and q, prime numbers.
          /// While b is not inverse number to p-1 and q-1, p and q re-generates.
          /// After finding good p and q, calculate a that is inverse of b mod ((p-1)(q-1)).
          /// n is calculated as p * q.
          /// Returns by reference the BigNum a, p, q, and n.
          /// </summary>
          public static void GenRSA(BigNum b, BigNum a, BigNum q, BigNum p, BigNum nByRef)
          {
               bool isBInverseOfPMinus1AndQMinus1 = false;
               BigNum n = new BigNum();
               
               while (!isBInverseOfPMinus1AndQMinus1)
               {
                    p.Set(GenPrime());
                    q.Set(GenPrime());

                    BigNum pMinus1 = new BigNum(p);
                    pMinus1.SubNum(BigNum.One);
                    BigNum qMinus1 = new BigNum(q);
                    qMinus1.SubNum(BigNum.One);

                    BigNum fiOfN = new BigNum(pMinus1);
                    fiOfN.MultNum(qMinus1);
                    BigNum gcd = new BigNum();
                    ExtendedGCD(b, fiOfN, new BigNum(), new BigNum(), gcd);
                    if(gcd.Compare(BigNum.One))
                    {
                         isBInverseOfPMinus1AndQMinus1 = true;
                         a.Set(Inverse(b, fiOfN));
                         if(a.IsNegative)
                              a.AddNum(fiOfN);

                         n = new BigNum(p);
                         n.MultNum(q);
                    }
               }

               nByRef.Set(n);
          }

          /// <summary>
          /// Encrypts given x with x^b mod n.
          /// </summary>
          public static BigNum Encrypt(BigNum x, BigNum b, BigNum n)
          {
               return X2PowerYModZ(x, b, n);
          }

          /// <summary>
          /// Decrypts given y with y^a mod n.
          /// </summary>
          public static BigNum Decrypt(BigNum y, BigNum a, BigNum n)
          {
               return X2PowerYModZ(y, a, n);
          }
     }
}