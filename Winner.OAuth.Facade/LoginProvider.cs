using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    public class LoginProvider : FacadeBase
    {
        private string _user_code;
        private string _password;
        private LoginType loginType;
        private string _scope;
        public LoginProvider(string user_code, string password, string scope, LoginType login_type = LoginType.LOGIN_BY_PASSWORD)
        {
            this._user_code = user_code;
            this._password = password;
            this.loginType = login_type;
            this._scope = scope;
        }
        /// <summary>
        /// 忽略密码检查
        /// </summary>
        public bool IgnorePassword { get; set; }
        public bool Login(int client_source, string client_system, string device_id, string ip_address, string session_id, string clientVersion, int appid)
        {
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                Alert(ResultType.系统异常, "加载用户模块失败");
                return false;
            }
            try
            {
                UserVoucherType uvt = xUtils.GetVoucherType(this._user_code);
                this.User = fac.GetUserByVoucher(this._user_code, uvt);
            }
            catch (ApplicationException ex)
            {
                Alert(ResultType.非法操作, "无效的登录账号");
                Log.Error($"无效的登录账号[{this._user_code}]", ex);
                return false;
            }
            catch (Exception exp)
            {
                Log.Error("登录异常", exp);
                Alert(ResultType.系统异常, "系统异常");
                return false;
            }
            if (this.User == null)
            {
                Alert(ResultType.无效数据类型, "用户未注册");
                return false;
            }
            if (this.User.Status != UserStatus.已激活)
            {
                Alert(ResultType.非法操作, $"账户{this.User.Status.ToString()}");
                return false;
            }
            var lockResult = this.User.IsLocked(Winner.User.Interface.Lock.LockRight.登录);
            if (lockResult.IsLocked)
            {
                Alert(ResultType.非法操作, lockResult.Reason);
                return false;
            }
            if (loginType == LoginType.LOGIN_BY_PASSWORD || loginType == LoginType.密码登录)
            {
                if (!IgnorePassword && !this.User.CheckLoginPassword(_password))
                {
                    Alert(ResultType.非法操作, this.User.ErrorInfo.Message);
                    return false;
                }
            }
            else if (loginType == LoginType.短信验证码登录)
            {
                SmsValidateProvider smsValid = new SmsValidateProvider(this.User.MobileNo, SmsValidateType.登录验证码);
                if (!smsValid.ValidateCode(_password))
                {
                    Alert(ResultType.非法操作, smsValid.PromptInfo);
                    return false;
                }
            }
            else
            {
                Alert(ResultType.无效数据类型, "无效的登录方式");
                return false;
            }
            SaveUserDevice(this.User.UserId, device_id, client_system, client_source);
            //this.Token = xUtils.EncryptAccessToken(this.User.UserId, this.User.UserCode, appid);

            UserTokenProvider utp = new UserTokenProvider(appid, this.User.UserId, null, device_id, this._scope);
            if (!utp.GenerateUserToken())
            {
                Alert(utp.PromptInfo);
                return false;
            }
            this.OAuthUser = utp.OAuthUser;
            this.Token = utp.OAuthUser.Token;


            //Tauth_Session daSession = new Tauth_Session
            var daSession = DaoFactory.Tauth_Session();
            daSession.Client_Source = client_source;
            daSession.Client_System = client_system;
            daSession.Device_Id = device_id;
            daSession.Ip_Address = ip_address;
            daSession.Session_Id = session_id;
            daSession.Status = 1;
            daSession.User_Id = this.User.UserId;
            daSession.Token = this.Token;
            daSession.Client_Version = clientVersion;

            if (!daSession.Insert())
            {
                Alert(ResultType.系统异常, "保存登录会话失败");
                return false;
            }
            Logined();
            return true;
        }
        public IUser User { get; set; }
        public event Action<IUser> OnLogined;
        public string Token { get; private set; }
        public UserOpenModel OAuthUser { get; private set; }
        private void Logined()
        {
            if (OnLogined != null)
            {
                var delegates = OnLogined.GetInvocationList();
                foreach (Delegate d in delegates)
                {
                    Action<IUser> act = d as Action<IUser>;
                    act?.Invoke(this.User);
                }
            }
        }

        private static void SaveUserDevice(int userid, string deviceId, string system, int deviceType)
        {
            var dao = DaoFactory.Tauth_User_Device();
            if (dao.SelectByDeviceId_UserId(deviceId, userid))
            {
                return;
            }
            dao.Device_Id = deviceId;
            dao.User_Id = userid;
            dao.Device_Model = system;
            dao.Is_Authorized = 1;
            //dao.DeviceType = deviceType;
            dao.Insert();
        }
    }
}
