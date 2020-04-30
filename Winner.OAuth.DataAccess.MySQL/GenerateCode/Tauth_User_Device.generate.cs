   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_User_Device.generate.cs
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
	/// 会员设备
	/// </summary>
	public partial class Tauth_User_Device : DataAccessBase,ITauth_User_Device
	{
		#region 构造和基本
		public Tauth_User_Device():base()
		{}
		public Tauth_User_Device(DataRow dataRow):base(dataRow)
		{}
		public const string _ID = "ID";
		public const string _DEVICE_ID = "DEVICE_ID";
		public const string _USER_ID = "USER_ID";
		public const string _DEVICE_MODEL = "DEVICE_MODEL";
		public const string _IS_AUTHORIZED = "IS_AUTHORIZED";
		public const string _CREATETIME = "CREATETIME";
		public const string _LAST_MODIFY_TIME = "LAST_MODIFY_TIME";
		public const string _LAST_ACCESS_TIME = "LAST_ACCESS_TIME";
		public const string _TableName = "TAUTH_USER_DEVICE";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_USER_DEVICE");
			table.Columns.Add(_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_DEVICE_ID,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue = 0;
			table.Columns.Add(_DEVICE_MODEL,typeof(string)).DefaultValue = string.Empty;
			table.Columns.Add(_IS_AUTHORIZED,typeof(int)).DefaultValue = 1;
			table.Columns.Add(_CREATETIME,typeof(DateTime)).DefaultValue = DateTime.Now;
			table.Columns.Add(_LAST_MODIFY_TIME,typeof(DateTime)).DefaultValue = DateTime.Now;
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
		/// 主键ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Id
		{
			get{ return  Convert.ToInt32(DataRow[_ID]);}
			 set{setProperty(_ID, value);}
		}
		/// <summary>
		/// 主键设备ID(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Device_Id
		{
			get{ return DataRow[_DEVICE_ID].ToString();}
			 set{setProperty(_DEVICE_ID, value);}
		}
		/// <summary>
		/// 拥有者会员ID(必填)
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
		/// 设备型号(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Device_Model
		{
			get{ return DataRow[_DEVICE_MODEL].ToString();}
			 set{setProperty(_DEVICE_MODEL, value);}
		}
		/// <summary>
		/// 是否已授权访问(必填)
		/// <para>
		/// defaultValue: 1;   Length: 0Byte
		/// </para>
		/// </summary>
		public int Is_Authorized
		{
			get{ return  Convert.ToInt32(DataRow[_IS_AUTHORIZED]);}
			 set{setProperty(_IS_AUTHORIZED, value);}
		}
		/// <summary>
		/// 首次使用时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Createtime
		{
			get{ return  Convert.ToDateTime(DataRow[_CREATETIME]);}
		}
		/// <summary>
		/// 上次修改时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 0Byte
		/// </para>
		/// </summary>
		public DateTime Last_Modify_Time
		{
			get{ return  Convert.ToDateTime(DataRow[_LAST_MODIFY_TIME]);}
			 set{setProperty(_LAST_MODIFY_TIME, value);}
		}
		/// <summary>
		/// 上次访问系统时间(必填)
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
			string sql = "SELECT id,device_id,user_id,device_model,is_authorized,createtime,last_modify_time,last_access_time FROM tauth_user_device WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_USER_DEVICE WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int id)
		{
			string condition = " ID=?ID";
			AddParameter(_ID,id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " ID=?ID";
			AddParameter(_ID,DataRow[_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			string sql = @"insert into tauth_user_device(device_id,user_id,device_model,is_authorized,last_modify_time,last_access_time)
			values (?device_id,?user_id,?device_model,?is_authorized,?last_modify_time,?last_access_time)";
			AddParameter(_DEVICE_ID,DataRow[_DEVICE_ID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_DEVICE_MODEL,DataRow[_DEVICE_MODEL]);
			AddParameter(_IS_AUTHORIZED,DataRow[_IS_AUTHORIZED]);
			AddParameter(_LAST_MODIFY_TIME,DataRow[_LAST_MODIFY_TIME]);
			AddParameter(_LAST_ACCESS_TIME,DataRow[_LAST_ACCESS_TIME]);
			int row_id;
			bool result = InsertBySql(sql,out row_id);
			this.Id = row_id;
			return result;
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_User_DeviceCollection.Field,object> conditionDic)
		{
            if (conditionDic.Count <= 0)
                return false;
			ChangePropertys.Remove(_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_USER_DEVICE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?TO_{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter("TO_"+ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE id=?id ");
			AddParameter(_ID, DataRow[_ID]);			
            foreach (Tauth_User_DeviceCollection.Field key in conditionDic.Keys)
            {
				if(Tauth_User_DeviceCollection.Field.Id == key) continue;

                object value = conditionDic[key];
                string name = string.Concat("condition_", key);
                sql.Append(" AND ").Append(key.ToString().ToUpper()).Append("=?").Append(name.ToUpper());
                AddParameter(name, value);
            }            
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_USER_DEVICE SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=?{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE id=?id");
			AddParameter(_ID, DataRow[_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByDeviceId_UserId(string device_id,int user_id)
		{
			string condition = null;
			condition += "DEVICE_ID=?DEVICE_ID AND USER_ID=?USER_ID";
			AddParameter(_DEVICE_ID,device_id);
            AddParameter(_USER_ID, user_id);
			return SelectByCondition(condition);
		}
		public bool SelectByPK(int id)
		{
			string condition = " ID=?ID";
			AddParameter(_ID,id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// 会员设备[集合对象]
	/// </summary>
	public partial class Tauth_User_DeviceCollection : DataAccessCollectionBase,ITauth_User_DeviceCollection
	{
		#region 构造和基本
		public Tauth_User_DeviceCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_User_Device().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_User_Device(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_User_Device._TableName;}
		}
		public Tauth_User_Device this[int index]
        {
            get { return new Tauth_User_Device(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Id=0,
			Device_Id=1,
			User_Id=2,
			Device_Model=3,
			Is_Authorized=4,
			Createtime=5,
			Last_Modify_Time=6,
			Last_Access_Time=7,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT ID,DEVICE_ID,USER_ID,DEVICE_MODEL,IS_AUTHORIZED,CREATETIME,LAST_MODIFY_TIME,LAST_ACCESS_TIME FROM tauth_user_device WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1 ORDER BY id DESC";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_User_Device Find(Predicate<Tauth_User_Device> match)
        {
            foreach (Tauth_User_Device item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_User_DeviceCollection FindAll(Predicate<Tauth_User_Device> match)
        {
            Tauth_User_DeviceCollection list = new Tauth_User_DeviceCollection();
            foreach (Tauth_User_Device item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_User_Device> match)
        {
            foreach (Tauth_User_Device item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_User_Device> match)
        {
            BeginTransaction();
            foreach (Tauth_User_Device item in this)
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