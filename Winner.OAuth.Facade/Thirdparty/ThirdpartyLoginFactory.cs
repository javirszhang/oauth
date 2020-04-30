using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Winner.Framework.Utils;

namespace Winner.OAuth.Facade.Thirdparty
{
    public class ThirdpartyLoginFactory
    {
        public static List<ThirdpartyLoginBehavior> _mArray = new List<ThirdpartyLoginBehavior>();
        private readonly List<ThirdpartyConf> Configs;
        public ThirdpartyLoginFactory(IOptions<List<ThirdpartyConf>> options)
        {
            this.Configs = options.Value;
        }
        private List<ThirdpartyLoginBehavior> LoadBehavior(bool force = false)
        {
            if (!force && _mArray != null && _mArray.Count > 0)
            {
                return _mArray;
            }
            _mArray.Clear();
            //string conf_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_data", "thirdpartylogin.json");
            //if (!File.Exists(conf_file))
            //{
            //    Log.Error($"未找到第三方登录配置文件[{conf_file}]");
            //    return null;
            //}
            //string jsonText = File.ReadAllText(conf_file);

            //List<ThirdpartyConf> confs = Javirs.Common.Json.JsonSerializer.Deserializer<List<ThirdpartyConf>>(jsonText);
            var confs = this.Configs;
            if (HasConflict(confs))
            {
                Log.Error("第三方登录配置包含冲突");
                return null;
            }
            foreach (var item in confs)
            {
                if (item == null)
                {
                    Log.Info("第三方登录配置参数[ThirdpartyConf]为空");
                }
                var ins = CreateObjectByProvider(item.Provider, item);
                if (ins != null && _mArray.Find(it => it.Config.Platform == item.Platform) == null)
                {
                    _mArray.Add(ins);
                }
            }
            return _mArray;
        }
        /// <summary>
        /// 是否包含冲突
        /// </summary>
        /// <param name="confs"></param>
        /// <returns></returns>
        private static bool HasConflict(List<ThirdpartyConf> confs)
        {
            for (int i = 0; i < confs.Count; i++)
            {
                var cur = confs[i];
                for (int j = i + 1; j < confs.Count; j++)
                {
                    var next = confs[j];
                    if (cur.Platform == next.Platform)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public ThirdpartyLoginBehavior GetBehavior(string platform)
        {
            if (string.IsNullOrEmpty(platform))
            {
                return null;
            }
            LoadBehavior();
            var behavior = _mArray.Find(it => platform.Equals(it.Config.Platform, StringComparison.OrdinalIgnoreCase));
            return behavior;
        }
        private static ThirdpartyLoginBehavior CreateObjectByProvider(string provider, params object[] constructorParameter)
        {
            string[] mArray = null;
            if (provider.Contains(","))
            {
                mArray = provider.Split(',');
            }
            else
            {
                mArray = new string[] { "Winner.OAuth.Facade", provider };
            }
            Assembly ass = Assembly.Load(mArray[0]);
            if (ass == null)
            {
                try
                {
                    ass = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", mArray[0] + ".dll"));
                }
                catch
                {
                    return null;
                }
            }
            Type t = ass.GetType(mArray[1]);
            if (t == null)
            {
                return null;
            }
            object proto = Activator.CreateInstance(t, constructorParameter);
            if (proto == null)
            {
                Log.Info("创建第三方登录提供者对象失败");
                return null;
            }
            ThirdpartyLoginBehavior behavior = proto as ThirdpartyLoginBehavior;
            if (behavior == null)
            {
                Log.Info("强制转换失败");
                return null;
            }
            return behavior;
        }

        public static string[] SupportPlatforms()
        {
            var list = _mArray?.Select(it => it.Config.Platform);
            return list?.ToArray();
        }
        private static Assembly GetDomainAssebly(string name)
        {
            var all = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var ass in all)
            {
                var ass_name = ass.GetName();
                if (name.Equals(ass_name.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return ass;
                }
            }
            return null;
        }

    }
    public class ThirdpartyConf
    {
        public string Platform { get; set; }
        public string Provider { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string Scope { get; set; }
    }


    public class ThirdpartyUserToken
    {
        public string OpenID { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AuthCode { get; set; }
        public string State { get; set; }
    }

    public class ThirdpartyUserInfo
    {
        public int errcode { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
    }
    public class ThirdpartyAccessToken
    {
        public string AccessToken { get; set; }
        public DateTime TokenExpires { get; set; }
        public string AppID { get; set; }
    }

    public class ThirdpartyJsApiTicket : ThirdpartyAccessToken
    {
        public string Ticket { get; set; }
        public DateTime TicketExpires { get; set; }
    }
}
