﻿/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 11:33:03
 * @editor:zhengrunqiang
 * @email:653269669@qq.com
 * @date: 2013/5/27 14:57:00
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using MicroAssistant.Common;
using MicroAssistant.Meta;


namespace MicroAssistant.DataAccess
{
    public class SysRoleUserAccessor
    {
        private MySqlCommand cmdInsertSysRoleUser;
        private MySqlCommand cmdDeleteSysRoleUser;
        private MySqlCommand cmdUpdateSysRoleUser;
        private MySqlCommand cmdLoadSysRoleUser;
        private MySqlCommand cmdLoadAllSysRoleUser;
        private MySqlCommand cmdGetSysRoleUserCount;
        private MySqlCommand cmdGetSysRoleUser;
       
        

        private SysRoleUserAccessor()
        {
            #region cmdInsertSysRoleUser

            cmdInsertSysRoleUser = new MySqlCommand("INSERT INTO sys_role_user(role_id,user_id) values (@RoleId,@UserId)");
            cmdInsertSysRoleUser.Parameters.Add("@SysRoleUserId", MySqlDbType.Int32);
            cmdInsertSysRoleUser.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdInsertSysRoleUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateSysRoleUser

            cmdUpdateSysRoleUser = new MySqlCommand(" update sys_role_user set sys_role_user_id = @SysRoleUserId,role_id = @RoleId,user_id = @UserId where sys_role_user_id = @SysRoleUserId");
            cmdUpdateSysRoleUser.Parameters.Add("@SysRoleUserId", MySqlDbType.Int32);
            cmdUpdateSysRoleUser.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdUpdateSysRoleUser.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteSysRoleUser

            cmdDeleteSysRoleUser = new MySqlCommand(" delete from sys_role_user where user_id = @UserId and {0}");
            cmdDeleteSysRoleUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysRoleUser

            cmdLoadSysRoleUser = new MySqlCommand(@" select sys_role_user_id,role_id,user_id from sys_role_user where (@SysRoleFunctionId=0 or sys_role_function_id=@SysRoleFunctionId) and (@RoleId=0 or role_id=@RoleId) and (@UserId=0 or user_id=@UserId) limit @PageIndex,@PageSize");
            cmdLoadSysRoleUser.Parameters.Add("@SysRoleUserId", MySqlDbType.Int32);
            cmdLoadSysRoleUser.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdLoadSysRoleUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdLoadSysRoleUser.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysRoleUser.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysRoleUserCount

            cmdGetSysRoleUserCount = new MySqlCommand(" select count(*)  from sys_role_user where (@SysRoleFunctionId=0 or sys_role_function_id=@SysRoleFunctionId) and (@RoleId=0 or role_id=@RoleId) and (@UserId=0 or user_id=@UserId) ");
            cmdGetSysRoleUserCount.Parameters.Add("@SysRoleUserId", MySqlDbType.Int32);
            cmdGetSysRoleUserCount.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdGetSysRoleUserCount.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion

            #region cmdLoadAllSysRoleUser

            cmdLoadAllSysRoleUser = new MySqlCommand(" select sys_role_user_id,role_id,user_id from sys_role_user");

            #endregion

            #region cmdGetSysRoleUser

            cmdGetSysRoleUser = new MySqlCommand(" select sys_role_user_id,role_id,user_id from sys_role_user where sys_role_user_id = @SysRoleUserId");
            cmdGetSysRoleUser.Parameters.Add("@SysRoleUserId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(SysRoleUser e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysRoleUser = cmdInsertSysRoleUser.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdInsertSysRoleUser.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                //_cmdInsertSysRoleUser.Parameters["@SysRoleUserId"].Value = e.SysRoleUserId;
                _cmdInsertSysRoleUser.Parameters["@RoleId"].Value = e.RoleId;
                _cmdInsertSysRoleUser.Parameters["@UserId"].Value = e.UserId;

                _cmdInsertSysRoleUser.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysRoleUser.Dispose();
                _cmdInsertSysRoleUser = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int UserId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysRoleUser = cmdDeleteSysRoleUser.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysRoleUser.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                string SqlText = string.Empty;
                _cmdDeleteSysRoleUser.CommandText = string.Format(_cmdDeleteSysRoleUser.CommandText, SqlText);
                _cmdDeleteSysRoleUser.Parameters["@UserId"].Value = UserId;

                if (_cmdDeleteSysRoleUser.ExecuteNonQuery() > 0)
                {
                    returnValue = true;
                }
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysRoleUser.Dispose();
                _cmdDeleteSysRoleUser = null;
            }
        }
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public bool Delete(int UserId, int RoleId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysRoleUser = cmdDeleteSysRoleUser.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysRoleUser.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                string SqlText="role_id="+RoleId;
                _cmdDeleteSysRoleUser.CommandText =string.Format(_cmdDeleteSysRoleUser.CommandText,SqlText);
                _cmdDeleteSysRoleUser.Parameters["@UserId"].Value = UserId;
                if (_cmdDeleteSysRoleUser.ExecuteNonQuery() > 0)
                {
                    returnValue = true;
                }
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysRoleUser.Dispose();
                _cmdDeleteSysRoleUser = null;
            }
        }
        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysRoleUser e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysRoleUser = cmdUpdateSysRoleUser.Clone() as MySqlCommand;
            _cmdUpdateSysRoleUser.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysRoleUser.Parameters["@SysRoleUserId"].Value = e.SysRoleUserId;
                _cmdUpdateSysRoleUser.Parameters["@RoleId"].Value = e.RoleId;
                _cmdUpdateSysRoleUser.Parameters["@UserId"].Value = e.UserId;

                _cmdUpdateSysRoleUser.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysRoleUser.Dispose();
                _cmdUpdateSysRoleUser = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 根据条件分页获取指定数据
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>
        public PageEntity<SysRoleUser> Search(Int32 SysRoleUserId, Int32 RoleId, Int32 UserId, int pageIndex, int pageSize)
        {
            PageEntity<SysRoleUser> returnValue = new PageEntity<SysRoleUser>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysRoleUser = cmdLoadSysRoleUser.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysRoleUserCount = cmdGetSysRoleUserCount.Clone() as MySqlCommand;
            _cmdLoadSysRoleUser.Connection = oc;
            _cmdGetSysRoleUserCount.Connection = oc;

            try
            {
                _cmdLoadSysRoleUser.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysRoleUser.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysRoleUser.Parameters["@SysRoleUserId"].Value = SysRoleUserId;
                _cmdLoadSysRoleUser.Parameters["@RoleId"].Value = RoleId;
                _cmdLoadSysRoleUser.Parameters["@UserId"].Value = UserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysRoleUser.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysRoleUser().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetSysRoleUserCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysRoleUser.Dispose();
                _cmdLoadSysRoleUser = null;
                _cmdGetSysRoleUserCount.Dispose();
                _cmdGetSysRoleUserCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysRoleUser> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysRoleUser = cmdLoadAllSysRoleUser.Clone() as MySqlCommand;
            _cmdLoadAllSysRoleUser.Connection = oc; List<SysRoleUser> returnValue = new List<SysRoleUser>();
            try
            {
                _cmdLoadAllSysRoleUser.CommandText = string.Format(_cmdLoadAllSysRoleUser.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysRoleUser.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysRoleUser().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysRoleUser.Dispose();
                _cmdLoadAllSysRoleUser = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysRoleUser Get(int SysRoleUserId)
        {
            SysRoleUser returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysRoleUser = cmdGetSysRoleUser.Clone() as MySqlCommand;

            _cmdGetSysRoleUser.Connection = oc;
            try
            {
                _cmdGetSysRoleUser.Parameters["@SysRoleUserId"].Value = SysRoleUserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysRoleUser.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysRoleUser().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysRoleUser.Dispose();
                _cmdGetSysRoleUser = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysRoleUserAccessor instance = new SysRoleUserAccessor();
        public static SysRoleUserAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
