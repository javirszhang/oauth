using System;
using System.Collections.Generic;
using System.Text;
using Winner.OAuth.Facade.Caches;

namespace Winner.OAuth.Facade
{
    public class OAuthOpenID
    {
        /// <summary>
        /// 接入APPID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 会员账号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// OpenID的隐式转换
        /// </summary>
        /// <param name="openId"></param>
        public static implicit operator OAuthOpenID(string openId)
        {
            int appid, userId;
            string userCode;
            if (!EncodeDecodeOpenID.DecryptOpenId(openId, out userId, out userCode, out appid))
            {
                return null;
            }
            OAuthOpenID open = new OAuthOpenID
            {
                AppId = appid,
                UserId = userId,
                UserCode = userCode
            };
            return open;
        }
        /// <summary>
        /// 生成加密的openid
        /// </summary>
        /// <returns></returns>
        public string ToCipherString()
        {
            var app = OAuthAppCache.Get(this.AppId);
            if (app == null)
            {
                throw new ApplicationException("openid生成失败，无效的应用ID[APPID]");
            }
            string cipherText = EncodeDecodeOpenID.EncryptOpenId(AppId, UserId, UserCode, app.UidEncryptKey);
            return cipherText;
        }
    }
}
