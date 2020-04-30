   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Code.generate.cs
 * CreateTime : 2018-07-27 15:04:16
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
	/// 授权码
	/// </summary>
	public partial class Tauth_Code : DataAccessBase, ITauth_Code
	{
		#region 构造和基本
		public Tauth_Code():base()
		{}
		public Tauth_Code(DataRow dataRow):base(dataRow)
		{}
		public const string _AUTH_ID = "AUTH_ID";
		public const string _USER_ID = "USER_ID";
		public const string _APP_ID = "APP_ID";
		public const string _GRANT_CODE = "GRANT_CODE";
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _EXPIRE_TIME = "EXPIRE_TIME";
		public const string _STATUS = "STATUS";
		public const string _DEVICE_ID = "DEVICE_ID";
		public const string _RIGHT_JSON = "RIGHT_JSON";
		public const string _TableName = "TAUTH_CODE";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_CODE");
			table.Columns.Add(_AUTH_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_APP_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_GRANT_CODE,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_EXPIRE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_DEVICE_ID,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_RIGHT_JSON,typeof(string)).DefaultValue = DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 鉴权ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Auth_Id
		{
			get{ return Convert.ToInt32(DataRow[_AUTH_ID]);}
			 set{setProperty(_AUTH_ID, value);}
		}
		/// <summary>
		/// 用户ID(必填)
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
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int App_Id
		{
			get{ return Convert.ToInt32(DataRow[_APP_ID]);}
			 set{setProperty(_APP_ID, value);}
		}
		/// <summary>
		/// 授权码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Grant_Code
		{
			get{ return DataRow[_GRANT_CODE].ToString();}
			 set{setProperty(_GRANT_CODE, value);}
		}
		/// <summary>
		/// 授权码作用域(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Scope_Id
		{
			get{ return Convert.ToInt32(DataRow[_SCOPE_ID]);}
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
		/// 授权码过期时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Expire_Time
		{
			get{ return Convert.ToDateTime(DataRow[_EXPIRE_TIME]);}
			 set{setProperty(_EXPIRE_TIME, value);}
		}
		/// <summary>
		/// 授权码状态[0：未使用，1：已使用](必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 登录授权的设备ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Device_Id
		{
			get{ return DataRow[_DEVICE_ID].ToString();}
			 set{setProperty(_DEVICE_ID, value);}
		}
		/// <summary>
		/// 权限信息Json字符串(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 4000Byte
		/// </para>
		/// </summary>
		public string Right_Json
		{
			get{ return DataRow[_RIGHT_JSON].ToString();}
			 set{setProperty(_RIGHT_JSON, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT AUTH_ID,USER_ID,APP_ID,GRANT_CODE,SCOPE_ID,CREATE_TIME,REMARKS,EXPIRE_TIME,STATUS,DEVICE_ID,RIGHT_JSON FROM tauth_code WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_CODE WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int auth_id)
		{
			string condition = " AUTH_ID=?AUTH_ID";
			AddParameter(_AUTH_ID,auth_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " AUTH_ID=?AUTH_ID";
			AddParameter(_AUTH_ID,DataRow[_AUTH_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"INSERT INTO tauth_code(USER_ID,APP_ID,GRANT_CODE,SCOPE_ID,REMARKS,EXPIRE_TIME,STATUS,DEVICE_ID,RIGHT_JSON)
			VALUES (?USER_ID,?APP_ID,?GRANT_CODE,?SCOPE_ID,?REMARKS,?EXPIRE_TIME,?STATUS,?DEVICE_ID,?RIGHT_JSON)";
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_APP_ID,DataRow[_APP_ID]);
			AddParameter(_GRANT_CODE,DataRow[_GRANT_CODE]);
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_EXPIRE_TIME,DataRow[_EXPIRE_TIME]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_DEVICE_ID,DataRow[_DEVICE_ID]);
			AddParameter(_RIGHT_JSON,DataRow[_RIGHT_JSON]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Auth_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_CodeCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_AUTH_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_CODE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE AUTH_ID=?AUTH_ID ");
			AddParameter(_AUTH_ID, DataRow[_AUTH_ID]);			
            foreach (Tauth_CodeCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_CodeCollection.Field.Auth_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_AUTH_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_CODE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE AUTH_ID=?AUTH_ID");
			AddParameter(_AUTH_ID, DataRow[_AUTH_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByAppId_GrantCode(int app_id,string grant_code)
		{
			string condition = null;
			condition += "APP_ID=?APP_ID";
			AddParameter(_APP_ID,app_id);
			condition += " AND GRANT_CODE=?GRANT_CODE";
			AddParameter(_GRANT_CODE,grant_code);

			return SelectByCondition(condition);
		}
		public bool SelectByPk(int auth_id)
		{
			string condition = " AUTH_ID=?AUTH_ID";
			AddParameter(_AUTH_ID,auth_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 授权码[集合对象]
	/// </summary>
	public partial class Tauth_CodeCollection : DataAccessCollectionBase, ITauth_CodeCollection
	{
		#region 构造和基本
		public Tauth_CodeCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Code().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Code(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Code._TableName;}
		}
		public Tauth_Code this[int index]
        {
            get { return new Tauth_Code(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Auth_Id=0,
			User_Id=1,
			App_Id=2,
			Grant_Code=3,
			Scope_Id=4,
			Create_Time=5,
			Remarks=6,
			Expire_Time=7,
			Status=8,
			Device_Id=9,
			Right_Json=10,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT AUTH_ID,USER_ID,APP_ID,GRANT_CODE,SCOPE_ID,CREATE_TIME,REMARKS,EXPIRE_TIME,STATUS,DEVICE_ID,RIGHT_JSON FROM tauth_code WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY AUTH_ID DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Code Find(Predicate<Tauth_Code> match)
        {
            foreach (Tauth_Code item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_CodeCollection FindAll(Predicate<Tauth_Code> match)
        {
            Tauth_CodeCollection list = new Tauth_CodeCollection();
            foreach (Tauth_Code item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Code> match)
        {
            foreach (Tauth_Code item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Code> match)
        {
            BeginTransaction();
            foreach (Tauth_Code item in this)
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