   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITnet_User.generate.cs
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
	/// 
	/// </summary>
	public partial interface ITnet_User : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 用户编号(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int User_Id { get; set; }

		/// <summary>
		/// 用户昵称(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		string User_Nickname { get; set; }

		/// <summary>
		/// 用户姓名(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		string User_Name { get; set; }

		/// <summary>
		/// 推荐人用户编号(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		int? Father_Id { get; set; }

		/// <summary>
		/// 用户状态$UserStatus$,未激活=0,已激活=1,已注销=2,已封锁=3(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int User_Status { get; set; }

		/// <summary>
		/// 用户级别(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int User_Level { get; set; }

		/// <summary>
		/// 实名认证状态$AuthStatus${未实名=0,审核中=1,已认证=2，认证失败=4}(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Auth_Status { get; set; }

		/// <summary>
		/// 实名验证时间(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime? Auth_Time { get; set; }

		/// <summary>
		/// 用户头像(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		string Photo_Url { get; set; }

		/// <summary>
		/// 数据来源(必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		int Data_Source { get; set; }

		/// <summary>
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 400Byte
		/// </para>
		/// </summary>
		string Remarks { get; set; }

		/// <summary>
		/// 录入时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Create_Time { get;  }

		#endregion
		
		#region 基本方法
		
		bool Delete(int user_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int user_id);
		#endregion
	}
	/// <summary>
	/// [集合对象]
	/// </summary>
	public partial interface ITnet_UserCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}