   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_App.generate.cs
 * CreateTime : 2018-07-27 15:06:56
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
	/// TAUTH_APP
	/// </summary>
	public partial interface ITauth_App : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int App_Id { get; set; }

		/// <summary>
		/// 应用账号(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		string App_Code { get; set; }

		/// <summary>
		/// 应用名称(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 40Byte
		/// </para>
		/// </summary>
		string App_Name { get; set; }

		/// <summary>
		/// 域名(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		string App_Host { get; set; }

		/// <summary>
		/// 安全码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		string Secret_Key { get; set; }

		/// <summary>
		/// OPEN_ID加密密钥(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		string Uid_Encrypt_Key { get; set; }

		/// <summary>
		/// 访问令牌(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		string Access_Token { get; set; }

		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Create_Time { get;  }

		/// <summary>
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 400Byte
		/// </para>
		/// </summary>
		string Remarks { get; set; }

		/// <summary>
		/// 应用状态(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Status { get; set; }

		/// <summary>
		/// 是否内部应用(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Is_Internal { get; set; }

		/// <summary>
		/// logo_url(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		string Logo_Url { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int app_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int app_id);
		bool SelectByAppCode(string appcode);
		#endregion
	}
	/// <summary>
	/// TAUTH_APP[集合对象]
	/// </summary>
	public partial interface ITauth_AppCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}