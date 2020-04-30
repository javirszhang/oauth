   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Api_Group.generate.cs
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
	/// API分组信息
	/// </summary>
	public partial interface ITauth_Api_Group : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 分组ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Group_Id { get; set; }

		/// <summary>
		/// 分组名称(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 40Byte
		/// </para>
		/// </summary>
		string Group_Name { get; set; }

		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Create_Time { get;  }

		#endregion
		
		#region 基本方法
		
		bool Delete(int group_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int group_id);
		#endregion
	}
	/// <summary>
	/// API分组信息[集合对象]
	/// </summary>
	public partial interface ITauth_Api_GroupCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}