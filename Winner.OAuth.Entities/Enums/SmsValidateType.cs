using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.OAuth.Entities
{
    public enum SmsValidateType
    {
        注册 = 0,
        重置登录密码 = 1,
        重置支付密码 = 2,
        绑定手机号 = 3,
        登录验证码 = 6
    }
}
