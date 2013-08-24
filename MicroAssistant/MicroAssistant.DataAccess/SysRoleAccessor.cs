/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 11:37:29
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
    public class SysRoleAccessor
    {
        private MySqlCommand cmdInsertSysRole;
        private MySqlCommand cmdDeleteSysRole;
        private MySqlCommand cmdUpdateSysRole;
        private MySqlCommand cmdLoadSysRole;
        private MySqlCommand cmdLoadAllSysRole;
        private MySqlCommand cmdGetSysRoleCount;
        private MySqlCommand cmdGetSysRole;

        private SysRoleAccessor()
        {
            #region cmdInsertSysRole

            cmdInsertSysRole = new MySqlCommand("INSERT INTO sys_role(role_id,role_name) values (@RoleId,@RoleName)");

            cmdInsertSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdInsertSysRole.Parameters.Add("@RoleName", MySqlDbType.String);
            #endregion

            #region cmdUpdateSysRole

            cmdUpdateSysRole = new MySqlCommand(" update sys_role set role_id = @RoleId,role_name = @RoleName where role_id = @RoleId");
            cmdUpdateSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdUpdateSysRole.Parameters.Add("@RoleName", MySqlDbType.String);

            #endregion

            #region cmdDeleteSysRole

            cmdDeleteSysRole = new MySqlCommand(" delete from sys_role where role_id = @RoleId");
            cmdDeleteSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysRole

            cmdLoadSysRole = new MySqlCommand(@" select role_id,role_name from sys_role limit @PageIndex,@PageSize");
            cmdLoadSysRole.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysRole.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysRoleCount

            cmdGetSysRoleCount = new MySqlCommand(" select count(*)  from sys_role ");

            #endregion

            #region cmdLoadAllSysRole

            cmdLoadAllSysRole = new MySqlCommand(" select role_id,role_name from sys_role");

            #endregion

            #region cmdGetSysRole

            cmdGetSysRole = new MySqlCommand(" select role_id,role_name from sys_role where role_id = @RoleId");
            cmdGetSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(SysRole e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysRole = cmdInsertSysRole.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertSysRole.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysRole.Parameters["@RoleId"].Value = e.RoleId;
                _cmdInsertSysRole.Parameters["@RoleName"].Value = e.RoleName;
                _cmdInsertSysRole.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertSysRole.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysRole.Dispose();
                _cmdInsertSysRole = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int RoleId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysRole = cmdDeleteSysRole.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysRole.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysRole.Parameters["@RoleId"].Value = RoleId;


                _cmdDeleteSysRole.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysRole.Dispose();
                _cmdDeleteSysRole = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysRole e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysRole = cmdUpdateSysRole.Clone() as MySqlCommand;
            _cmdUpdateSysRole.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysRole.Parameters["@RoleId"].Value = e.RoleId;
                _cmdUpdateSysRole.Parameters["@RoleName"].Value = e.RoleName;

                _cmdUpdateSysRole.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysRole.Dispose();
                _cmdUpdateSysRole = null;
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
        public PageEntity<SysRole> Search(Int32 RoleId, String RoleName, int pageIndex, int pageSize)
        {
            PageEntity<SysRole> returnValue = new PageEntity<SysRole>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysRole = cmdLoadSysRole.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysRoleCount = cmdGetSysRoleCount.Clone() as MySqlCommand;
            _cmdLoadSysRole.Connection = oc;
            _cmdGetSysRoleCount.Connection = oc;

            try
            {
                _cmdLoadSysRole.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysRole.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysRole.Parameters["@RoleId"].Value = RoleId;
                _cmdLoadSysRole.Parameters["@RoleName"].Value = RoleName;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysRole.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysRole().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetSysRoleCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysRole.Dispose();
                _cmdLoadSysRole = null;
                _cmdGetSysRoleCount.Dispose();
                _cmdGetSysRoleCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysRole> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysRole = cmdLoadAllSysRole.Clone() as MySqlCommand;
            _cmdLoadAllSysRole.Connection = oc; List<SysRole> returnValue = new List<SysRole>();
            try
            {
                _cmdLoadAllSysRole.CommandText = string.Format(_cmdLoadAllSysRole.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysRole.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysRole().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysRole.Dispose();
                _cmdLoadAllSysRole = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysRole Get(int RoleId)
        {
            SysRole returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysRole = cmdGetSysRole.Clone() as MySqlCommand;

            _cmdGetSysRole.Connection = oc;
            try
            {
                _cmdGetSysRole.Parameters["@RoleId"].Value = RoleId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysRole.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysRole().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysRole.Dispose();
                _cmdGetSysRole = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysRoleAccessor instance = new SysRoleAccessor();
        public static SysRoleAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
