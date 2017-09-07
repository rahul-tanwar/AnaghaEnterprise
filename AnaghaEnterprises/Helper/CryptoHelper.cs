using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AnaghaEnterprises.Helper
{
    public static  class CryptoHelper
    {
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            string key = ConfigurationManager.AppSettings["EncryptionKey"];
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            var tDes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var cTransform = tDes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tDes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cypherString)
        {
            byte[] keyArray;
            var toDecryptArray = Convert.FromBase64String(cypherString);
            string key = ConfigurationManager.AppSettings["EncryptionKey"];
            {
                var hashmd = new MD5CryptoServiceProvider();
                keyArray = hashmd.ComputeHash(Encoding.UTF8.GetBytes(key));
                hashmd.Clear();
            }
            var tDes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var cTransform = tDes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

            tDes.Clear();
            return Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);
        }
    }
}
