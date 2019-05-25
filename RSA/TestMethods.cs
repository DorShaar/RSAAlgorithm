using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class TestMethods
    {
        public static void MSB1LocationTest()
        {
            Console.WriteLine("Creating new BigNum type number.");
            BigNum bigNum = new BigNum();
            for (int i = 0; i < GlobalVariables.MAX_NUM; ++i)
            {
                Console.Write(bigNum[i]);
            }

            Console.WriteLine($"The first 1' digit is in position: {bigNum.MSB1Location}");
            Console.WriteLine();
        }

        public static void A()
        {
            Console.WriteLine("(A) Using ReadNum methods.");
            Console.WriteLine("1. reading 0101");
            BigNum bigNum1a = new BigNum();
            bigNum1a.ReadNum("0101");
            Console.WriteLine(bigNum1a.WriteNum());
            Console.WriteLine($"The first 1' digit is in position: {bigNum1a.MSB1Location}");

            Console.WriteLine("2. reading positions of 1s. Getting a the list {399, 385}.");
            BigNum bigNum2a = new BigNum();
            bigNum2a.ReadNum(new List<int> { 399, 385 });
            Console.WriteLine(bigNum2a.WriteNum());
            Console.WriteLine($"The first 1' digit is in position: {bigNum2a.MSB1Location}");

            Console.WriteLine();
        }

        public static void B()
        {
            Console.WriteLine("(B) Using WriteNum method.");

            BigNum bigNum1a = new BigNum();
            bigNum1a.ReadNum("0101");
            BigNum bigNum2a = new BigNum();
            bigNum2a.ReadNum(new List<int> { 399, 385 });

            List<int> bigNum1PositionsOf1s = bigNum1a.WriteNum1sPosition();
            List<int> bigNum2PositionsOf1s = bigNum2a.WriteNum1sPosition();
            Console.Write("first number 1 digit positions are at: ");
            foreach (var pos in bigNum1PositionsOf1s)
            {
                Console.Write(pos);
                Console.Write(",");
            }

            Console.WriteLine();
            Console.Write("second number 1 digit positions are at: ");
            foreach (var pos in bigNum2PositionsOf1s)
            {
                Console.Write(pos);
                Console.Write(",");
            }

            Console.WriteLine();
        }

        public static void C()
        {
            Console.WriteLine("(C) Using LongToBig method.");
            Console.WriteLine("Creating number with long 35.");
            BigNum bigNumC1 = new BigNum(35L);
            Console.WriteLine(bigNumC1.WriteNum());
            Console.WriteLine("Creating number with long 9223372036854775807 (max value of long).");
            BigNum bigNumC2 = new BigNum(9223372036854775807L);
            Console.WriteLine(bigNumC2.WriteNum());

            Console.WriteLine();
        }

        public static void D()
        {
            Console.WriteLine("(D) Using BigToLong method.");
            BigNum bigNumC1 = new BigNum(35L);
            long? num1 = bigNumC1.BigToLong();
            Console.WriteLine(num1);

            BigNum bigNumC2 = new BigNum(9223372036854775807L);
            long? num2 = bigNumC2.BigToLong();
            Console.WriteLine(num2);

            BigNum bigNumD = new BigNum();
            bigNumD.Complement();
            long? num3 = bigNumD.BigToLong();
            Console.WriteLine(num3);

            Console.WriteLine();
        }

        public static void E()
        {
            Console.WriteLine("(E) Using AddNum method.");
            Console.WriteLine("8 + 13 = 21.");
            BigNum bigNumE1 = new BigNum(8L);
            BigNum bigNumE2 = new BigNum(13L);
            bigNumE1.AddNum(bigNumE2);
            Console.WriteLine(bigNumE1.WriteNum());

            Console.WriteLine();
        }

        public static void F()
        {
            Console.WriteLine("(F) Using ShiftLeft method.");
            Console.WriteLine("Multiply 25 by 2.");
            BigNum bigNumF = new BigNum(25L);
            bigNumF.ShiftLeft();
            Console.WriteLine(bigNumF.WriteNum());

            Console.WriteLine();
        }

        public static void G()
        {
            Console.WriteLine("(G) Using MultNum method.");
            Console.WriteLine("Multiply 7 by 3. Equals 21 (10101 in base 2)");
            BigNum bigNumG1 = new BigNum(7L);
            BigNum bigNumG2 = new BigNum(3L);
            bigNumG1.MultNum(bigNumG2);
            Console.WriteLine(bigNumG1.WriteNum());

            Console.WriteLine("Multiply 113 by 73. Equals 8249 (10000000111001 in base 2)");
            BigNum bigNumG3 = new BigNum(113L);
            BigNum bigNumG4 = new BigNum(73L);
            bigNumG3.MultNum(bigNumG4);
            Console.WriteLine(bigNumG3.WriteNum());

            Console.WriteLine();
        }

        public static void H()
        {
            Console.WriteLine("(G) Using CompNum method.");
            Console.WriteLine("Comparing between zeroes.");
            BigNum bigNumH1 = new BigNum();
            BigNum bigNumH2 = new BigNum();
            bigNumH1.CompNum(bigNumH2);

            Console.WriteLine("Comparing between 8 and 16.");
            BigNum bigNumH3 = new BigNum(8L);
            BigNum bigNumH4 = new BigNum(16L);
            bigNumH3.CompNum(bigNumH4);

            Console.WriteLine("Comparing between 32 and 32.");
            BigNum bigNumH5 = new BigNum(32L);
            BigNum bigNumH6 = new BigNum();
            var positionsOf1sH = new List<int> { 394 };
            bigNumH6.ReadNum(positionsOf1sH);
            bigNumH5.CompNum(bigNumH6);

            Console.WriteLine();
        }

        public static void I()
        {
            Console.WriteLine("(I) Using SubNum method.");
            Console.WriteLine("18 - 5 = 13");
            BigNum bigNumI1 = new BigNum(18L);
            BigNum bigNumI2 = new BigNum(5L);
            bigNumI1.SubNum(bigNumI2);
            Console.WriteLine(bigNumI1.WriteNum());

            Console.WriteLine("375683 - 12345 = 363338 (1011000101101001010 in binary.)");
            BigNum bigNumI3 = new BigNum(375683L);
            BigNum bigNumI4 = new BigNum(12345L);
            bigNumI3.SubNum(bigNumI4);
            Console.WriteLine(bigNumI3.WriteNum());

            Console.WriteLine();
        }

        public static void J()
        {
            Console.WriteLine("(I) Using DivNum method.");
            Console.WriteLine("257 / 13 ==> result = 19, reminder 10.");

            BigNum divided = new BigNum(257L);
            BigNum divider = new BigNum(13L);
            BigNum quitient = new BigNum();
            BigNum reminder = new BigNum();
            MathOps.DivNum(divided, divider, quitient, reminder);

            Console.WriteLine($"Quitient is: {quitient.WriteNum()}, Reminder is: {reminder.WriteNum()}.");
            Console.WriteLine();
        }

        public static void K()
        {
            Console.WriteLine("(K) Using ExtendedGCD method.");

            Console.WriteLine("Calculates GCD(26, 21). Should be = 1");
            BigNum bigNum3 = new BigNum(26);
            BigNum bigNum4 = new BigNum(21);
            BigNum x = new BigNum();
            BigNum y = new BigNum();
            BigNum gcd = new BigNum();
            MathOps.ExtendedGCD(bigNum4, bigNum3, x, y, gcd);
            Console.WriteLine($"GCD = {gcd.WriteNum()} = {bigNum3.DecimalValue}*{x.DecimalValue}" +
                 $" + {bigNum4.DecimalValue}*{y.DecimalValue}");

            Console.WriteLine("Calculates GCD(15, 10). Should be = 5");
            BigNum bigNum1 = new BigNum(15);
            BigNum bigNum2 = new BigNum(10);
            MathOps.ExtendedGCD(bigNum2, bigNum1, x, y, gcd);
            Console.WriteLine($"GCD = {gcd.DecimalValue} = {bigNum1.DecimalValue}*{x.DecimalValue}" +
                 $" + {bigNum2.DecimalValue}*{y.DecimalValue}");
            Console.WriteLine();
        }

        public static void L()
        {
            Console.WriteLine("(L) Using Inverse method.");
            BigNum bigNum1 = new BigNum(26);
            BigNum bigNum2 = new BigNum(21);

            Console.WriteLine("Calculates the opposite number of 26 mod 21:");
            Console.WriteLine(MathOps.Inverse(bigNum1, bigNum2).DecimalValue);
            Console.WriteLine("Calculates the opposite number of 21 mod 26:");
            Console.WriteLine(MathOps.Inverse(bigNum2, bigNum1).DecimalValue);

            BigNum bigNum3 = new BigNum(15);
            BigNum bigNum4 = new BigNum(10);
            Console.WriteLine("Calculates the opposite number of 15 mod 10:");
            if (MathOps.Inverse(bigNum3, bigNum4) == null)
            {
                Console.WriteLine("There is no opposite for 15");
            }

            Console.WriteLine();
        }

        public static void M()
        {
            Console.WriteLine("(M) Using Power method.");
            Console.WriteLine("Calculating 30^14 mod 7 = 4.");
            BigNum x = new BigNum(30);
            BigNum power = new BigNum(14);
            BigNum mod = new BigNum(7);
            Console.WriteLine(MathOps.Power(x, power, mod).DecimalValue);

            Console.WriteLine("Calculating 130^27 mod 41 = 24.");
            BigNum x2 = new BigNum(130);
            BigNum power2 = new BigNum(27);
            BigNum mod2 = new BigNum(41);
            Console.WriteLine(MathOps.Power(x2, power2, mod2).DecimalValue);

            Console.WriteLine();
        }

        public static void N()
        {
            Console.WriteLine("(N) Using IsPrime method.");
            Console.WriteLine("Checking if 7, 513, 9973, 2147483647. We should get: true, false, true, true.");
            BigNum n1 = new BigNum(7);
            BigNum n2 = new BigNum(513);
            BigNum n3 = new BigNum(9973);
            BigNum eightMarsenNumber = new BigNum(2147483647);
            int k = 20;

            Console.WriteLine(MathOps.IsPrime(n1, k).ToString());
            Console.WriteLine(MathOps.IsPrime(n2, k).ToString());
            Console.WriteLine(MathOps.IsPrime(n3, k).ToString());
            Console.WriteLine(MathOps.IsPrime(eightMarsenNumber, k).ToString());

            Console.WriteLine();
        }

        public static void O()
        {
            Console.WriteLine("(O) Using GenPrime method.");
            Console.WriteLine($"Generated number is: {MathOps.GenPrime().BinaryValue}");

            Console.WriteLine();
        }

        public static void IIA()
        {
            Console.WriteLine("I.");
            Console.WriteLine("Calculating 1233456897 ^ 8001 mod 1783369431");
            BigNum num = new BigNum(1233456897);
            BigNum power = new BigNum(8001);
            BigNum mod = new BigNum(1783369431);
            BigNum result = new BigNum();
            result = MathOps.Power(num, power, mod);
            Console.WriteLine(result.DecimalValue);

            Console.WriteLine();
        }

        public static void IIB()
        {
            Console.WriteLine("II.");
            Console.WriteLine("Finds the first five prime numbers with 100 binary digits");
            int countPrimeNumberFound = 0;
            int tryNumber = 0;

            while (countPrimeNumberFound < 5)
            {
                List<int> positionsOf1s = new List<int> // Trusting that the number will have 100 binary digits.
                    {
                         GlobalVariables.MAX_NUM - 100,
                         GlobalVariables.MAX_NUM - 1
                    };
                int k = 20;

                BigNum primeNumberSuspect = new BigNum();
                primeNumberSuspect.ReadNum(positionsOf1s);
                Console.WriteLine($"try number: {++tryNumber}");
                if (MathOps.IsPrime(primeNumberSuspect, k))
                {
                    Console.WriteLine($"Prime {countPrimeNumberFound}: {primeNumberSuspect.BinaryValue}");
                    Console.WriteLine();
                }

                primeNumberSuspect.AddNum(GlobalVariables.two);
            }

            Console.WriteLine();
        }

        public static void IIC()
        {
            Console.WriteLine("III.");
            Console.WriteLine("Using RSA");

            //BigNum b = new BigNum();
            //List<int> bPositionsOf1s = new List<int>
            //{
            //     GlobalVariables.MAX_NUM - 16 - 1,
            //     GlobalVariables.MAX_NUM - 1
            //};
            //b.ReadNum(bPositionsOf1s);

            BigNum b = new BigNum();
            List<int> bPositionsOf1s = new List<int>
            {
                    GlobalVariables.MAX_NUM - 5 - 1,
                    GlobalVariables.MAX_NUM - 1
            };
            b.ReadNum(bPositionsOf1s);
            Console.Write("b = ");
            MathOps.PrintNumberInPowersOfTwo(b);

            BigNum plainText = new BigNum();
            List<int> plainTextPositionsOf1s = new List<int>
            {
                GlobalVariables.MAX_NUM - 197 - 1,
                GlobalVariables.MAX_NUM - 100 - 1,
                GlobalVariables.MAX_NUM - 50 - 1,
                GlobalVariables.MAX_NUM - 6 - 1,
                GlobalVariables.MAX_NUM - 1 - 1
            };
            plainText.ReadNum(plainTextPositionsOf1s);
            Console.Write("Encrypting the number: ");
            MathOps.PrintNumberInPowersOfTwo(plainText);

            BigNum cipherText = new BigNum();
            BigNum a = new BigNum();
            BigNum n = new BigNum();
            BigNum p = new BigNum();
            BigNum q = new BigNum();

            Console.WriteLine("Generating RSA");
            MathOps.GenRSA(b, a, p, q, n);
            Console.WriteLine("Encrypting plaintext to:");
            cipherText = MathOps.Encrypt(plainText, b, n);
            MathOps.PrintNumberInPowersOfTwo(cipherText);

            Console.WriteLine("Decrypting ciphertext to:");
            BigNum decryptedText = MathOps.Decrypt(cipherText, a, n);
            MathOps.PrintNumberInPowersOfTwo(decryptedText);

            plainText.CompNum(decryptedText);

            Console.WriteLine();
        }

        public static void IID()
        {
            BigNum n = new BigNum(55687);
            BigNum i = new BigNum(1L);
            int count = 0;

            BigNum y = new BigNum(41008L);
            BigNum a = new BigNum(23425L);
            BigNum x = MathOps.Power(y, a, n);
            Console.WriteLine(x.DecimalValue);

            //while (i<=n)
            //{
            //    BigNum gcd = new BigNum();
            //    MathOps.ExtendedGCD(i, n, new BigNum(), new BigNum(), gcd);
            //    if (!gcd.CompNumWithoutPrint(GlobalVariables.one))
            //    {
            //        count++;
            //        Console.WriteLine(i.DecimalValue);
            //    }

            //    i.AddNum(GlobalVariables.one);
            //}

            Console.WriteLine(count);
        }
    }
}
