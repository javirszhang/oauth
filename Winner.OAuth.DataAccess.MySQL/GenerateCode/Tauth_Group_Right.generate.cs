   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Group_Right.generate.cs
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
	/// API分组权限
	/// </summary>
	public partial class Tauth_Group_Right : DataAccessBase, ITauth_Group_Right
	{
		#region 构造和基本
		public Tauth_Group_Right():base()
		{}
		public Tauth_Group_Right(DataRow dataRow):base(dataRow)
		{}
		public const string _RIGHT_ID = "RIGHT_ID";
		public const string _GROUP_ID = "GROUP_ID";
		public const string _API_ID = "API_ID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _TableName = "TAUTH_GROUP_RIGHT";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_GROUP_RIGHT");
			table.Columns.Add(_RIGHT_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_GROUP_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_API_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue = DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 主键(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Right_Id
		{
			get{ return Convert.ToInt32(DataRow[_RIGHT_ID]);}
			 set{setProperty(_RIGHT_ID, value);}
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
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT RIGHT_ID,GROUP_ID,API_ID,CREATE_TIME,REMARKS FROM tauth_group_right WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_GROUP_RIGHT WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int right_id)
		{
			string condition = " RIGHT_ID=?RIGHT_ID";
			AddParameter(_RIGHT_ID,right_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " RIGHT_ID=?RIGHT_ID";
			AddParameter(_RIGHT_ID,DataRow[_RIGHT_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"INSERT INTO tauth_group_right(GROUP_ID,API_ID,REMARKS)
			VALUES (?GROUP_ID,?API_ID,?REMARKS)";
			AddParameter(_GROUP_ID,DataRow[_GROUP_ID]);
			AddParameter(_API_ID,DataRow[_API_ID]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Right_Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_Group_RightCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_RIGHT_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_GROUP_RIGHT SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE RIGHT_ID=?RIGHT_ID ");
			AddParameter(_RIGHT_ID, DataRow[_RIGHT_ID]);			
            foreach (Tauth_Group_RightCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_Group_RightCollection.Field.Right_Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_RIGHT_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_GROUP_RIGHT SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE RIGHT_ID=?RIGHT_ID");
			AddParameter(_RIGHT_ID, DataRow[_RIGHT_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int right_id)
		{
			string condition = " RIGHT_ID=?RIGHT_ID";
			AddParameter(_RIGHT_ID,right_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// API分组权限[集合对象]
	/// </summary>
	public partial class Tauth_Group_RightCollection : DataAccessCollectionBase, ITauth_Group_RightCollection
	{
		#region 构造和基本
		public Tauth_Group_RightCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Group_Right().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Group_Right(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Group_Right._TableName;}
		}
		public Tauth_Group_Right this[int index]
        {
            get { return new Tauth_Group_Right(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Right_Id=0,
			Group_Id=1,
			Api_Id=2,
			Create_Time=3,
			Remarks=4,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT RIGHT_ID,GROUP_ID,API_ID,CREATE_TIME,REMARKS FROM tauth_group_right WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListByGroup_Id(int group_id)
		{
			string condition = "GROUP_ID=?GROUP_ID ORDER BY RIGHT_ID DESC";
			AddParameter(Tauth_Group_Right._GROUP_ID,group_id);
			return ListByCondition(condition);		
		}
		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY RIGHT_ID DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Group_Right Find(Predicate<Tauth_Group_Right> match)
        {
            foreach (Tauth_Group_Right item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_Group_RightCollection FindAll(Predicate<Tauth_Group_Right> match)
        {
            Tauth_Group_RightCollection list = new Tauth_Group_RightCollection();
            foreach (Tauth_Group_Right item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Group_Right> match)
        {
            foreach (Tauth_Group_Right item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Group_Right> match)
        {
            BeginTransaction();
            foreach (Tauth_Group_Right item in this)
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