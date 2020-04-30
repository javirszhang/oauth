   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Token.generate.cs
 * CreateTime : 2019-01-10 11:18:44
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
	/// 用户授权令牌
	/// </summary>
	public partial class Tauth_Token : DataAccessBase,ITauth_Token
	{
		#region 构造和基本
		public Tauth_Token():base()
		{}
		public Tauth_Token(DataRow dataRow):base(dataRow)
		{}
		public const string _TOKEN_ID = "TOKEN_ID";
		public const string _APP_ID = "APP_ID";
		public const string _USER_ID = "USER_ID";
		public const string _TOKEN_CODE = "TOKEN_CODE";
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _EXPIRE_TIME = "EXPIRE_TIME";
		public const string _REFRESH_TOKEN = "REFRESH_TOKEN";
		public const string _REFRESH_TIMEOUT = "REFRESH_TIMEOUT";
		public const string _GRANT_ID = "GRANT_ID";
		public const string _DEVICE_ID = "DEVICE_ID";
		public const string _LAST_ACCESS_TIME = "LAST_ACCESS_TIME";
		public const string _TableName = "TAUTH_TOKEN";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_TOKEN");
			table.Columns.Add(_TOKEN_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_APP_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_TOKEN_CODE,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_EXPIRE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_REFRESH_TOKEN,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_REFRESH_TIMEOUT,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_GRANT_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_DEVICE_ID,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_LAST_ACCESS_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 令牌ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Token_Id
		{
			get{ return  Convert.ToInt32(DataRow[_TOKEN_ID]);}
			 set{setProperty(_TOKEN_ID, value);}
		}
		/// <summary>
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int App_Id
		{
			get{ return  Convert.ToInt32(DataRow[_APP_ID]);}
			 set{setProperty(_APP_ID, value);}
		}
		/// <summary>
		/// 用户ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int User_Id
		{
			get{ return  Convert.ToInt32(DataRow[_USER_ID]);}
			 set{setProperty(_USER_ID, value);}
		}
		/// <summary>
		/// 令牌代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Token_Code
		{
			get{ return DataRow[_TOKEN_CODE].ToString();}
			 set{setProperty(_TOKEN_CODE, value);}
		}
		/// <summary>
		/// 作用域ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Scope_Id
		{
			get{ return  Convert.ToInt32(DataRow[_SCOPE_ID]);}
			 set{setProperty(_SCOPE_ID, value);}
		}
		/// <summary>
		/// 创建时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Create_Time
		{
			get{ return  Convert.ToDateTime(DataRow[_CREATE_TIME]);}
		}
		/// <summary>
		/// 过期时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Expire_Time
		{
			get{ return  Convert.ToDateTime(DataRow[_EXPIRE_TIME]);}
			 set{setProperty(_EXPIRE_TIME, value);}
		}
		/// <summary>
		/// 刷新令牌的凭证(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Refresh_Token
		{
			get{ return DataRow[_REFRESH_TOKEN].ToString();}
			 set{setProperty(_REFRESH_TOKEN, value);}
		}
		/// <summary>
		/// 刷新令牌过期时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Refresh_Timeout
		{
			get{ return  Convert.ToDateTime(DataRow[_REFRESH_TIMEOUT]);}
			 set{setProperty(_REFRESH_TIMEOUT, value);}
		}
		/// <summary>
		/// 授权ID（TAUTH_CODE.AUTH_ID）(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Grant_Id
		{
			get{ return  Convert.ToInt32(DataRow[_GRANT_ID]);}
			 set{setProperty(_GRANT_ID, value);}
		}
		/// <summary>
		/// 设备号(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Device_Id
		{
			get{ return DataRow[_DEVICE_ID].ToString();}
			 set{setProperty(_DEVICE_ID, value);}
		}
		/// <summary>
		/// (必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Last_Access_Time
		{
			get{ return  Convert.ToDateTime(DataRow[_LAST_ACCESS_TIME]);}
			 set{setProperty(_LAST_ACCESS_TIME, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT TOKEN_ID,APP_ID,USER_ID,TOKEN_CODE,SCOPE_ID,CREATE_TIME,EXPIRE_TIME,REFRESH_TOKEN,REFRESH_TIMEOUT,GRANT_ID,device_id,last_access_time FROM tauth_token WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_TOKEN WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int token_id)
		{
			string condition = " TOKEN_ID=?TOKEN_ID";
			AddParameter(_TOKEN_ID,token_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " TOKEN_ID=?TOKEN_ID";
			AddParameter(_TOKEN_ID,DataRow[_TOKEN_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"insert into tauth_token(TOKEN_IDAPP_ID,USER_ID,TOKEN_CODE,SCOPE_ID,EXPIRE_TIME,REFRESH_TOKEN,REFRESH_TIMEOUT,GRANT_ID,device_id,last_access_time)
			values (?TOKEN_ID?APP_ID,?USER_ID,?TOKEN_CODE,?SCOPE_ID,?EXPIRE_TIME,?REFRESH_TOKEN,?REFRESH_TIMEOUT,?GRANT_ID,?device_id,?last_access_time)";
			AddParameter(_TOKEN_ID,DataRow[_TOKEN_ID]);
			AddParameter(_APP_ID,DataRow[_APP_ID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_TOKEN_CODE,DataRow[_TOKEN_CODE]);
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			AddParameter(_EXPIRE_TIME,DataRow[_EXPIRE_TIME]);
			AddParameter(_REFRESH_TOKEN,DataRow[_REFRESH_TOKEN]);
			AddParameter(_REFRESH_TIMEOUT,DataRow[_REFRESH_TIMEOUT]);
			AddParameter(_GRANT_ID,DataRow[_GRANT_ID]);
			AddParameter(_DEVICE_ID,DataRow[_DEVICE_ID]);
			AddParameter(_LAST_ACCESS_TIME,DataRow[_LAST_ACCESS_TIME]);
			bool result = InsertBySql(sql);
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_TokenCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_TOKEN_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_TOKEN SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE TOKEN_ID=?TOKEN_ID ");
			AddParameter(_TOKEN_ID, DataRow[_TOKEN_ID]);			
            foreach (Tauth_TokenCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_TokenCollection.Field.Token_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_TOKEN_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_TOKEN SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE TOKEN_ID=?TOKEN_ID");
			AddParameter(_TOKEN_ID, DataRow[_TOKEN_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByAppId_UserId_DeviceId(int app_id,int user_id,string device_id)
		{
			string condition = null;
			condition += "APP_ID=?APP_ID";
			AddParameter(_APP_ID,app_id);
			condition += " AND USER_ID=?USER_ID";
			AddParameter(_USER_ID,user_id);
			condition += " AND DEVICE_ID=?DEVICE_ID";
			AddParameter(_DEVICE_ID,device_id);

			return SelectByCondition(condition);
		}
		public bool SelectByPK(int token_id)
		{
			string condition = " TOKEN_ID=?TOKEN_ID";
			AddParameter(_TOKEN_ID,token_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 用户授权令牌[集合对象]
	/// </summary>
	public partial class Tauth_TokenCollection : DataAccessCollectionBase,ITauth_TokenCollection
	{
		#region 构造和基本
		public Tauth_TokenCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Token().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Token(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Token._TableName;}
		}
		public Tauth_Token this[int index]
        {
            get { return new Tauth_Token(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Token_Id=0,
			App_Id=1,
			User_Id=2,
			Token_Code=3,
			Scope_Id=4,
			Create_Time=5,
			Expire_Time=6,
			Refresh_Token=7,
			Refresh_Timeout=8,
			Grant_Id=9,
			Device_Id=10,
			Last_Access_Time=11,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT TOKEN_ID,APP_ID,USER_ID,TOKEN_CODE,SCOPE_ID,CREATE_TIME,EXPIRE_TIME,REFRESH_TOKEN,REFRESH_TIMEOUT,GRANT_ID,DEVICE_ID,LAST_ACCESS_TIME FROM tauth_token WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY TOKEN_ID DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Token Find(Predicate<Tauth_Token> match)
        {
            foreach (Tauth_Token item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_TokenCollection FindAll(Predicate<Tauth_Token> match)
        {
            Tauth_TokenCollection list = new Tauth_TokenCollection();
            foreach (Tauth_Token item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Token> match)
        {
            foreach (Tauth_Token item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Token> match)
        {
            BeginTransaction();
            foreach (Tauth_Token item in this)
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