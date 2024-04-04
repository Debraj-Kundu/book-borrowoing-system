using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLayer.Core.Utils
{
    public static class CommonMethods
    {
        public static string key = "#secret@password!1hashing_key$";

        public static string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string Decrypt(string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
                return string.Empty;
            var base64EncodeBytes = Convert.FromBase64String(base64EncodedData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }
    }
}
