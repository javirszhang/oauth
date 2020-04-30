   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_Session.generate.cs
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
	/// 当前用户登录会话
	/// </summary>
	public partial interface ITauth_Session : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 客户端版本号(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 20Byte
		/// </para>
		/// </summary>
		string Client_Version { get; set; }

		/// <summary>
		/// 会话ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Login_Id { get; set; }

		/// <summary>
		/// 用户ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int User_Id { get; set; }

		/// <summary>
		/// 会员代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		string Session_Id { get; set; }

		/// <summary>
		/// 登录令牌(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		string Token { get; set; }

		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Create_Time { get;  }

		/// <summary>
		/// 设备ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		string Device_Id { get; set; }

		/// <summary>
		/// 登录ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 20Byte
		/// </para>
		/// </summary>
		string Ip_Address { get; set; }

		/// <summary>
		/// 客户端系统(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		string Client_System { get; set; }

		/// <summary>
		/// 登录来源(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Client_Source { get; set; }

		/// <summary>
		/// 登录状态[1：活跃中，2：正常退出，3：超时退出](必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		int Status { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int login_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByPk(int login_id);
		#endregion
	}
	/// <summary>
	/// 当前用户登录会话[集合对象]
	/// </summary>
	public partial interface ITauth_SessionCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}