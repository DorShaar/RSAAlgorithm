using System.Text;

namespace Main
{
     class Decipher
     {
          public static string DecipherString(string encryptedString, int keyA, int keyB,
               MultGroup multGroup, AddGroup addGroup)
          {
               StringBuilder decipheredString = new StringBuilder();
               foreach (char ch in encryptedString)
               {
                    int y = CalcUtilities.GetEnglishLetterIndex(ch);
                    int minusOperationResult = addGroup.GetNegativeNumber((y - keyB));
                    int inverseNumber = multGroup.GetInverseNumber(keyA);
                    int x = (minusOperationResult * inverseNumber) % multGroup.Mod;
                    x = x <= 0 ? -x : x; // Makes X positive.
                    decipheredString.Append(CalcUtilities.GetUpperEnglishLetterByIndex(x));
               }

               return decipheredString.ToString();
          }
     }
}
