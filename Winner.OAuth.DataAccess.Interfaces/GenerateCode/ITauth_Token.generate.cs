   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Token.generate.cs
 * CreateTime : 2019-01-10 11:07:31
 * CodeGenerateVersion : 1.0.0.0
 * TemplateVersion: 2.0.0
 * E_Mail : zhj.pavel@gmail.com
 * Blog : 
 * Copyright (C) YXH
 * 
 ***************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winner.Framework.Core.Interface;
using Winner.Framework.Core.DataAccess;

namespace Winner.OAuth.DataAccess.Interfaces
{
	/// <summary>
	/// 用户授权令牌
	/// </summary>
	public partial interface ITauth_Token : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 令牌ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int Token_Id { get; set; }

		/// <summary>
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int App_Id { get; set; }

		/// <summary>
		/// 用户ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int User_Id { get; set; }

		/// <summary>
		/// 令牌代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		string Token_Code { get; set; }

		/// <summary>
		/// 作用域ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int Scope_Id { get; set; }

		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Create_Time { get;  }

		/// <summary>
		/// 过期时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Expire_Time { get; set; }

		/// <summary>
		/// 刷新令牌的凭证(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		string Refresh_Token { get; set; }

		/// <summary>
		/// 刷新令牌过期时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Refresh_Timeout { get; set; }

		/// <summary>
		/// 授权ID（TAUTH_CODE.AUTH_ID）(必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int Grant_Id { get; set; }

		/// <summary>
		/// 设备号(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 50Byte
		/// </para>
		/// </summary>
		string Device_Id { get; set; }

		/// <summary>
		/// (必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Last_Access_Time { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int token_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByAppId_UserId_DeviceId(int app_id,int user_id,string device_id);
		
		bool SelectByPK(int token_id);
		#endregion
	}
	/// <summary>
	/// 用户授权令牌[集合对象]
	/// </summary>
	public partial interface ITauth_TokenCollection : ITransaction, IEnumerable,IPromptInfo
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}