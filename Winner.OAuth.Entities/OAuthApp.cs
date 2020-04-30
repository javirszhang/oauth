using System;
using Winner.Framework.Utils;

namespace Winner.OAuth.Entities
{
    public class OAuthApp
    {
        [MapMember("APP_ID")]
        public int Id { get; set; }
        [MapMember("APP_CODE")]
        public string Appid { get; set; }
        public string AppName { get; set; }
        public string AppHost { get; set; }
        public string SecretKey { get; set; }
        public string UidEncryptKey { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 是否英雄会内部应用
        /// </summary>
        public bool IsInternal { get; set; }
        public string LogoUrl { get; set; }
        /// <summary>
        /// 是否允许多终端同时登陆[0 - 不允许，1 - 允许]
        /// </summary>
        public int AllowMultiLogin { get; set; }
        /// <summary>
        /// 发放的Token有效期，单位秒，默认3600s=两小时
        /// </summary>
        public int TokenExpireIn { get; set; }
    }
}
