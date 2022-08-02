using System;
using System.Security.Cryptography;
using System.Text;

namespace TechChallenge.Core.Utils
{
    public static class Md5Generator
    {
        public static string CreateMD5(string input)
        {
            // Creates an instance of the default implementation of the MD5 hash algorithm.
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(input);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash.ToLower();
            }
        }
    }
}
