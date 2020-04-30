   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Scope_Apis.generate.cs
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
	/// 授权作用域允许调用的API
	/// </summary>
	public partial class Tauth_Scope_Apis : DataAccessBase, ITauth_Scope_Apis
	{
		#region 构造和基本
		public Tauth_Scope_Apis():base()
		{}
		public Tauth_Scope_Apis(DataRow dataRow):base(dataRow)
		{}
		public const string _API_ID = "API_ID";
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _API_URL = "API_URL";
		public const string _API_NAME = "API_NAME";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _STATUS = "STATUS";
		public const string _TableName = "TAUTH_SCOPE_APIS";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_SCOPE_APIS");
			table.Columns.Add(_API_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_API_URL,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_API_NAME,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
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
		/// 接口ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Api_Id
		{
			get{ return Convert.ToInt32(DataRow[_API_ID]);}
			 set{setProperty(_API_ID, value);}
		}
		/// <summary>
		/// 作用域ID(必填)
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
		/// api地址(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Api_Url
		{
			get{ return DataRow[_API_URL].ToString();}
			 set{setProperty(_API_URL, value);}
		}
		/// <summary>
		/// api名称（api提供的功能描述）(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Api_Name
		{
			get{ return DataRow[_API_NAME].ToString();}
			 set{setProperty(_API_NAME, value);}
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
		/// api状态(必填)
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
			string sql = "SELECT API_ID,SCOPE_ID,API_URL,API_NAME,CREATE_TIME,STATUS FROM tauth_scope_apis WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_SCOPE_APIS WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int api_id)
		{
			string condition = " API_ID=?API_ID";
			AddParameter(_API_ID,api_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " API_ID=?API_ID";
			AddParameter(_API_ID,DataRow[_API_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"INSERT INTO tauth_scope_apis(SCOPE_ID,API_URL,API_NAME,STATUS)
			VALUES (?SCOPE_ID,?API_URL,?API_NAME,?STATUS)";
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			AddParameter(_API_URL,DataRow[_API_URL]);
			AddParameter(_API_NAME,DataRow[_API_NAME]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Api_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_Scope_ApisCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_API_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_SCOPE_APIS SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE API_ID=?API_ID ");
			AddParameter(_API_ID, DataRow[_API_ID]);			
            foreach (Tauth_Scope_ApisCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_Scope_ApisCollection.Field.Api_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_API_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_SCOPE_APIS SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE API_ID=?API_ID");
			AddParameter(_API_ID, DataRow[_API_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int api_id)
		{
			string condition = " API_ID=?API_ID";
			AddParameter(_API_ID,api_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 授权作用域允许调用的API[集合对象]
	/// </summary>
	public partial class Tauth_Scope_ApisCollection : DataAccessCollectionBase, ITauth_Scope_ApisCollection
	{
		#region 构造和基本
		public Tauth_Scope_ApisCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Scope_Apis().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Scope_Apis(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Scope_Apis._TableName;}
		}
		public Tauth_Scope_Apis this[int index]
        {
            get { return new Tauth_Scope_Apis(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Api_Id=0,
			Scope_Id=1,
			Api_Url=2,
			Api_Name=3,
			Create_Time=4,
			Status=5,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT API_ID,SCOPE_ID,API_URL,API_NAME,CREATE_TIME,STATUS FROM tauth_scope_apis WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListByScope_Id(int scope_id)
		{
			string condition = "SCOPE_ID=?SCOPE_ID ORDER BY API_ID DESC";
			AddParameter(Tauth_Scope_Apis._SCOPE_ID,scope_id);
			return ListByCondition(condition);		
		}
		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY API_ID DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Scope_Apis Find(Predicate<Tauth_Scope_Apis> match)
        {
            foreach (Tauth_Scope_Apis item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_Scope_ApisCollection FindAll(Predicate<Tauth_Scope_Apis> match)
        {
            Tauth_Scope_ApisCollection list = new Tauth_Scope_ApisCollection();
            foreach (Tauth_Scope_Apis item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Scope_Apis> match)
        {
            foreach (Tauth_Scope_Apis item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Scope_Apis> match)
        {
            BeginTransaction();
            foreach (Tauth_Scope_Apis item in this)
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