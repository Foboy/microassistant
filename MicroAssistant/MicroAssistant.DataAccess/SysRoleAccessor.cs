/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/11/2 14:30:49
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
        private MySqlCommand cmdLoadEntRole;
        private MySqlCommand cmdDeleteByUserId;
        private MySqlCommand cmdGetSysRoleByUserId;

        private SysRoleAccessor()
        {
            #region cmdInsertSysRole

            cmdInsertSysRole = new MySqlCommand("INSERT INTO sys_role(role_name,ent_id,father_id) values (@RoleName,@EntId,@FatherId)");

            cmdInsertSysRole.Parameters.Add("@RoleName", MySqlDbType.String);
            cmdInsertSysRole.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertSysRole.Parameters.Add("@FatherId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateSysRole

            cmdUpdateSysRole = new MySqlCommand(" update sys_role set role_name = @RoleName,ent_id = @EntId,father_id = @FatherId where role_id = @RoleId");
            cmdUpdateSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdUpdateSysRole.Parameters.Add("@RoleName", MySqlDbType.String);
            cmdUpdateSysRole.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateSysRole.Parameters.Add("@FatherId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteSysRole

            cmdDeleteSysRole = new MySqlCommand(" delete from sys_role where role_id = @RoleId");
            cmdDeleteSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);
            #endregion

            #region cmdDeleteByUserId

            cmdDeleteByUserId = new MySqlCommand(" delete from sys_role where user_id = @UserId");
            cmdDeleteByUserId.Parameters.Add("@UserId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysRole

            cmdLoadSysRole = new MySqlCommand(@" select role_id,role_name,ent_id,father_id from sys_role limit @PageIndex,@PageSize");
            cmdLoadSysRole.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysRole.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysRoleCount

            cmdGetSysRoleCount = new MySqlCommand(" select count(*)  from sys_role ");

            #endregion

            #region cmdLoadAllSysRole

            cmdLoadAllSysRole = new MySqlCommand(" select role_id,role_name,ent_id,father_id from sys_role");

            #endregion

            #region cmdGetSysRole

            cmdGetSysRole = new MySqlCommand(" select role_id,role_name,ent_id,father_id from sys_role where role_id = @RoleId");
            cmdGetSysRole.Parameters.Add("@RoleId", MySqlDbType.Int32);

            #endregion

            #region cmdLoadEntRole

            cmdLoadEntRole = new MySqlCommand(@" select 
    0 role_id,
    '全部' role_name,
    0 father_id,
    count(b.sys_role_user_id) count
from
    sys_role_user b
where
    ent_id = @EntId
union select 
    -1 role_id,
    '未审核' role_name,
    0 father_id,
    (select 
            count(b.user_id)
        from
            sys_user b
        where
            ent_id = @EntId
                and b.user_id not in (select 
                    a.user_id
                from
                    sys_role_user a
                where
                    a.ent_id = @EntId))
from dual 
union select 
    a.role_id,
    a.role_name,
    a.father_id,
    (select 
            count(b.sys_role_user_id)
        from
            sys_role_user b
        where
            a.role_id = b.role_id) count
from
    sys_role a
where
    ent_id = @EntId

");
            cmdLoadEntRole.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysRoleByUserId 获取某个用户所有角色列表信息
            cmdGetSysRoleByUserId = new MySqlCommand(@"SELECT 
        s.*
    from
        sys_role s
    right JOIN (select 
		r.role_id
    from
        sys_role_user r
    WHERE
        r.user_id = @UserId) p ON p.role_id = s.role_id");
            cmdGetSysRoleByUserId.Parameters.Add("@UserId", MySqlDbType.Int32);
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
                _cmdInsertSysRole.Parameters["@RoleName"].Value = e.RoleName;
                _cmdInsertSysRole.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertSysRole.Parameters["@FatherId"].Value = e.FatherId;

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
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool DeleteByUserId(int userId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteByUserId = cmdDeleteByUserId.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteByUserId.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteByUserId.Parameters["@UserId"].Value = userId;


                _cmdDeleteByUserId.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteByUserId.Dispose();
                _cmdDeleteByUserId = null;
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
                _cmdUpdateSysRole.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateSysRole.Parameters["@FatherId"].Value = e.FatherId;

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
        public PageEntity<SysRole> Search(Int32 RoleId, String RoleName, Int32 EntId, Int32 FatherId, int pageIndex, int pageSize)
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
                _cmdLoadSysRole.Parameters["@EntId"].Value = EntId;
                _cmdLoadSysRole.Parameters["@FatherId"].Value = FatherId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysRole.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysRole().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetSysRoleCount.ExecuteScalar());
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

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysRole> LoadEntRole(int entId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadEntRole = cmdLoadEntRole.Clone() as MySqlCommand;
            _cmdLoadEntRole.Connection = oc; 
            List<SysRole> returnValue = new List<SysRole>();
            try
            {

                _cmdLoadEntRole.Parameters["@EntId"].Value = entId;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadEntRole.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysRole().BuildCountEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadEntRole.Dispose();
                _cmdLoadEntRole = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 加载某个用户所有权限列表（包括所有级）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<SysRole> SearchSysRolesByUserId(int userId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysRoleByUserId = cmdGetSysRoleByUserId.Clone() as MySqlCommand;
            _cmdGetSysRoleByUserId.Connection = oc;
            List<SysRole> returnValue = new List<SysRole>();
            try
            {

                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdGetSysRoleByUserId.Parameters["@UserId"].Value = userId;
                MySqlDataReader reader = _cmdGetSysRoleByUserId.ExecuteReader();
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
                _cmdGetSysRoleByUserId.Dispose();
                _cmdGetSysRoleByUserId = null;
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
