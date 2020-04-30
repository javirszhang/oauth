   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : ITnet_User_Voucher.generate.cs
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
	/// 
	/// </summary>
	public partial interface ITnet_User_Voucher : ITransaction,IPromptInfo
	{		
		#region 属性		
		DataRow DataRow { get; }
		ExecCommand DebugSQL { get; }
		/// <summary>
		/// 会员凭证ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Voucher_Id { get; set; }

		/// <summary>
		/// 会员ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int User_Id { get; set; }

		/// <summary>
		/// 会员账号(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		string User_Code { get; set; }

		/// <summary>
		/// 账号类型$UserVoucherType$[1：手机号，2：邮箱，3：QQ_openid，4：wechat_openid，5：weibo_openid](必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		int Voucher_Type { get; set; }

		/// <summary>
		/// 是否允许作为账号登录[0：不允许，1：允许](必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		int Allow_Login { get; set; }

		/// <summary>
		/// 是否经过验证，比如手机号短信验证，邮箱验证，第三方登录授权验证等等(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		int Is_Valid { get; set; }

		/// <summary>
		/// 绑定时间(必填)
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
		/// 凭据状态[1：正常，2：注销](必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		int Status { get; set; }

		#endregion
		
		#region 基本方法
		
		bool Delete(int voucher_id);
		
		bool Delete();
				
		bool Insert();
		
		bool Update();
		
	
		bool SelectByUserCode_VoucherType(string user_code,int voucher_type);
		
		bool SelectByPk(int voucher_id);
		#endregion
	}
	/// <summary>
	/// [集合对象]
	/// </summary>
	public partial interface ITnet_User_VoucherCollection : ITransaction, IEnumerable
	{
		DataTable DataTable { get; }
		int Count { get; }
		IChangePage ChangePage { get; set; }
		bool ListByUser_Id(int user_id);
		bool ListAll();
	}
}