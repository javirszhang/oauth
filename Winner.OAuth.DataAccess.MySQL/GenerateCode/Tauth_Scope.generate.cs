   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Scope.generate.cs
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
	/// 授权作用域
	/// </summary>
	public partial class Tauth_Scope : DataAccessBase, ITauth_Scope
	{
		#region 构造和基本
		public Tauth_Scope():base()
		{}
		public Tauth_Scope(DataRow dataRow):base(dataRow)
		{}
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _SCOPE_CODE = "SCOPE_CODE";
		public const string _SCOPE_NAME = "SCOPE_NAME";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _IS_EXPLLICIT = "IS_EXPLLICIT";
		public const string _TableName = "TAUTH_SCOPE";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_SCOPE");
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_SCOPE_CODE,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_SCOPE_NAME,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue = DBNull.Value;
			table.Columns.Add(_IS_EXPLLICIT,typeof(int)).DefaultValue = 0;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
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
		/// 作用域代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 20Byte
		/// </para>
		/// </summary>
		public string Scope_Code
		{
			get{ return DataRow[_SCOPE_CODE].ToString();}
			 set{setProperty(_SCOPE_CODE, value);}
		}
		/// <summary>
		/// 作用域名字(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 20Byte
		/// </para>
		/// </summary>
		public string Scope_Name
		{
			get{ return DataRow[_SCOPE_NAME].ToString();}
			 set{setProperty(_SCOPE_NAME, value);}
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
		/// defaultValue: DBNull.Value;   Length: 140Byte
		/// </para>
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 是否要求显式授权(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Is_Expllicit
		{
			get{ return Convert.ToInt32(DataRow[_IS_EXPLLICIT]);}
			 set{setProperty(_IS_EXPLLICIT, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT SCOPE_ID,SCOPE_CODE,SCOPE_NAME,CREATE_TIME,REMARKS,IS_EXPLLICIT FROM tauth_scope WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_SCOPE WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int scope_id)
		{
			string condition = " SCOPE_ID=?SCOPE_ID";
			AddParameter(_SCOPE_ID,scope_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " SCOPE_ID=?SCOPE_ID";
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"INSERT INTO tauth_scope(SCOPE_CODE,SCOPE_NAME,REMARKS,IS_EXPLLICIT)
			VALUES (?SCOPE_CODE,?SCOPE_NAME,?REMARKS,?IS_EXPLLICIT)";
			AddParameter(_SCOPE_CODE,DataRow[_SCOPE_CODE]);
			AddParameter(_SCOPE_NAME,DataRow[_SCOPE_NAME]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_IS_EXPLLICIT,DataRow[_IS_EXPLLICIT]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Scope_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_ScopeCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_SCOPE_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_SCOPE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE SCOPE_ID=?SCOPE_ID ");
			AddParameter(_SCOPE_ID, DataRow[_SCOPE_ID]);			
            foreach (Tauth_ScopeCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_ScopeCollection.Field.Scope_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_SCOPE_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_SCOPE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE SCOPE_ID=?SCOPE_ID");
			AddParameter(_SCOPE_ID, DataRow[_SCOPE_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int scope_id)
		{
			string condition = " SCOPE_ID=?SCOPE_ID";
			AddParameter(_SCOPE_ID,scope_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 授权作用域[集合对象]
	/// </summary>
	public partial class Tauth_ScopeCollection : DataAccessCollectionBase, ITauth_ScopeCollection
	{
		#region 构造和基本
		public Tauth_ScopeCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Scope().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Scope(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Scope._TableName;}
		}
		public Tauth_Scope this[int index]
        {
            get { return new Tauth_Scope(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Scope_Id=0,
			Scope_Code=1,
			Scope_Name=2,
			Create_Time=3,
			Remarks=4,
			Is_Expllicit=5,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT SCOPE_ID,SCOPE_CODE,SCOPE_NAME,CREATE_TIME,REMARKS,IS_EXPLLICIT FROM tauth_scope WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY SCOPE_ID DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Scope Find(Predicate<Tauth_Scope> match)
        {
            foreach (Tauth_Scope item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_ScopeCollection FindAll(Predicate<Tauth_Scope> match)
        {
            Tauth_ScopeCollection list = new Tauth_ScopeCollection();
            foreach (Tauth_Scope item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Scope> match)
        {
            foreach (Tauth_Scope item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Scope> match)
        {
            BeginTransaction();
            foreach (Tauth_Scope item in this)
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