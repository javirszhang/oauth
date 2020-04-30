   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Scope_Right.generate.cs
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
	/// 授权作用域的权限
	/// </summary>
	public partial interface ITauth_Scope_Right : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 主键(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Right_Id { get; set; }

		/// <summary>
		/// 作用域ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Scope_Id { get; set; }

		/// <summary>
		/// 关联ID（REF_TYPE=1关联GROUP，REF_TYPE=2关联API）(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Ref_Id { get; set; }

		/// <summary>
		/// 关联类型[0：关联TAUTH_API_GROUP，1：关联TAUTH_API_INFO](必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Ref_Type { get; set; }

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

		#endregion
		
		#region 基本方法
		
		bool Delete(int right_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int right_id);
		#endregion
	}
	/// <summary>
	/// 授权作用域的权限[集合对象]
	/// </summary>
	public partial interface ITauth_Scope_RightCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}