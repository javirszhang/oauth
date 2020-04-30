using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Utils;
using Winner.User.Interface;

namespace Winner.OAuth.Facade
{
    /// <summary>
    /// 邀请码提供者
    /// </summary>
    public class InvitationCodeProvider
    {
        private string _refereeCode;
        private IUser _associateUser;
        public InvitationCodeProvider(string refereeCode)
        {
            this._refereeCode = refereeCode;
        }
        /// <summary>
        /// 获取关联的会员信息
        /// </summary>
        /// <returns></returns>
        public IUser GetAssociateUser()
        {
            if (_associateUser != null)
            {
                return _associateUser;
            }
            if (string.IsNullOrEmpty(_refereeCode))
            {
                return null;
            }
            var fac = UserModuleFactory.GetUserModuleInstance();
            if (fac == null)
            {
                Log.Info("缺少用户模块");
                return null;
            }
            if (_refereeCode.IsMobileNo())//手机号账号
            {
                _associateUser = fac.GetUserByMobileno(_refereeCode);
            }
            else if (_refereeCode.StartsWith("U"))//ID邀请码
            {
                int userid;
                if (!int.TryParse(_refereeCode.Replace("U", ""), out userid))
                {
                    return null;
                }
                _associateUser = fac.GetUserByID(userid);
            }
            else if (_refereeCode.StartsWith(xUtils.CustomAccountPrefix))//自定义账号
            {
                _associateUser = fac.GetUserByCode(_refereeCode);
            }
            else
            {
                Log.Info("未识别的邀请码");
            }
            return _associateUser;
        }

        /// <summary>
        /// 关联的会员信息
        /// </summary>
        public IUser AssociateUser { get { return GetAssociateUser(); } }
    }
}
