using System;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Utils;
using Winner.OAuth.DataAccess.Interfaces;

namespace Winner.OAuth.Facade
{
    public static class DaoFactory
    {
        public static ITauth_Api_Group Tauth_Api_Group()
        {
            return BuildDAO<ITauth_Api_Group>(new Winner.OAuth.DataAccess.MySQL.Tauth_Api_Group(), new Winner.OAuth.DataAccess.Oracle.Tauth_Api_Group());
        }
        public static ITauth_Api_GroupCollection Tauth_Api_GroupCollection()
        {
            return BuildDAO<ITauth_Api_GroupCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_Api_GroupCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_Api_GroupCollection());
        }
        public static ITauth_Api_Info Tauth_Api_Info()
        {
            return BuildDAO<ITauth_Api_Info>(new Winner.OAuth.DataAccess.MySQL.Tauth_Api_Info(), new Winner.OAuth.DataAccess.Oracle.Tauth_Api_Info());
        }
        public static ITauth_Api_InfoCollection Tauth_Api_InfoCollection()
        {
            return BuildDAO<ITauth_Api_InfoCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_Api_InfoCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_Api_InfoCollection());
        }
        public static ITauth_App Tauth_App()
        {
            return BuildDAO<ITauth_App>(new Winner.OAuth.DataAccess.MySQL.Tauth_App(), new Winner.OAuth.DataAccess.Oracle.Tauth_App());
        }
        public static ITauth_AppCollection Tauth_AppCollection()
        {
            return BuildDAO<ITauth_AppCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_AppCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_AppCollection());
        }
        public static ITauth_Code Tauth_Code()
        {
            return BuildDAO<ITauth_Code>(new Winner.OAuth.DataAccess.MySQL.Tauth_Code(), new Winner.OAuth.DataAccess.Oracle.Tauth_Code());
        }
        public static ITauth_CodeCollection Tauth_CodeCollection()
        {
            return BuildDAO<ITauth_CodeCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_CodeCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_CodeCollection());
        }
        public static ITauth_Group_Right Tauth_Group_Right()
        {
            return BuildDAO<ITauth_Group_Right>(new Winner.OAuth.DataAccess.MySQL.Tauth_Group_Right(), new Winner.OAuth.DataAccess.Oracle.Tauth_Group_Right());
        }
        public static ITauth_Group_RightCollection Tauth_Group_RightCollection()
        {
            return BuildDAO<ITauth_Group_RightCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_Group_RightCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_Group_RightCollection());
        }
        public static ITauth_Scope Tauth_Scope()
        {
            return BuildDAO<ITauth_Scope>(new Winner.OAuth.DataAccess.MySQL.Tauth_Scope(), new Winner.OAuth.DataAccess.Oracle.Tauth_Scope());
        }
        public static ITauth_ScopeCollection Tauth_ScopeCollection()
        {
            return BuildDAO<ITauth_ScopeCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_ScopeCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_ScopeCollection());
        }
        public static ITauth_Scope_Apis Tauth_Scope_Apis()
        {
            return BuildDAO<ITauth_Scope_Apis>(new Winner.OAuth.DataAccess.MySQL.Tauth_Scope_Apis(), new Winner.OAuth.DataAccess.Oracle.Tauth_Scope_Apis());
        }
        public static ITauth_Scope_ApisCollection Tauth_Scope_ApisCollection()
        {
            return BuildDAO<ITauth_Scope_ApisCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_Scope_ApisCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_Scope_ApisCollection());
        }
        public static ITauth_Scope_Right Tauth_Scope_Right()
        {
            return BuildDAO<ITauth_Scope_Right>(new Winner.OAuth.DataAccess.MySQL.Tauth_Scope_Right(), new Winner.OAuth.DataAccess.Oracle.Tauth_Scope_Right());
        }
        public static ITauth_Scope_RightCollection Tauth_Scope_RightCollection()
        {
            return BuildDAO<ITauth_Scope_RightCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_Scope_RightCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_Scope_RightCollection());
        }
        public static ITauth_Session Tauth_Session()
        {
            return BuildDAO<ITauth_Session>(new Winner.OAuth.DataAccess.MySQL.Tauth_Session(), new Winner.OAuth.DataAccess.Oracle.Tauth_Session());
        }
        public static ITauth_SessionCollection Tauth_SessionCollection()
        {
            return BuildDAO<ITauth_SessionCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_SessionCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_SessionCollection());
        }
        public static ITauth_Token Tauth_Token()
        {
            return BuildDAO<ITauth_Token>(new Winner.OAuth.DataAccess.MySQL.Tauth_Token(), new Winner.OAuth.DataAccess.Oracle.Tauth_Token());
        }
        public static ITauth_TokenCollection Tauth_TokenCollection()
        {
            return BuildDAO<ITauth_TokenCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_TokenCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_TokenCollection());
        }
        public static ITauth_Token_Right Tauth_Token_Right()
        {
            return BuildDAO<ITauth_Token_Right>(new Winner.OAuth.DataAccess.MySQL.Tauth_Token_Right(), new Winner.OAuth.DataAccess.Oracle.Tauth_Token_Right());
        }
        public static ITauth_Token_RightCollection Tauth_Token_RightCollection()
        {
            return BuildDAO<ITauth_Token_RightCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_Token_RightCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_Token_RightCollection());
        }
        public static ITauth_User_Device Tauth_User_Device()
        {
            return BuildDAO<ITauth_User_Device>(new Winner.OAuth.DataAccess.MySQL.Tauth_User_Device(), new Winner.OAuth.DataAccess.Oracle.Tauth_User_Device());
        }
        public static ITauth_User_DeviceCollection Tauth_User_DeviceCollection()
        {
            return BuildDAO<ITauth_User_DeviceCollection>(new Winner.OAuth.DataAccess.MySQL.Tauth_User_DeviceCollection(), new Winner.OAuth.DataAccess.Oracle.Tauth_User_DeviceCollection());
        }
        public static ITnet_User Tnet_User()
        {
            return BuildDAO<ITnet_User>(new Winner.OAuth.DataAccess.MySQL.Tnet_User(), new Winner.OAuth.DataAccess.Oracle.Tnet_User());
        }
        public static ITnet_UserCollection Tnet_UserCollection()
        {
            return BuildDAO<ITnet_UserCollection>(new Winner.OAuth.DataAccess.MySQL.Tnet_UserCollection(), new Winner.OAuth.DataAccess.Oracle.Tnet_UserCollection());
        }
        public static ITnet_User_Voucher Tnet_User_Voucher()
        {
            return BuildDAO<ITnet_User_Voucher>(new Winner.OAuth.DataAccess.MySQL.Tnet_User_Voucher(), new Winner.OAuth.DataAccess.Oracle.Tnet_User_Voucher());
        }
        public static ITnet_User_VoucherCollection Tnet_User_VoucherCollection()
        {
            return BuildDAO<ITnet_User_VoucherCollection>(new Winner.OAuth.DataAccess.MySQL.Tnet_User_VoucherCollection(), new Winner.OAuth.DataAccess.Oracle.Tnet_User_VoucherCollection());
        }
        public static IVauth_Scope_Right Vauth_Scope_Right()
        {
            return BuildDAO<IVauth_Scope_Right>(new Winner.OAuth.DataAccess.MySQL.Vauth_Scope_Right(), new Winner.OAuth.DataAccess.Oracle.Vauth_Scope_Right());
        }
        public static IVauth_Scope_RightCollection Vauth_Scope_RightCollection()
        {
            return BuildDAO<IVauth_Scope_RightCollection>(new Winner.OAuth.DataAccess.MySQL.Vauth_Scope_RightCollection(), new Winner.OAuth.DataAccess.Oracle.Vauth_Scope_RightCollection());
        }
        public static DatabaseType CurrentDatabaseType
        {
            get
            {
                DatabaseType type = DatabaseType.MySQL;
                int winner_count = 0;
                foreach (var css in ConfigProvider.GetConnectionStrings())
                {
                    switch (css.Key.ToLower())
                    {
                        case "winner.framework.oracle.connectionstring":
                            winner_count++;
                            type = DatabaseType.Oracle;
                            break;
                        case "winner.framework.mysql.connectionstring":
                            type = DatabaseType.MySQL;
                            winner_count++;
                            break;
                            /*case "winner.framework.mssql.connectionstring":
                                type = DatabaseType.MsSQL;
                                winner_count++;
                                break;
                            case "winner.framework.postgre.connectionstring":
                                type = DatabaseType.Postgre;
                                winner_count++;
                                break;
                            case "winner.framework.sqlite.connectionstring":
                                type = DatabaseType.SQLite;
                                winner_count++;
                                break;*/
                    }
                }
                if (winner_count == 0)
                {
                    throw new ApplicationException("请配置数据库连接字符串");
                }
                if (winner_count > 1)
                {
                    throw new ApplicationException("配置冲突，检测到多个数据库连接字符串配置。");
                }
                return type;
            }
        }
        private static T BuildDAO<T>(T t1, T t2)
        {
            T t = t1;
            switch (CurrentDatabaseType)
            {
                case DatabaseType.MySQL:
                    t = t1;
                    break;
                case DatabaseType.Oracle:
                    t = t2;
                    break;
            }
            return t;
        }
    }
    public enum DatabaseType
    {
        MySQL,
        Oracle,
    }
}
