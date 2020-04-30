/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Tnet_User.generate.cs
* CreateTime : 2018-07-31 16:58:58
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
using Winner.Framework.Utils;

namespace Winner.OAuth.DataAccess.MySQL
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Tnet_User : DataAccessBase,ITnet_User
	{
		#region 构造和基本
		public Tnet_User():base()
		{}
		public Tnet_User(DataRow dataRow):base(dataRow)
		{}
		public const string _USER_ID = "USER_ID";
		public const string _USER_NICKNAME = "USER_NICKNAME";
		public const string _USER_NAME = "USER_NAME";
		public const string _FATHER_ID = "FATHER_ID";
		public const string _USER_STATUS = "USER_STATUS";
		public const string _USER_LEVEL = "USER_LEVEL";
		public const string _AUTH_STATUS = "AUTH_STATUS";
		public const string _AUTH_TIME = "AUTH_TIME";
		public const string _PHOTO_URL = "PHOTO_URL";
		public const string _DATA_SOURCE = "DATA_SOURCE";
		public const string _REMARKS = "REMARKS";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _TableName = "TNET_USER";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TNET_USER");
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_USER_NICKNAME,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_USER_NAME,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_FATHER_ID,typeof(int)).DefaultValue = DBNull.Value;
			table.Columns.Add(_USER_STATUS,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_USER_LEVEL,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_AUTH_STATUS,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_AUTH_TIME,typeof(DateTime)).DefaultValue = DBNull.Value;
			table.Columns.Add(_PHOTO_URL,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_DATA_SOURCE,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 用户编号(必填)
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
		/// 用户昵称(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 50Byte
		/// </para>
		/// </summary>
		public string User_Nickname
		{
			get{ return DataRow[_USER_NICKNAME].ToString();}
			 set{setProperty(_USER_NICKNAME, value);}
		}
		/// <summary>
		/// 用户真实姓名(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string User_Name
		{
			get{ return DataRow[_USER_NAME].ToString();}
			 set{setProperty(_USER_NAME, value);}
		}
		/// <summary>
		/// 推荐人用户编号(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 0Byte
		/// </para>
		/// </summary>
		public int? Father_Id
		{
			get{ return Helper.ToInt32(DataRow[_FATHER_ID]);}
			 set{setProperty(_FATHER_ID, value);}
		}
		/// <summary>
		/// 用户状态$UserStatus$,未激活=0,已激活=1,已注销=2,已封锁=3(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int User_Status
		{
			get{ return Convert.ToInt32(DataRow[_USER_STATUS]);}
			 set{setProperty(_USER_STATUS, value);}
		}
		/// <summary>
		/// 用户级别(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int User_Level
		{
			get{ return Convert.ToInt32(DataRow[_USER_LEVEL]);}
			 set{setProperty(_USER_LEVEL, value);}
		}
		/// <summary>
		/// 实名认证状态$AuthStatus${未实名=0,审核中=1,已认证=2，认证失败=4}(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Auth_Status
		{
			get{ return Convert.ToInt32(DataRow[_AUTH_STATUS]);}
			 set{setProperty(_AUTH_STATUS, value);}
		}
		/// <summary>
		/// 实名验证时间(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime? Auth_Time
		{
			get{ return Helper.ToDateTime(DataRow[_AUTH_TIME]);}
			 set{setProperty(_AUTH_TIME, value);}
		}
		/// <summary>
		/// 用户头像(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 255Byte
		/// </para>
		/// </summary>
		public string Photo_Url
		{
			get{ return DataRow[_PHOTO_URL].ToString();}
			 set{setProperty(_PHOTO_URL, value);}
		}
		/// <summary>
		/// 数据来源(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Data_Source
		{
			get{ return Convert.ToInt32(DataRow[_DATA_SOURCE]);}
			 set{setProperty(_DATA_SOURCE, value);}
		}
		/// <summary>
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 255Byte
		/// </para>
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 注册时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Create_Time
		{
			get{ return Convert.ToDateTime(DataRow[_CREATE_TIME]);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT user_id,user_nickname,user_name,father_id,user_status,user_level,auth_status,auth_time,photo_url,data_source,remarks,create_time FROM tnet_user WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TNET_USER WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int user_id)
		{
			string condition = " USER_ID=?USER_ID";
			AddParameter(_USER_ID,user_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " USER_ID=?USER_ID";
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"insert into tnet_user(user_nickname,user_name,father_id,user_status,user_level,auth_status,auth_time,photo_url,data_source,remarks)
			values (?user_nickname,?user_name,?father_id,?user_status,?user_level,?auth_status,?auth_time,?photo_url,?data_source,?remarks)";
			AddParameter(_USER_NICKNAME,DataRow[_USER_NICKNAME]);
			AddParameter(_USER_NAME,DataRow[_USER_NAME]);
			AddParameter(_FATHER_ID,DataRow[_FATHER_ID]);
			AddParameter(_USER_STATUS,DataRow[_USER_STATUS]);
			AddParameter(_USER_LEVEL,DataRow[_USER_LEVEL]);
			AddParameter(_AUTH_STATUS,DataRow[_AUTH_STATUS]);
			AddParameter(_AUTH_TIME,DataRow[_AUTH_TIME]);
			AddParameter(_PHOTO_URL,DataRow[_PHOTO_URL]);
			AddParameter(_DATA_SOURCE,DataRow[_DATA_SOURCE]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.User_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tnet_UserCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_USER_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TNET_USER SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE user_id=?user_id ");
			AddParameter(_USER_ID, DataRow[_USER_ID]);			
            foreach (Tnet_UserCollection.Field key in conditionDic.Keys)
            {
				if(Tnet_UserCollection.Field.User_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_USER_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TNET_USER SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE user_id=?user_id");
			AddParameter(_USER_ID, DataRow[_USER_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int user_id)
		{
			string condition = " USER_ID=?USER_ID";
			AddParameter(_USER_ID,user_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// [集合对象]
	/// </summary>
	public partial class Tnet_UserCollection : DataAccessCollectionBase,ITnet_UserCollection
	{
		#region 构造和基本
		public Tnet_UserCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tnet_User().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tnet_User(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tnet_User._TableName;}
		}
		public Tnet_User this[int index]
        {
            get { return new Tnet_User(DataTable.Rows[index]); }
        }
		public enum Field
        {
			User_Id=0,
			User_Nickname=1,
			User_Name=2,
			Father_Id=3,
			User_Status=4,
			User_Level=5,
			Auth_Status=6,
			Auth_Time=7,
			Photo_Url=8,
			Data_Source=9,
			Remarks=10,
			Create_Time=11,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT USER_ID,USER_NICKNAME,USER_NAME,FATHER_ID,USER_STATUS,USER_LEVEL,AUTH_STATUS,AUTH_TIME,PHOTO_URL,DATA_SOURCE,REMARKS,CREATE_TIME FROM tnet_user WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY user_id DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tnet_User Find(Predicate<Tnet_User> match)
        {
            foreach (Tnet_User item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tnet_UserCollection FindAll(Predicate<Tnet_User> match)
        {
            Tnet_UserCollection list = new Tnet_UserCollection();
            foreach (Tnet_User item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tnet_User> match)
        {
            foreach (Tnet_User item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tnet_User> match)
        {
            BeginTransaction();
            foreach (Tnet_User item in this)
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