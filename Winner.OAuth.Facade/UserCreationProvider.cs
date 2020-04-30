using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils.Model;
using Winner.OAuth.Entities;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 创建会员账号业务
    /// </summary>
    public class UserCreationProvider : FacadeBase
    {
        private string _refereeCode, _password, _userCode;
        private UserVoucherType _uvt;
        /// <summary>
        /// 创建会员账号业务
        /// </summary>
        /// <param name="userCode">会员账号</param>
        /// <param name="password">指定登录密码</param>
        /// <param name="refereeCode">指定推荐人</param>
        /// <param name="userId">指定会员ID</param>
        public UserCreationProvider(string userCode, string password, string refereeCode, UserVoucherType uvt = UserVoucherType.手机号)
        {
            this._userCode = userCode;
            this._password = password;
            this._refereeCode = refereeCode;
            this._uvt = uvt;
            this.Vouchers = new List<IUserVoucher>();
        }
        public List<IUserVoucher> Vouchers { get; private set; }
        /// <summary>
        /// 新增会员
        /// </summary>
        /// <returns></returns>
        public bool AddUser(string userName = null, string avatar = null)
        {
            InvitationCodeProvider provider = new InvitationCodeProvider(_refereeCode);
            IUser associateUser = provider.GetAssociateUser();
            if (!string.IsNullOrEmpty(_refereeCode) && associateUser == null)
            {
                Alert("无效的邀请码");
                return false;
            }
            int? refereeId = associateUser?.UserId;
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                Alert("未找到会员模块提供程序");
                return false;
            }
            if (IsUserCodeAlreadyExist(this._userCode, this._uvt, fac))
            {
                Alert("账号已注册，请勿重复注册");
                return false;
            }
            IUser user = fac.GetUserObject();
            user.UserName = userName ?? GetDefaultUserName(_userCode);
            user.Status = UserStatus.已激活;
            user.Grade = fac.UserGradeObject(0);
            user.Refer_ID = refereeId;
            user.Auth_Status = Auth_Status.未认证;
            user.Avatar = avatar;
            string cusCode = GetCustomeAccount();
            IUserVoucher cusVoucher = fac.GetVoucherObject();
            cusVoucher.AllowLogin = true;
            cusVoucher.IsValid = true;
            cusVoucher.UserCode = cusCode;
            cusVoucher.VoucherType = UserVoucherType.自定义号码;
            cusVoucher.Status = 1;
            Vouchers.Add(cusVoucher);

            IUserVoucher mobileVoucher = fac.GetVoucherObject();
            mobileVoucher.AllowLogin = true;
            mobileVoucher.IsValid = !(this._uvt == UserVoucherType.手机号 && AppConfig.DisableMobileVerification);
            mobileVoucher.UserCode = _userCode;
            mobileVoucher.VoucherType = this._uvt;
            mobileVoucher.Status = 1;
            Vouchers.Add(mobileVoucher);
            Dictionary<PasswordType, string> pwdDictionary = new Dictionary<PasswordType, string>();
            if (!string.IsNullOrEmpty(_password))
            {
                pwdDictionary.Add(PasswordType.登录密码, _password);
            }
            var profile = fac.GetProfileManager();
            if (!profile.Create(user, Vouchers, pwdDictionary))
            {
                Alert(profile.ErrorInfo.Message);
                return false;
            }
            this.User = fac.GetUserByCode(cusCode);
            return true;
        }
        public IUser User { get; private set; }
        private static FuncResult<int?> GetIntroducerId(string refereeCode)
        {
            if (string.IsNullOrEmpty(refereeCode) && AppConfig.RegisterRefereeRequired)
            {
                return FuncResult.FailResult<int?>("必须填写推荐人");
            }

            if (string.IsNullOrEmpty(refereeCode))
            {
                return FuncResult.SuccessResult(default(int?));
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser refereeUser = null;
            if (refereeCode.StartsWith("U"))
            {
                refereeUser = fac?.GetUserByCode(refereeCode);
            }
            else
            {
                int userid;
                if (!int.TryParse(refereeCode.Replace("U", ""), out userid))
                {
                    return FuncResult.FailResult<int?>("无效的邀请码");
                }
                refereeUser = fac?.GetUserByID(userid);
            }
            if (refereeUser == null)
            {
                return FuncResult.FailResult<int?>("推荐人账号不存在");
            }
            return FuncResult.SuccessResult(refereeUser?.UserId);
        }

        public event Action<IUser> Success;
        private void OnSuccess(IUser user)
        {
            if (Success != null)
            {
                var delegates = Success.GetInvocationList();//获取委托链逐个调用，避免失败中断执行链表
                foreach (var del in delegates)
                {
                    try
                    {
                        Action<IUser> act = (Action<IUser>)del;
                        act.Invoke(user);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 获取自定义账号
        /// </summary>
        /// <returns></returns>
        private static string GetCustomeAccount()
        {
            var daUser = DaoFactory.Tnet_User();
            string code = daUser.GenerateCustomCode();

            string account = string.Concat(xUtils.CustomAccountPrefix, code.PadLeft(8, '0'));
            var fac = UserModuleFactory.GetUserModuleInstance();
            IUser user = fac.GetUserByVoucher(account, UserVoucherType.自定义号码);
            if (user != null)
            {
                return GetCustomeAccount();
            }
            return account;
        }
        /// <summary>
        /// 检查UserCode是否已存在
        /// </summary>
        /// <returns></returns>
        private static bool IsUserCodeAlreadyExist(string code, UserVoucherType uvt, IUserFactory fac)
        {
            IUser user = fac.GetUserByVoucher(code, uvt);
            return user != null;
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
    }
}
