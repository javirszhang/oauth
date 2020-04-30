   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Group_Right.generate.cs
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
	/// API分组权限
	/// </summary>
	public partial interface ITauth_Group_Right : ITransaction,IPromptInfo
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
		/// 分组ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Group_Id { get; set; }

		/// <summary>
		/// 接口ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Api_Id { get; set; }

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
	/// API分组权限[集合对象]
	/// </summary>
	public partial interface ITauth_Group_RightCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListByGroup_Id(int group_id);
		bool ListAll();
	}
}