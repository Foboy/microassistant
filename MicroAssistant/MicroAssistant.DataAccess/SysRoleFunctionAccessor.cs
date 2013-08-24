/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 11:37:03
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
    public class SysRoleFunctionAccessor
    {
        private MySqlCommand cmdInsertSysRoleFunction;
        private MySqlCommand cmdDeleteSysRoleFunction;
        private MySqlCommand cmdDeleteSysRoleFunctionByRoleId;
        private MySqlCommand cmdUpdateSysRoleFunction;
        private MySqlCommand cmdLoadSysRoleFunction;
        private MySqlCommand cmdLoadAllSysRoleFunction;
        private MySqlCommand cmdGetSysRoleFunctionCount;
        private MySqlCommand cmdGetSysRoleFunction;

        private SysRoleFunctionAccessor()
        {
            #region cmdInsertSysRoleFunction

            cmdInsertSysRoleFunction = new MySqlCommand("INSERT INTO sys_role_function(sys_role_function_id,role_id,function_id) values (@SysRoleFunctionId,@RoleId,@FunctionId)");

            cmdInsertSysRoleFunction.Parameters.Add("@SysRoleFunctionId", MySqlDbType.Int32);
            cmdInsertSysRoleFunction.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdInsertSysRoleFunction.Parameters.Add("@FunctionId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateSysRoleFunction

            cmdUpdateSysRoleFunction = new MySqlCommand("update sys_role_function set sys_role_function_id = @SysRoleFunctionId,role_id = @RoleId,function_id = @FunctionId where sys_role_function_id = @SysRoleFunctionId");
            cmdUpdateSysRoleFunction.Parameters.Add("@SysRoleFunctionId", MySqlDbType.Int32);
            cmdUpdateSysRoleFunction.Parameters.Add("@RoleId", MySqlDbType.Int32);
            cmdUpdateSysRoleFunction.Parameters.Add("@FunctionId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteSysRoleFunction

            cmdDeleteSysRoleFunction = new MySqlCommand(" delete from sys_role_function where sys_role_function_id = @SysRoleFunctionId");
            cmdDeleteSysRoleFunction.Parameters.Add("@SysRoleFunctionId", MySqlDbType.Int32);
            #endregion

            #region cmdDeleteSysRoleFunctionByRoleId

            cmdDeleteSysRoleFunctionByRoleId = new MySqlCommand(" delete from sys_role_function where role_id = @RoleId");
            cmdDeleteSysRoleFunctionByRoleId.Parameters.Add("@RoleId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysRoleFunction 选择条件查询

            cmdLoadSysRoleFunction = new MySqlCommand(@" select sys_role_function_id,role_id,function_id from sys_role_function where (@SysRoleFunctionId=0 or sys_role_function_id=@SysRoleFunctionId) and (@RoleId=0 or role_id=@RoleId) and (@FunctionId=0 and function_id =@FunctionId) limit @PageIndex,@PageSize");
            cmdLoadSysRoleFunction.Parameters.Add("@SysRoleFunctionId", MySqlDbType.Int32);
            cmdLoadSysRoleFunction.Parameters.Add("RoleId", MySqlDbType.Int32);
            cmdLoadSysRoleFunction.Parameters.Add("FunctionId", MySqlDbType.Int32);
            cmdLoadSysRoleFunction.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysRoleFunction.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion cmdLoadSysRoleFunction 选择条件查询

            #region cmdGetSysRoleFunctionCount 选择条件查询数据总条数

            cmdGetSysRoleFunctionCount = new MySqlCommand(" select count(*) from sys_role_function where (@SysRoleFunctionId=0 or sys_role_function_id=@SysRoleFunctionId) and (@RoleId=0 or role_id=@RoleId) and (@FunctionId=0 and function_id =@FunctionId)");
            cmdGetSysRoleFunctionCount.Parameters.Add("@SysRoleFunctionId", MySqlDbType.Int32);
            cmdGetSysRoleFunctionCount.Parameters.Add("RoleId", MySqlDbType.Int32);
            cmdGetSysRoleFunctionCount.Parameters.Add("FunctionId", MySqlDbType.Int32);

            #endregion cmdGetSysRoleFunctionCount 选择条件查询数据总条数

            #region cmdLoadAllSysRoleFunction

            cmdLoadAllSysRoleFunction = new MySqlCommand(" select sys_role_function_id,role_id,function_id from sys_role_function");

            #endregion

            #region cmdGetSysRoleFunction

            cmdGetSysRoleFunction = new MySqlCommand(" select sys_role_function_id,role_id,function_id from sys_role_function where sys_role_function_id = @SysRoleFunctionId");
            cmdGetSysRoleFunction.Parameters.Add("@SysRoleFunctionId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(SysRoleFunction e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysRoleFunction = cmdInsertSysRoleFunction.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdInsertSysRoleFunction.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysRoleFunction.Parameters["@SysRoleFunctionId"].Value = e.SysRoleFunctionId;
                _cmdInsertSysRoleFunction.Parameters["@RoleId"].Value = e.RoleId;
                _cmdInsertSysRoleFunction.Parameters["@FunctionId"].Value = e.FunctionId;

                _cmdInsertSysRoleFunction.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysRoleFunction.Dispose();
                _cmdInsertSysRoleFunction = null;
            }
        }

        /// <summary>
        /// 为某个角色添加权限
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(int roleid, List<int> functionids)
        {
            bool returnValue = false;
            try
            {
                for (int i = 0; i < functionids.Count; i++)
                {
                    SysRoleFunction srf = new SysRoleFunction();
                    srf.FunctionId=functionids[i];
                    srf.RoleId=roleid;
                    Insert(srf);
                }
                returnValue = true;
            }
            catch
            {

            }
            return returnValue;
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int SysRoleFunctionId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysRoleFunction = cmdDeleteSysRoleFunction.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysRoleFunction.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysRoleFunction.Parameters["@SysRoleFunctionId"].Value = SysRoleFunctionId;


                _cmdDeleteSysRoleFunction.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysRoleFunction.Dispose();
                _cmdDeleteSysRoleFunction = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool DeleteByRoleId(int roleId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysRoleFunctionByRoleId = cmdDeleteSysRoleFunctionByRoleId.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysRoleFunctionByRoleId.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysRoleFunctionByRoleId.Parameters["@RoleId"].Value = roleId;


                _cmdDeleteSysRoleFunctionByRoleId.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysRoleFunctionByRoleId.Dispose();
                _cmdDeleteSysRoleFunctionByRoleId = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysRoleFunction e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysRoleFunction = cmdUpdateSysRoleFunction.Clone() as MySqlCommand;
            _cmdUpdateSysRoleFunction.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysRoleFunction.Parameters["@SysRoleFunctionId"].Value = e.SysRoleFunctionId;
                _cmdUpdateSysRoleFunction.Parameters["@RoleId"].Value = e.RoleId;
                _cmdUpdateSysRoleFunction.Parameters["@FunctionId"].Value = e.FunctionId;

                _cmdUpdateSysRoleFunction.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysRoleFunction.Dispose();
                _cmdUpdateSysRoleFunction = null;
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
        public PageEntity<SysRoleFunction> Search(Int32 SysRoleFunctionId, Int32 RoleId, Int32 FunctionId, int pageIndex, int pageSize)
        {
            PageEntity<SysRoleFunction> returnValue = new PageEntity<SysRoleFunction>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysRoleFunction = cmdLoadSysRoleFunction.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysRoleFunctionCount = cmdGetSysRoleFunctionCount.Clone() as MySqlCommand;
            _cmdLoadSysRoleFunction.Connection = oc;
            _cmdGetSysRoleFunctionCount.Connection = oc;

            try
            {
                _cmdLoadSysRoleFunction.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysRoleFunction.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysRoleFunction.Parameters["@SysRoleFunctionId"].Value = SysRoleFunctionId;
                _cmdLoadSysRoleFunction.Parameters["@RoleId"].Value = RoleId;
                _cmdLoadSysRoleFunction.Parameters["@FunctionId"].Value = FunctionId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysRoleFunction.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysRoleFunction().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetSysRoleFunctionCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysRoleFunction.Dispose();
                _cmdLoadSysRoleFunction = null;
                _cmdGetSysRoleFunctionCount.Dispose();
                _cmdGetSysRoleFunctionCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysRoleFunction> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysRoleFunction = cmdLoadAllSysRoleFunction.Clone() as MySqlCommand;
            _cmdLoadAllSysRoleFunction.Connection = oc; List<SysRoleFunction> returnValue = new List<SysRoleFunction>();
            try
            {
                _cmdLoadAllSysRoleFunction.CommandText = string.Format(_cmdLoadAllSysRoleFunction.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysRoleFunction.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysRoleFunction().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysRoleFunction.Dispose();
                _cmdLoadAllSysRoleFunction = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysRoleFunction Get(int SysRoleFunctionId)
        {
            SysRoleFunction returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysRoleFunction = cmdGetSysRoleFunction.Clone() as MySqlCommand;

            _cmdGetSysRoleFunction.Connection = oc;
            try
            {
                _cmdGetSysRoleFunction.Parameters["@SysRoleFunctionId"].Value = SysRoleFunctionId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysRoleFunction.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysRoleFunction().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysRoleFunction.Dispose();
                _cmdGetSysRoleFunction = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysRoleFunctionAccessor instance = new SysRoleFunctionAccessor();
        public static SysRoleFunctionAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
