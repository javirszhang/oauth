using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Utils;

namespace Winner.OAuth.Entities
{
    public class AppConfig
    {
        /// <summary>
        /// 注册时推荐人是否为必填，true means required,otherwise false!
        /// </summary>
        public static bool RegisterRefereeRequired
        {
            get
            {
                string tmp = ConfigProvider.GetAppSetting(nameof(RegisterRefereeRequired));
                return "true".Equals(tmp, StringComparison.OrdinalIgnoreCase);
            }
        }
        /// <summary>
        /// 手机号码验证是否已禁用，true表示已禁用，false表示已启用
        /// </summary>
        public static bool DisableMobileVerification
        {
            get
            {
                string val = ConfigProvider.GetAppSetting(nameof(DisableMobileVerification));
                return "true".Equals(val, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
