using System;
using System.Security.Cryptography;

namespace Squabby.Helpers.Cryptography
{
    public static class Random
    {
        /// <summary>
        /// Generate random string
        /// </summary>
        /// <param name="length">length of string in chars</param>
        /// <returns>random base64 string</returns>
        public static string GetSecureRandomString(int length)
        {
            // 4 chars = 3 bytes
            var data = new byte[(length/4)*3];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}