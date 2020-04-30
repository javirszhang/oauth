using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Winner.OAuth.Entities
{
    /// <summary>
    /// 第三方登录平台
    /// </summary>
    public enum ThirdpartyLoginPlatform
    {
        [Display(Name = "微信")]
        Wechat = 4,        
        [Display(Name = "支付宝")]
        Alipay = 7,
        [Display(Name = "QQ")]
        QQ = 8,
        [Display(Name = "微博")]
        Weibo = 9,
    }
}
