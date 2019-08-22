using System;

namespace PBS.Business.Utilities.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public string Encrypt (string inputString, int key)
        {
            char[] ans = new char[inputString.Length];

            for (int i = 0; i < inputString.Length; i++)
            {
                int temp = GetASCIIValue (inputString[i]);
                temp += (i + key);

                ans[i] = GetCharFromASCII (temp);
            }

            return new string (ans);
        }
        public string Decrypt (string inputString, int key)
        {
            char[] ans = new char[inputString.Length];

            for (int i = 0; i < inputString.Length; i++)
            {
                int temp = GetASCIIValue (inputString[i]);
                temp -= (i + key);

                if (temp < 300 && temp > 0)
                {
                    ans[i] = GetCharFromASCII (temp);
                }
            }

            return new string (ans);
        }
        private char GetCharFromASCII (int ch)
        {
            return Convert.ToChar (ch);
        }

        private int GetASCIIValue (char ch)
        {
            return Convert.ToInt32 (ch);
        }
    }
}
