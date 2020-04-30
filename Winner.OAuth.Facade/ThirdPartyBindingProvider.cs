using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 第三方登录绑定手机号业务
    /// </summary>
    public class ThirdPartyBindingProvider : FacadeBase
    {
        private ThirdPartyBindingModel _model;
        /// <summary>
        /// 第三方登录绑定手机号业务
        /// </summary>
        public ThirdPartyBindingProvider(ThirdPartyBindingModel model)
        {
            this._model = model;
        }
        public IUser User { get; private set; }
        public bool Register()
        {
            string[] array = new string[2];
            string plainText;
            if (!xUtils.RsaDecrypt(_model.AuthCode, out plainText))
            {
                Alert("OpenID解密失败");
                return false;
            }
            int pos = plainText.IndexOf('_');
            array[0] = plainText.Substring(0, pos);
            array[1] = plainText.Substring(pos + 1);
            long timestamp;
            if (!long.TryParse(array[0], out timestamp))
            {
                Alert("OpenID解密失败");
                return false;
            }
            long currentTime = xUtils.GetCurrentTimeStamp();
            if (currentTime - timestamp > 120)
            {
                Alert("请求已过期");
                return false;
            }
            string openID = array[1];
            SmsValidateProvider smsValidate = new SmsValidateProvider(_model.MobileNo, SmsValidateType.绑定手机号);
            if (!smsValidate.ValidateCode(_model.ValidateCode))
            {
                Alert(smsValidate.PromptInfo);
                return false;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                Alert("系统模块异常");
                return false;
            }
            if (!Enum.TryParse(_model.Platform, true, out ThirdpartyLoginPlatform platform))
            {
                Alert($"无效的第三方登录平台[{_model.Platform}]");
                return false;
            }
            UserVoucherType uvt = (UserVoucherType)platform;
            IUser thirdpartyUser = fac.GetUserByVoucher(openID, uvt);
            if (thirdpartyUser == null)
            {
                IUser user = fac.GetUserByMobileno(_model.MobileNo);
                if (user == null)
                {
                    var voucher = fac.GetVoucherObject();
                    voucher.AllowLogin = true;
                    voucher.IsValid = true;
                    voucher.Status = 1;
                    voucher.UserCode = _model.MobileNo;
                    voucher.VoucherType = UserVoucherType.手机号;
                    UserCreationProvider ucp = new UserCreationProvider(openID, null, _model.RefereeCode, uvt);
                    ucp.Vouchers.Add(voucher);
                    if (!ucp.AddUser(_model.NickName ?? xUtils.GetDefaultUserName(_model.MobileNo), _model.Avatar))
                    {
                        Alert(ucp.PromptInfo);
                        return false;
                    }
                    user = ucp.User;
                }
                else
                {
                    var voucher = fac.GetVoucherObject();
                    voucher.AllowLogin = true;
                    voucher.IsValid = true;
                    voucher.Status = 1;
                    voucher.UserCode = openID;
                    voucher.VoucherType = uvt;
                    if (!voucher.Save(user.UserId))
                    {
                        Alert((ResultType)503, "已有账号绑定第三方登录失败");
                        return false;
                    }
                    user.Refresh();
                }
                this.User = user;
            }
            else
            {
                var thirdpartyVoucher = thirdpartyUser.Vouchers?.Find(it => it.VoucherType == uvt);
                if (thirdpartyVoucher != null)
                {
                    Alert((ResultType)409, $"该账号[{_model.MobileNo}]已绑定{platform.GetDisplayText()}");
                    return false;
                }
                var voucher = fac.GetVoucherObject();
                voucher.AllowLogin = true;
                voucher.IsValid = true;
                voucher.Status = 1;
                voucher.UserCode = _model.MobileNo;
                voucher.VoucherType = UserVoucherType.手机号;
                if (!voucher.Save(thirdpartyUser.UserId))
                {
                    Alert((ResultType)503, "已有账号绑定第三方登录失败");
                    return false;
                }
                thirdpartyUser.Refresh();
                this.User = thirdpartyUser;
            }
            return true;
        }
    }
    public class ThirdPartyBindingModel
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 第三方平台登录授权码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 第三方平台名称(qq,wechat,alipay,weibo)
        /// </summary>
        public string Platform { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 推荐人账号
        /// </summary>
        public string RefereeCode { get; set; }
        /// <summary>
        /// 短信验证码
        /// </summary>
        public string ValidateCode { get; set; }
    }
}
