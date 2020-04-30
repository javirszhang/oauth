   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tnet_User_Voucher.generate.cs
 * CreateTime : 2018-07-27 15:07:38
 * CodeGenerateVersion : 1.0.0.0
 * TemplateVersion: 2.0.0
 * E_Mail : zhj.pavel@gmail.com
 * Blog : 
 * Copyright (C) YXH
 * 
 ***************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winner.Framework.Core.DataAccess.MySql;
using Winner.OAuth.Entities;
using Winner.OAuth.DataAccess.Interfaces;

namespace Winner.OAuth.DataAccess.MySQL
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Tnet_User_Voucher : DataAccessBase, ITnet_User_Voucher
	{
		#region 构造和基本
		public Tnet_User_Voucher():base()
		{}
		public Tnet_User_Voucher(DataRow dataRow):base(dataRow)
		{}
		public const string _VOUCHER_ID = "VOUCHER_ID";
		public const string _USER_ID = "USER_ID";
		public const string _USER_CODE = "USER_CODE";
		public const string _VOUCHER_TYPE = "VOUCHER_TYPE";
		public const string _ALLOW_LOGIN = "ALLOW_LOGIN";
		public const string _IS_VALID = "IS_VALID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _STATUS = "STATUS";
		public const string _TableName = "TNET_USER_VOUCHER";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TNET_USER_VOUCHER");
			table.Columns.Add(_VOUCHER_ID,typeof(int)).DefaultValue = DBNull.Value;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_USER_CODE,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_VOUCHER_TYPE,typeof(int)).DefaultValue = 1;
			table.Columns.Add(_ALLOW_LOGIN,typeof(int)).DefaultValue = 1;
			table.Columns.Add(_IS_VALID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue = 1;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 会员凭证ID(必填)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Voucher_Id
		{
			get{ return Convert.ToInt32(DataRow[_VOUCHER_ID]);}
			 set{setProperty(_VOUCHER_ID, value);}
		}
		/// <summary>
		/// 会员ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int User_Id
		{
			get{ return Convert.ToInt32(DataRow[_USER_ID]);}
			 set{setProperty(_USER_ID, value);}
		}
		/// <summary>
		/// 会员账号(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		public string User_Code
		{
			get{ return DataRow[_USER_CODE].ToString();}
			 set{setProperty(_USER_CODE, value);}
		}
		/// <summary>
		/// 账号类型$UserVoucherType$[1：手机号，2：邮箱，3：QQ_openid，4：wechat_openid，5：weibo_openid](必填)
		/// <para>
		/// defaultValue: 1;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Voucher_Type
		{
			get{ return Convert.ToInt32(DataRow[_VOUCHER_TYPE]);}
			 set{setProperty(_VOUCHER_TYPE, value);}
		}
		/// <summary>
		/// 是否允许作为账号登录[0：不允许，1：允许](必填)
		/// <para>
		/// defaultValue: 1;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Allow_Login
		{
			get{ return Convert.ToInt32(DataRow[_ALLOW_LOGIN]);}
			 set{setProperty(_ALLOW_LOGIN, value);}
		}
		/// <summary>
		/// 是否经过验证，比如手机号短信验证，邮箱验证，第三方登录授权验证等等(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Is_Valid
		{
			get{ return Convert.ToInt32(DataRow[_IS_VALID]);}
			 set{setProperty(_IS_VALID, value);}
		}
		/// <summary>
		/// 绑定时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Create_Time
		{
			get{ return Convert.ToDateTime(DataRow[_CREATE_TIME]);}
		}
		/// <summary>
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 凭据状态[1：正常，2：注销](必填)
		/// <para>
		/// defaultValue: 1;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT voucher_id,user_id,user_code,voucher_type,allow_login,is_valid,create_time,remarks,status FROM tnet_user_voucher WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TNET_USER_VOUCHER WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int voucher_id)
		{
			string condition = " VOUCHER_ID=?VOUCHER_ID";
			AddParameter(_VOUCHER_ID,voucher_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " VOUCHER_ID=?VOUCHER_ID";
			AddParameter(_VOUCHER_ID,DataRow[_VOUCHER_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"INSERT INTO tnet_user_voucher(user_id,user_code,voucher_type,allow_login,is_valid,remarks,status)
			VALUES (?user_id,?user_code,?voucher_type,?allow_login,?is_valid,?remarks,?status)";
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_USER_CODE,DataRow[_USER_CODE]);
			AddParameter(_VOUCHER_TYPE,DataRow[_VOUCHER_TYPE]);
			AddParameter(_ALLOW_LOGIN,DataRow[_ALLOW_LOGIN]);
			AddParameter(_IS_VALID,DataRow[_IS_VALID]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Voucher_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tnet_User_VoucherCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_VOUCHER_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TNET_USER_VOUCHER SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE voucher_id=?voucher_id ");
			AddParameter(_VOUCHER_ID, DataRow[_VOUCHER_ID]);			
            foreach (Tnet_User_VoucherCollection.Field key in conditionDic.Keys)
            {
				if(Tnet_User_VoucherCollection.Field.Voucher_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_VOUCHER_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TNET_USER_VOUCHER SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE voucher_id=?voucher_id");
			AddParameter(_VOUCHER_ID, DataRow[_VOUCHER_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByUserCode_VoucherType(string user_code,int voucher_type)
		{
			string condition = null;
			condition += "USER_CODE=?USER_CODE";
			AddParameter(_USER_CODE,user_code);
			condition += " AND VOUCHER_TYPE=?VOUCHER_TYPE";
			AddParameter(_VOUCHER_TYPE,voucher_type);

			return SelectByCondition(condition);
		}
		public bool SelectByPk(int voucher_id)
		{
			string condition = " VOUCHER_ID=?VOUCHER_ID";
			AddParameter(_VOUCHER_ID,voucher_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// [集合对象]
	/// </summary>
	public partial class Tnet_User_VoucherCollection : DataAccessCollectionBase, ITnet_User_VoucherCollection
	{
		#region 构造和基本
		public Tnet_User_VoucherCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tnet_User_Voucher().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tnet_User_Voucher(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tnet_User_Voucher._TableName;}
		}
		public Tnet_User_Voucher this[int index]
        {
            get { return new Tnet_User_Voucher(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Voucher_Id=0,
			User_Id=1,
			User_Code=2,
			Voucher_Type=3,
			Allow_Login=4,
			Is_Valid=5,
			Create_Time=6,
			Remarks=7,
			Status=8,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT VOUCHER_ID,USER_ID,USER_CODE,VOUCHER_TYPE,ALLOW_LOGIN,IS_VALID,CREATE_TIME,REMARKS,STATUS FROM tnet_user_voucher WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListByUser_Id(int user_id)
		{
			string condition = "user_id=?user_id ORDER BY voucher_id DESC";
			AddParameter(Tnet_User_Voucher._USER_ID,user_id);
			return ListByCondition(condition);		
		}
		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY voucher_id DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tnet_User_Voucher Find(Predicate<Tnet_User_Voucher> match)
        {
            foreach (Tnet_User_Voucher item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tnet_User_VoucherCollection FindAll(Predicate<Tnet_User_Voucher> match)
        {
            Tnet_User_VoucherCollection list = new Tnet_User_VoucherCollection();
            foreach (Tnet_User_Voucher item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tnet_User_Voucher> match)
        {
            foreach (Tnet_User_Voucher item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tnet_User_Voucher> match)
        {
            BeginTransaction();
            foreach (Tnet_User_Voucher item in this)
            {
                item.ReferenceTransactionFrom(Transaction);
                if (!match(item))
                    continue;
                if (!item.Delete())
                {
                    Rollback();
                    return false;
                }
            }
            Commit();
            return true;
        }
		#endregion
		#endregion		
	}
}