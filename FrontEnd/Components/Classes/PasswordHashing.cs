using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace FrontEnd.Components.Classes
{
    public class PasswordHashing
    {
        private const int _MemorySize = 65536;
        private const int _ParallelismDegree = 2;
        private const int _Iterations = 5;
        private const int _SaltSize = 16;
        private const int _HashSize = 32;

        public byte[] CreateSalt()
        {
            return RandomNumberGenerator.GetBytes(_SaltSize);
        }
        public string HashPassword(string pass, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pass);

            using var arg2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = _ParallelismDegree,
                Iterations = _Iterations,
                MemorySize = _MemorySize
            };
            byte[] hash = arg2.GetBytes(_HashSize);
            return Convert.ToHexString(hash);
        }

    }
}
