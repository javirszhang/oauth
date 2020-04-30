   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : IVauth_Scope_Right.generate.cs
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
using Winner.Framework.Utils;

namespace Winner.OAuth.DataAccess.Interfaces
{
	/// <summary>
	/// 授权作用域权限信息视图
	/// </summary>
	public partial interface IVauth_Scope_Right : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 授权作用域ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		int? Scope_Id { get; set; }

		/// <summary>
		/// api编号ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		int? Api_Id { get; set; }

		/// <summary>
		/// 接口名称(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 40Byte
		/// </para>
		/// </summary>
		string Api_Name { get; set; }

		/// <summary>
		/// 接口地址(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		string Api_Url { get; set; }

		/// <summary>
		/// 创建时间(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime? Create_Time { get;  }

		/// <summary>
		/// 状态(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		int? Status { get; set; }

		/// <summary>
		/// 接口属于项目ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		int? App_Type { get; set; }

		#endregion
		
		#region 基本方法
		
		#endregion
	}
	/// <summary>
	/// 授权作用域权限信息视图[集合对象]
	/// </summary>
	public partial interface IVauth_Scope_RightCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}