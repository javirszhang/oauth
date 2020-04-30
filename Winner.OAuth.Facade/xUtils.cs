using Javirs.Common;
using Javirs.Common.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Winner.Framework.Utils;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    public static class xUtils
    {
        public static string CustomAccountPrefix
        {
            get { return Winner.ConfigurationManager.ConfigurationProvider.GetString(nameof(CustomAccountPrefix), "12"); }
        }
        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsMobileNo(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            string pattern = "^1[3456789]{1}\\d{9}$";
            return Regex.IsMatch(s, pattern);
        }
        public static UserVoucherType GetVoucherType(string userCode)
        {
            if (string.IsNullOrEmpty(userCode))
            {
                throw new ArgumentNullException("userCode");
            }
            string emailPattern = @"^[a-zA-Z0-9_\-\.]+@[\w\-]+(\.[\w\-]+)+$";
            if (userCode.IsMobileNo())
            {
                return UserVoucherType.手机号;
            }
            else if (Regex.IsMatch(userCode, emailPattern))
            {
                return UserVoucherType.邮箱;
            }
            else if (userCode.StartsWith("2088"))
            {
                return UserVoucherType.ALIPAY_UID;
            }
            else
            {
                return UserVoucherType.自定义号码;
            }
            throw new ApplicationException("无效的账号类型");
        }

        public static long GetCurrentTimeStamp()
        {
            TimeStamp stamp = DateTime.Now;
            return (long)stamp.Seconds;
        }

        public static string Base58ToBase64(string base58)
        {
            return Convert.ToBase64String(Base58.Decode(base58));
        }
        public static string Base64ToBase58(string base64)
        {
            return Base58.Encode(Convert.FromBase64String(base64));
        }
        public static bool RsaDecrypt(string base58Cipher, out string plainText)
        {
            try
            {
                string fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "yxhpaypwd_private_key.pem");
                Log.Debug("证书保存路径：" + fullpath);
                var rsa = PemCertificate.ReadFromPemFile(fullpath);
                byte[] cipherBytes = Base58.Decode(base58Cipher); //base58Cipher.HexString2ByteArray();
                byte[] byteRes = rsa.Decrypt(cipherBytes, false);
                plainText = Encoding.UTF8.GetString(byteRes);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("支付密码解密失败", ex);
                plainText = null;
                return false;
            }
        }
        public static string GetClientSource(int source)
        {
            string comsource = "UNKNOWN";
            switch (source)
            {
                case 1:
                    comsource = "Android";
                    break;
                case 2:
                    comsource = "iOS";
                    break;
                case 3:
                    comsource = "PC";
                    break;
            }
            return comsource;
        }
        public static string TransformFailRetCode(int code)
        {
            if (code <= 0)
            {
                return "0500";
            }
            return code.ToString().PadLeft(4, '0');
        }

        public static string TransformFailRetCode(ResultType resultType)
        {

            return TransformFailRetCode((int)resultType);

        }
        public static string GetDefaultUserName(string mobileno)
        {
            string name = System.Configuration.ConfigurationManager.AppSettings["DefaultUserNamePrefix"];
            if (mobileno.Length >= 4)
            {
                name += mobileno.Substring(mobileno.Length - 4);
            }
            else
            {
                name += mobileno;
            }
            return name;
        }

        public static string GetDisplayText(this Enum instance)
        {
            Type enumType = instance.GetType();
            var values = Enum.GetValues(enumType);
            var name = Enum.GetName(enumType, instance);
            var field = enumType.GetField(name);
            var display = field.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();
            if (display == null)
            {
                name = display.Name;
            }
            return name;
        }
    }
}
