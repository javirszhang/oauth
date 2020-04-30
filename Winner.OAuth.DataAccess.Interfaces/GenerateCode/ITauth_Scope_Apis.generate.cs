   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Scope_Apis.generate.cs
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
	/// 授权作用域允许调用的API
	/// </summary>
	public partial interface ITauth_Scope_Apis : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 接口ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Api_Id { get; set; }

		/// <summary>
		/// 作用域ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Scope_Id { get; set; }

		/// <summary>
		/// api地址(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		string Api_Url { get; set; }

		/// <summary>
		/// api名称（api提供的功能描述）(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		string Api_Name { get; set; }

		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Create_Time { get;  }

		/// <summary>
		/// api状态(必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		int Status { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int api_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int api_id);
		#endregion
	}
	/// <summary>
	/// 授权作用域允许调用的API[集合对象]
	/// </summary>
	public partial interface ITauth_Scope_ApisCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListByScope_Id(int scope_id);
		bool ListAll();
	}
}