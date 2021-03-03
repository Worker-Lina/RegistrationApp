using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationApp.Service
{
    public class CryptService
    {
        public Guid GetHashString(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);

            var cryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] byteHash = cryptoServiceProvider.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash);
        }
    }
}

