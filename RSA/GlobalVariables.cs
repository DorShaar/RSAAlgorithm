using System;

namespace RSA
{
     public static class GlobalVariables
     {
          public const int MAX_NUM = 400;
          public const int MaxNumberOfDigitsInLongType = 63;
          //public static readonly int MaxNumberOfDigitsInLongType = (Convert.ToString(long.MaxValue)).Length;
          public static readonly BigNum zero = new BigNum(0);
          public static readonly BigNum one = new BigNum(1);
          public static readonly BigNum two = new BigNum(2);
          public static readonly BigNum minusOne = new BigNum(-1);
     }
}
