   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Code.generate.cs
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
	/// 授权码
	/// </summary>
	public partial interface ITauth_Code : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 授权权限Json字符串(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 4000Byte
		/// </para>
		/// </summary>
		string Right_Json { get; set; }

		/// <summary>
		/// 鉴权ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Auth_Id { get; set; }

		/// <summary>
		/// 用户ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int User_Id { get; set; }

		/// <summary>
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int App_Id { get; set; }

		/// <summary>
		/// 授权码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		string Grant_Code { get; set; }

		/// <summary>
		/// 授权码作用域(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
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
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 400Byte
		/// </para>
		/// </summary>
		string Remarks { get; set; }

		/// <summary>
		/// 授权码过期时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Expire_Time { get; set; }

		/// <summary>
		/// 授权码状态[0：未使用，1：已使用](必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Status { get; set; }

		/// <summary>
		/// 登录授权的设备ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		string Device_Id { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int auth_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByAppId_GrantCode(int app_id,string grant_code);
		
		bool SelectByPk(int auth_id);
		#endregion
	}
	/// <summary>
	/// 授权码[集合对象]
	/// </summary>
	public partial interface ITauth_CodeCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}