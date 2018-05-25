using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMA.Common.EncryptionHelper
{   
    /// <summary>
    /// 张亮辉 2016-8-11
    /// </summary>
     public  class Encryption
    {

        private static Encryption _instance;

        private static object _lock = new object();
        public static Encryption GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Encryption();
                }
            }
            return _instance;
        }


        #region Base64
        /// <summary>
        /// string转Base64
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// string转Base64
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64转string
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64转string
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        #region DES加密解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位字符的密钥字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public string DESEncrypt(string data, string key, string iv)
        {
            byte[] byKey = Encoding.ASCII.GetBytes(key);
            byte[] byIV = Encoding.ASCII.GetBytes(iv);
            using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cst))
                        {
                            sw.Write(data);
                            sw.Flush();
                            cst.FlushFinalBlock();
                            sw.Flush();
                            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public string DESDecrypt(string data, string key, string iv)
        {
            byte[] byKey = Encoding.ASCII.GetBytes(key);
            byte[] byIV = Encoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch (Exception ex)
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
        #endregion

        #region MD5签名
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public string MD5Encrypt(string input)
        {
            return MD5Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// MD5签名
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public string MD5Encrypt(string input, Encoding encode)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] buffer = md5.ComputeHash(encode.GetBytes(input));
                return BitConverter.ToString(buffer).Replace("-", "");
            }
        }

        /// <summary>
        /// MD5对文件流签名
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string MD5Encrypt(Stream stream)
        {
            using (MD5 md5 = MD5CryptoServiceProvider.Create())
            {
                byte[] buffer = md5.ComputeHash(stream);
                return BitConverter.ToString(buffer).Replace("-", "");
            }
        }

        /// <summary>
        /// MD5签名(返回16位加密串)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string MD5Encrypt16(string input, Encoding encode)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(md5.ComputeHash(encode.GetBytes(input)), 4, 8).Replace("-", "");
            }
        }
        #endregion

        #region 3DES 加密解密

        public string DES3Encrypt(string data, string key)
        {
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
                using (ICryptoTransform DESEncrypt = DES.CreateEncryptor())
                {
                    byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(data);
                    return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
            }
        }

        public string DES3Decrypt(string data, string key)
        {
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
                using (ICryptoTransform DESDecrypt = DES.CreateDecryptor())
                {
                    byte[] Buffer = Convert.FromBase64String(data);
                    return ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
            }
        }

        #endregion
    }
}
