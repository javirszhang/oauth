   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Scope.generate.cs
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
	/// 授权作用域
	/// </summary>
	public partial interface ITauth_Scope : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 作用域ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Scope_Id { get; set; }

		/// <summary>
		/// 作用域代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 20Byte
		/// </para>
		/// </summary>
		string Scope_Code { get; set; }

		/// <summary>
		/// 作用域名字(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 40Byte
		/// </para>
		/// </summary>
		string Scope_Name { get; set; }

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
		/// defaultValue: DBNull.Value;   Length: 280Byte
		/// </para>
		/// </summary>
		string Remarks { get; set; }

		/// <summary>
		/// 是否要求显式授权(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Is_Expllicit { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int scope_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int scope_id);
		#endregion
	}
	/// <summary>
	/// 授权作用域[集合对象]
	/// </summary>
	public partial interface ITauth_ScopeCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}