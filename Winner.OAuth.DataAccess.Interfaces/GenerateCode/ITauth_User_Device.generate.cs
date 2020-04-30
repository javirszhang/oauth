   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITauth_User_Device.generate.cs
 * CreateTime : 2019-01-10 11:07:31
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
using Winner.OAuth.Entities;

namespace Winner.OAuth.DataAccess.Interfaces
{
	/// <summary>
	/// 会员设备
	/// </summary>
	public partial interface ITauth_User_Device : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// (必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int Id { get; set; }

		/// <summary>
		/// 主键设备ID(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		string Device_Id { get; set; }

		/// <summary>
		/// 拥有者会员ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 10Byte
		/// </para>
		/// </summary>
		int User_Id { get; set; }

		/// <summary>
		/// 设备型号(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		string Device_Model { get; set; }

		/// <summary>
		/// 是否已授权访问(必填)
		/// <para>
		/// defaultValue: 1;   Length: 10Byte
		/// </para>
		/// </summary>
		int Is_Authorized { get; set; }

		/// <summary>
		/// 首次使用时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Createtime { get;  }

		/// <summary>
		/// 上次修改时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Last_Modify_Time { get; set; }

		/// <summary>
		/// 上次访问系统时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		DateTime Last_Access_Time { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByDeviceId_UserId(string device_id,int user_id);
		
		bool SelectByPK(int id);
		#endregion
	}
	/// <summary>
	/// 会员设备[集合对象]
	/// </summary>
	public partial interface ITauth_User_DeviceCollection : ITransaction, IEnumerable,IPromptInfo
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListAll();
	}
}