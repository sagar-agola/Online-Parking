using System.Security.Cryptography;
using System.Text;

namespace PBS.Business.Utilities.Helpers
{
    public static class PasswordManager
    {
        public static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512 ())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (password));
            }
        }

        public static bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512 (passwordSalt))
            {
                var computedHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
