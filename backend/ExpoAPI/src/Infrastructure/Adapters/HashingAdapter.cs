using Microsoft.AspNetCore.DataProtection;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ExpoAPI.Infrastructure.Adapters
{
    public class HashingAdapter : IHashingAdapter
    {
        public string HashPassword(string username)
        {
            using (var sha256 = SHA256.Create())
            {
                const string key = "expoapi";
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{username}{key}"));
                return BitConverter.ToString(hashedBytes).Replace("-", "");
            }
        }
    }
}
