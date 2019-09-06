using System;

namespace PBS.Business.Utilities.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public string Encrypt (string inputString, int key)
        {
            string padding = DateTime.Now.DayOfWeek.ToString ().Substring (0, 3);
            inputString += padding;

            char[] ans = new char[inputString.Length];

            for (int i = 0; i < inputString.Length; i++)
            {
                int temp = Convert.ToInt32 (inputString[i]);
                temp += (i + key);

                ans[i] = Convert.ToChar (temp);
            }

            return new string (ans);
        }
        public string Decrypt (string inputString, int key)
        {
            char[] ans = new char[inputString.Length];

            for (int i = 0; i < inputString.Length; i++)
            {
                int temp = Convert.ToInt32 (inputString[i]);
                temp -= (i + key);

                if (temp < 300 && temp > 0)
                {
                    ans[i] = Convert.ToChar (temp);
                }
            }

            string answer = new string (ans);

            answer = answer.Substring (0, answer.Length - 3);

            return answer;
        }
    }
}
