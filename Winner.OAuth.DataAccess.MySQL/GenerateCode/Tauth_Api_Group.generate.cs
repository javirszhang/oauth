   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Api_Group.generate.cs
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
	/// API分组信息
	/// </summary>
	public partial class Tauth_Api_Group : DataAccessBase, ITauth_Api_Group
	{
		#region 构造和基本
		public Tauth_Api_Group():base()
		{}
		public Tauth_Api_Group(DataRow dataRow):base(dataRow)
		{}
		public const string _GROUP_ID = "GROUP_ID";
		public const string _GROUP_NAME = "GROUP_NAME";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _TableName = "TAUTH_API_GROUP";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_API_GROUP");
			table.Columns.Add(_GROUP_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_GROUP_NAME,typeof(string)).DefaultValue = string.Empty;
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
		/// 分组ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Group_Id
		{
			get{ return Convert.ToInt32(DataRow[_GROUP_ID]);}
			 set{setProperty(_GROUP_ID, value);}
		}
		/// <summary>
		/// 分组名称(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 20Byte
		/// </para>
		/// </summary>
		public string Group_Name
		{
			get{ return DataRow[_GROUP_NAME].ToString();}
			 set{setProperty(_GROUP_NAME, value);}
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
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT GROUP_ID,GROUP_NAME,CREATE_TIME FROM tauth_api_group WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_API_GROUP WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int group_id)
		{
			string condition = " GROUP_ID=?GROUP_ID";
			AddParameter(_GROUP_ID,group_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " GROUP_ID=?GROUP_ID";
			AddParameter(_GROUP_ID,DataRow[_GROUP_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"INSERT INTO tauth_api_group(GROUP_NAME,)
			VALUES (?GROUP_NAME,)";
			AddParameter(_GROUP_NAME,DataRow[_GROUP_NAME]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Group_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_Api_GroupCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_GROUP_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_API_GROUP SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE GROUP_ID=?GROUP_ID ");
			AddParameter(_GROUP_ID, DataRow[_GROUP_ID]);			
            foreach (Tauth_Api_GroupCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_Api_GroupCollection.Field.Group_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_GROUP_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_API_GROUP SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE GROUP_ID=?GROUP_ID");
			AddParameter(_GROUP_ID, DataRow[_GROUP_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int group_id)
		{
			string condition = " GROUP_ID=?GROUP_ID";
			AddParameter(_GROUP_ID,group_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// API分组信息[集合对象]
	/// </summary>
	public partial class Tauth_Api_GroupCollection : DataAccessCollectionBase, ITauth_Api_GroupCollection
	{
		#region 构造和基本
		public Tauth_Api_GroupCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Api_Group().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Api_Group(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Api_Group._TableName;}
		}
		public Tauth_Api_Group this[int index]
        {
            get { return new Tauth_Api_Group(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Group_Id=0,
			Group_Name=1,
			Create_Time=2,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT GROUP_ID,GROUP_NAME,CREATE_TIME FROM tauth_api_group WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY GROUP_ID DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Api_Group Find(Predicate<Tauth_Api_Group> match)
        {
            foreach (Tauth_Api_Group item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_Api_GroupCollection FindAll(Predicate<Tauth_Api_Group> match)
        {
            Tauth_Api_GroupCollection list = new Tauth_Api_GroupCollection();
            foreach (Tauth_Api_Group item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Api_Group> match)
        {
            foreach (Tauth_Api_Group item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Api_Group> match)
        {
            BeginTransaction();
            foreach (Tauth_Api_Group item in this)
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