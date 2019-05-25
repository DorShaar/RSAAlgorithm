using System;
using System.Collections.Generic;
using System.Text;

namespace RSA
{
     /// <summary>
     /// BigNum is a binary number in length of <GlobalVariables.MAX_NUM> digits.
     /// Every instance of BigNum is initialize to 0.
     /// </summary>
     public class BigNum
     {
          private int[] mBinaryNumber;
          private int? m1MSBLocation; // Contains the index of the most significant 1 in the binary number.
                                      // When <mBinaryNumber> = 0, then <m1MSBLocation> = null.

          public BigNum()
          {
               mBinaryNumber = new int[GlobalVariables.MAX_NUM];
               InitializeToZero();
               m1MSBLocation = null;
          }

          public BigNum(BigNum bigNum)
          {
               mBinaryNumber = new int[GlobalVariables.MAX_NUM];

               for (int i = 0; i < GlobalVariables.MAX_NUM; ++i)
               {
                    mBinaryNumber[i] = bigNum.mBinaryNumber[i];
               }

               m1MSBLocation = bigNum.m1MSBLocation;
          }

          public BigNum(long num)
          {
               mBinaryNumber = new int[GlobalVariables.MAX_NUM];
               LongToBig(num);
          }

          private void InitializeToZero()
          {
               for (int i = 0; i < GlobalVariables.MAX_NUM; ++i)
               {
                    mBinaryNumber[i] = 0;
               }
          }

          private bool IsBinaryDigit(char digit)
          {
               return ((digit == 0) || (digit == 1));
          }

          private bool IsBinaryNumber(string binaryNumber)
          {
               bool isBinaryNumber = true;

               foreach (char digit in binaryNumber)
               {
                    if (!IsBinaryDigit(digit))
                    {
                         isBinaryNumber = false;
                         break;
                    }
               }

               return isBinaryNumber;
          }

          private bool IsBigNumFitsIntoLongType(BigNum num)
          {
               bool isBigNumFitsIntoLongType;
               if (m1MSBLocation.HasValue)
               {
                    isBigNumFitsIntoLongType =
                         (GlobalVariables.MAX_NUM - num.m1MSBLocation.Value <= GlobalVariables.MaxNumberOfDigitsInLongType);
               }
               else // It means that num is zero.
               {
                    isBigNumFitsIntoLongType = true;
               }

               return isBigNumFitsIntoLongType;
          }

          private void UpdateMSB1Location()
          {
               int i = 0;

               m1MSBLocation = null;
               while (i < GlobalVariables.MAX_NUM)
               {
                    if (mBinaryNumber[i] == 1)
                    {
                         m1MSBLocation = i;
                         break;
                    }

                    ++i;
               }
          }

          public bool IsEven()
          {
               return (mBinaryNumber[GlobalVariables.MAX_NUM - 1] == 0);
          }

          /// <summary>
          /// A.
          /// Reads from console a binary number. If the user input is valid puts it in <mBinaryNumber>.
          /// Updates <m1MSBLocation>.
          /// </summary>
          public void ReadNum()
          {
               string binaryNumber = Console.ReadLine();
               if (!IsBinaryNumber(binaryNumber))
               {
                    Console.WriteLine("The number is not binary");
               }
               else
               {
                    ReadNum(binaryNumber);
               }
          }

          /// <summary>
          /// Puts the binaryNumber in <mBinaryNumber>.
          /// Assumption: <binaryNumber> is valid binary number.
          /// </summary>
          /// <param name="binaryNumber"></param>
          public void ReadNum(string binaryNumber)
          {
               InitializeToZero();
               int numberOfDigits = binaryNumber.Length;
               for (int i = 0; i < numberOfDigits; ++i)
               {
                    mBinaryNumber[GlobalVariables.MAX_NUM - numberOfDigits + i] = (int)Char.GetNumericValue(binaryNumber[i]);
               }

               // Note: if binaryNumber is negative, it has only 64 digits, so we need to fill the rest of the 1's
               // by ourselves.
               if ((binaryNumber[0] == '1') && (binaryNumber.Length - 1 == GlobalVariables.MaxNumberOfDigitsInLongType))
               {
                    for (int i = 0; i < GlobalVariables.MAX_NUM - numberOfDigits; ++i)
                    {
                         mBinaryNumber[i] = 1;
                    }
               }

               UpdateMSB1Location();
          }

          /// <summary>
          /// A.
          /// Gets the positions of the digits that their value is 1.
          /// In case a position is not valid, i.e. bigger then <MAX_NAM>, it ignores it.
          /// </summary>
          public void ReadNum(List<int> positionsOf1s)
          {
               InitializeToZero();
               int minPosition = GlobalVariables.MAX_NUM;
               foreach (int position in positionsOf1s)
               {
                    if (position < GlobalVariables.MAX_NUM)
                    {
                         mBinaryNumber[position] = 1;
                         minPosition = (minPosition > position) ? position : minPosition;
                    }
               }

               // Updates <m1MSBLocation>.
               if (minPosition == GlobalVariables.MAX_NUM)
               {
                    m1MSBLocation = null;
               }
               else
               {
                    m1MSBLocation = minPosition;
               }
          }

          /// <summary>
          /// Create list of positions of 1's in <mBinaryNumber>.
          /// The input ends when -1 or invalid value is typed.
          /// </summary>
          /// <returns></returns>
          public List<int> GetPositionsOf1s()
          {
               List<int> positionsOf1s = new List<int>();

               Console.WriteLine("Type positions of 1's. The input will be stopped if invalid value or -1 will be typed");
               char digit = (char)(Console.Read());
               while (IsBinaryDigit(digit))
               {
                    positionsOf1s.Add(digit);
               }

               return positionsOf1s;
          }

          /// <summary>
          /// B.
          /// Returns the binary number represented in <mBinaryNumber> without leading zeroes.
          /// </summary>
          /// <returns></returns>
          public string WriteNum()
          {
               StringBuilder binaryNumber = new StringBuilder();
               if (!m1MSBLocation.HasValue)
               {
                    binaryNumber.Append("0");
               }
               else
               {
                    for (int pos = m1MSBLocation.Value; pos < GlobalVariables.MAX_NUM; ++pos)
                    {
                         binaryNumber.Append(mBinaryNumber[pos]);
                    }
               }

               return binaryNumber.ToString();
          }

          /// <summary>
          /// B.
          /// Returns list of position of 1's in <mBinaryNumber>.
          /// </summary>
          /// <returns></returns>
          public List<int> WriteNum1sPosition()
          {
               List<int> positionOf1s = new List<int>();
               for (int pos = 0; pos < GlobalVariables.MAX_NUM; ++pos)
               {
                    if (mBinaryNumber[pos] == 1)
                    {
                         positionOf1s.Add(pos);
                    }
               }

               return positionOf1s;
          }

          /// <summary>
          /// C.
          /// Converts long number into BigNum type, into this instance.
          /// </summary>
          /// <param name="num"></param>
          public void LongToBig(long num)
          {
               int toBase = 2;
               string binaryNumber = Convert.ToString(num, toBase);

               if (binaryNumber != null)
               {
                    ReadNum(binaryNumber);
               }
               else
               {
                    Console.WriteLine("Invalid long number given");
               }
          }

          /// <summary>
          /// D.
          /// Converts the big number from this instance to long number.
          /// Checks that the number can fit into long. If so, returns it.
          /// The biggest positive value that can be hold by long variable is: 2^63 -1,
          /// therfore has 63 bits.
          /// </summary>
          /// <returns>longNumber</returns>
          public long? BigToLong()
          {
               BigNum copyNumber = new BigNum(this);
               long? longNumber;
               int fromBase = 2;
               bool isNegative = false;

               if (IsNegative)
               {
                    // Make copyNumber represented as positive number.
                    copyNumber.Complement();
                    copyNumber.AddNum(GlobalVariables.one);
                    isNegative = true;
               }

               if (IsBigNumFitsIntoLongType(copyNumber))
               {
                    longNumber = Convert.ToInt64(copyNumber.WriteNum(), fromBase);
                    if (isNegative)
                    {
                         longNumber = -longNumber;
                    }
               }
               else
               {
                    longNumber = null;
                    Console.WriteLine($"Convertion from BigNum to long is not possible," +
                         $"this BigNum has more then {GlobalVariables.MaxNumberOfDigitsInLongType} digits");
               }

               return longNumber;
          }

          /// <summary>
          /// E.
          /// Gets BigNum type and adds it into this instance.
          /// Uses <carry> of type BigNum to implement the binary adding.
          /// Let do summing B = A + B when a[i] is the i'th digit of A. C is the carry.
          /// If a[i] + c[i] == 0, b[i] does not changes.
          /// If a[i] + c[i] == 1, b[i] changes it's value and c[i-1] gets 1.
          /// If a[i] + c[i] == 2, b[i] does not changes and c[i-1] gets 1.
          /// </summary>
          /// <param name="numberToAdd"></param>
          public void AddNum(BigNum numberToAdd)
          {
               BigNum carry = new BigNum(); // <carry> = 00..0.

               for(int i = GlobalVariables.MAX_NUM - 1; i > 0; --i)
               {
                    if (numberToAdd[i] + carry[i] == 1)
                    {
                         if (mBinaryNumber[i] == 0)
                         {
                              mBinaryNumber[i] = 1;
                         }
                         else
                         {
                              mBinaryNumber[i] = 0;
                              carry[i - 1] = 1;
                         }
                    }
                    else if (numberToAdd[i] + carry[i] == 2)
                    {
                         carry[i - 1] = 1;
                    }
               }

               // Handles i == 0.
               if (numberToAdd[0] + carry[0] == 1)
               {
                    if (mBinaryNumber[0] == 0)
                    {
                         mBinaryNumber[0] = 1;
                    }
                    else
                    {
                         mBinaryNumber[0] = 0;
                         // Console.WriteLine("There is an overflow. Carry 1 is dropped.");
                    }
               }
               else if (numberToAdd[0] + carry[0] == 2)
               {
                    // Console.WriteLine("There is an overflow. Carry 1 is dropped.");
               }

               UpdateMSB1Location();
          }

          /// <summary>
          /// F.
          /// Shift all the bits in <mBinaryNumber> to the left.
          /// The arithmatic purpose of this operation is to mult by 2.
          /// The LSB is gets 0.
          /// </summary>
          public void ShiftLeft()
          {
               bool isOverflow = false;
               if (mBinaryNumber[0] == 1)
               {
                    //Console.WriteLine("There is an overflow. Carry 1 is dropped.");
                    isOverflow = true;
               }

               for(int i = 0; i < GlobalVariables.MAX_NUM - 1; ++i)
               {
                    mBinaryNumber[i] = mBinaryNumber[i + 1];
               }

               mBinaryNumber[GlobalVariables.MAX_NUM - 1] = 0;
               // Updates MSB1Location;
               if (isOverflow)
               {
                    UpdateMSB1Location();
               }
               else
               {
                    m1MSBLocation--;
               }
          }

          public void ShiftLeft(int times)
          {
               for(int i = 0; i < times; ++i)
               {
                    ShiftLeft();
               }
          }

          /// <summary>
          /// G.
          /// Every number can be represented as 2N or 2N+1, when N is even and natural number.
          /// Converts <numberToMult> into long and into representation of 2N or 2N+1.
          /// Then doing N times ShiftLeft() method, If the number is 2N+1, adding one time the initial value
          /// of this instance.
          /// Or
          /// Let the current value of this intance be <value>.
          /// If <numberToMult[i]> == 0 then mult by 2 (ShiftLeft() method).
          /// If <numberToMult[i]> == 1 then mult by 2 (ShiftLeft() method) and adds <value>.
          /// </summary>
          /// <param name="numberToMult"></param>
          public void MultNum(BigNum numberToMult)
          {
               BigNum valueToAdd = new BigNum(this);
               InitializeToZero();
               for (int i = GlobalVariables.MAX_NUM - 1; i >= numberToMult.m1MSBLocation; --i)
               {
                    if (numberToMult[i] == 1)
                    {
                         AddNum(valueToAdd);
                    }

                    valueToAdd.ShiftLeft();
               }

               UpdateMSB1Location();
          }

          /// <summary>
          /// H.
          /// Gets another BigNum type number and compare between the two.
          /// </summary>
          /// <returns></returns>
          public bool CompNum(BigNum numberToCompare)
          {
               bool isEqual = true;
               bool isFinishCompare = false;

               if ((!m1MSBLocation.HasValue) && (!numberToCompare.m1MSBLocation.HasValue))
               {
                    // That means that they both equal to zero.
                    isFinishCompare = true;
               }
               else if (((m1MSBLocation.HasValue) && (!numberToCompare.m1MSBLocation.HasValue)) ||
                        ((!m1MSBLocation.HasValue) && (numberToCompare.m1MSBLocation.HasValue)))
               {
                    // That means that one is zero and other is not.
                    isEqual = false;
                    isFinishCompare = true;
               }

               if (!isFinishCompare)
               {
                    for(int i = m1MSBLocation.Value; i < GlobalVariables.MAX_NUM; ++i)
                    {
                         if (mBinaryNumber[i] != numberToCompare.mBinaryNumber[i])
                         {
                              isEqual = false;
                              break;
                         }
                    }
               }

               if (isEqual)
               {
                    Console.WriteLine("The numbers are equal.");
               }
               else
               {
                    Console.WriteLine("The numbers are NOT equal.");
               }

               return isEqual;
          }

          public bool CompNumWithoutPrint(BigNum numberToCompare)
          {
               bool isEqual = true;
               bool isFinishCompare = false;

               if ((!m1MSBLocation.HasValue) && (!numberToCompare.m1MSBLocation.HasValue))
               {
                    // That means that they both equal to zero.
                    isFinishCompare = true;
               }
               else if (((m1MSBLocation.HasValue) && (!numberToCompare.m1MSBLocation.HasValue)) ||
                        ((!m1MSBLocation.HasValue) && (numberToCompare.m1MSBLocation.HasValue)))
               {
                    // That means that one is zero and other is not.
                    isEqual = false;
                    isFinishCompare = true;
               }

               if (!isFinishCompare)
               {
                    for (int i = m1MSBLocation.Value; i < GlobalVariables.MAX_NUM; ++i)
                    {
                         if (mBinaryNumber[i] != numberToCompare.mBinaryNumber[i])
                         {
                              isEqual = false;
                              break;
                         }
                    }
               }

               return isEqual;
          }

          /// <summary>
          /// I.
          /// Gets BigNum type and substract between the current value of the binary number with the given one.
          /// Let do substraction A = A-B. We know that A-B = A + (-B).
          /// Calculating -B: Doing complement and then add 1.
          /// </summary>
          /// <param name="numberToSubtract"></param>
          public void SubNum(BigNum numberToSubtract)
          {
               // numberToSubstract = B.
               BigNum modifiedNumberToSubtract = new BigNum(numberToSubtract);
               modifiedNumberToSubtract.Complement(); // numberToSubtract = -B - 1.
               modifiedNumberToSubtract.AddNum(GlobalVariables.one); // numberToSubtract = -B.
               AddNum(modifiedNumberToSubtract); // Allready updates <mMSB1Location>.
               UpdateMSB1Location();
          }

          public void Complement()
          {
               for(int i = 0; i < GlobalVariables.MAX_NUM; ++i)
               {
                    if (mBinaryNumber[i] == 0)
                    {
                         mBinaryNumber[i] = 1;
                    }
                    else // (mBinaryNumber[i] == 1)
                    {
                         mBinaryNumber[i] = 0;
                    }
               }

               UpdateMSB1Location();
          }

          public void Set(BigNum bigNum)
          {
               m1MSBLocation = bigNum.m1MSBLocation;
               //mBinaryNumber = bigNum.mBinaryNumber;
               for (int i = 0; i < GlobalVariables.MAX_NUM; ++i)
               {
                    mBinaryNumber[i] = bigNum.mBinaryNumber[i];
               }
          }

          /// <summary>
          /// Overloading the ">=" operator.
          /// If num1 >= num2, num1-num2 should be positive => subResult.IsNegative = false.
          /// It should return true, therefore !subResult.IsNegative.
          /// </summary>
          /// <param name="num1"></param>
          /// <param name="num2"></param>
          /// <returns></returns>
          public static bool operator >=(BigNum num1, BigNum num2)
          {
               BigNum subResult = new BigNum(num1);
               subResult.SubNum(num2);
               return !subResult.IsNegative;
          }

          /// <summary>
          /// Overloading the "<=" operator.
          /// If num1 >= num2, num1-num2 should be positive => subResult.IsNegative = false.
          /// It should return true, therefore !subResult.IsNegative.
          /// </summary>
          /// <param name="num1"></param>
          /// <param name="num2"></param>
          /// <returns></returns>
          public static bool operator <=(BigNum num1, BigNum num2)
          {
               bool isSmallEqual;
               if(num1.CompNumWithoutPrint(num2))
               {
                    isSmallEqual = true;
               }
               else
               {
                    isSmallEqual = !(num1 >= num2);
               }

               return isSmallEqual;
          }

          // Gets and Sets Methods.

          public int this[int key]
          {
               get
               {
                    return mBinaryNumber[key];
               }
               set
               {
                    if ((value == 0) || (value == 1))
                    {
                         mBinaryNumber[key] = value;
                    }
                    else
                    {
                         Console.WriteLine($"The {key} digit was not changed. {value} is not valid.");
                    }
               }
          }

          public int Length
          {
               get
               {
                    if (m1MSBLocation.HasValue)
                    {
                         return GlobalVariables.MAX_NUM - m1MSBLocation.Value;
                    }
                    else
                    {
                         return 0;
                    }
               }
          }

          public int? MSB1Location
          {
               get
               {
                    return m1MSBLocation;
               }
          }

          public bool IsNegative
          {
               get
               {
                    return (mBinaryNumber[0] == 1);
               }
          }

          public long? DecimalValue
          {
               get
               {
                    return BigToLong();
               }
          }

          public string BinaryValue
          {
               get
               {
                    return WriteNum();
               }
          }
     }
}
