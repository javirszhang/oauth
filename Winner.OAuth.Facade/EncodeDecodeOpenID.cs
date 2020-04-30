using Javirs.Common.Security;
using System;
using System.Collections.Generic;
using System.Text;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade.Caches;
using Winner.OAuth.Token;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 加解密OpenID
    /// </summary>
    public class EncodeDecodeOpenID
    {
        /// <summary>
        /// 加密OpenID
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userId"></param>
        /// <param name="userCode"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptOpenId(int appid, int userId, string userCode, string key)
        {
            string enc_uuid;
            DesEncrypt(string.Concat(userId.ToString().PadLeft(8, '0'), "|", userCode), key, out enc_uuid);
            string open_id;
            DesEncrypt(appid.ToString().PadLeft(8, '0') + "_" + enc_uuid, UserToken.SECRET, out open_id);
            return open_id;
        }
        /// <summary>
        /// 解密OpenID
        /// </summary>
        /// <param name="open_id"></param>
        /// <param name="userId"></param>
        /// <param name="userCode"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static bool DecryptOpenId(string open_id, out int userId, out string userCode, out int appid)
        {
            string tokenSecret = UserToken.SECRET;
            string enc_uuid;
            DesDecrypt(open_id, tokenSecret, out enc_uuid);
            var array = enc_uuid.Split('_');
            int lambdaAppid = appid = Convert.ToInt32(array[0]);
            var app = OAuthAppCache.Get(lambdaAppid);
            string plain_uid;
            DesDecrypt(array[1], app.UidEncryptKey, out plain_uid);
            string[] sArray = plain_uid.Split('|');
            userId = Convert.ToInt32(sArray[0]);
            userCode = sArray[1];
            return true;
        }
        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static bool DesEncrypt(string plainText, string key, out string cipherText)
        {
            Encoding utf8 = Encoding.UTF8;
            try
            {
                DesEncodeDecode des = new DesEncodeDecode(key,
                        System.Security.Cryptography.CipherMode.ECB,
                        System.Security.Cryptography.PaddingMode.PKCS7,
                        true, utf8);
                byte[] input = utf8.GetBytes(plainText);
                byte[] output = des.DesEncrypt(input);
                cipherText = Base58.Encode(output);
                return true;
            }
            catch (Exception ex)
            {
                cipherText = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static bool DesDecrypt(string cipherText, string key, out string plainText)
        {
            Encoding utf8 = Encoding.UTF8;
            try
            {
                DesEncodeDecode des = new DesEncodeDecode(key,
                        System.Security.Cryptography.CipherMode.ECB,
                        System.Security.Cryptography.PaddingMode.PKCS7,
                        true, utf8);
                byte[] input = Base58.Decode(cipherText);
                byte[] output = des.DesDecrypt(input);
                plainText = utf8.GetString(output);
                return true;
            }
            catch (Exception ex)
            {
                plainText = ex.Message;
                return false;
            }
        }
    }
}
