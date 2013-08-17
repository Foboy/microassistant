/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 12:16:44
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
    public class SysFunctionAccessor
    {
        private MySqlCommand cmdInsertSysFunction;
        private MySqlCommand cmdDeleteSysFunction;
        private MySqlCommand cmdUpdateSysFunction;
        private MySqlCommand cmdLoadSysFunction;
        private MySqlCommand cmdLoadAllSysFunction;
        private MySqlCommand cmdGetSysFunctionCount;
        private MySqlCommand cmdGetSysFunction;
        private MySqlCommand cmdGetSysUserRolePermisson;
        

        private SysFunctionAccessor()
        {
            #region cmdInsertSysFunction

            cmdInsertSysFunction = new MySqlCommand("INSERT INTO sys_function(idsys_function,function_name,father_id,mark,function_url,function_code,level) values (@IdsysFunction,@FunctionName,@FatherId,@Mark,@FunctionUrl,@FunctionCode,@Level)");

            cmdInsertSysFunction.Parameters.Add("@IdsysFunction", MySqlDbType.Int32);
            cmdInsertSysFunction.Parameters.Add("@FunctionName", MySqlDbType.String);
            cmdInsertSysFunction.Parameters.Add("@FatherId", MySqlDbType.Int32);
            cmdInsertSysFunction.Parameters.Add("@Mark", MySqlDbType.String);
            cmdInsertSysFunction.Parameters.Add("@FunctionUrl", MySqlDbType.String);
            cmdInsertSysFunction.Parameters.Add("@FunctionCode", MySqlDbType.String);
            cmdInsertSysFunction.Parameters.Add("@Level", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateSysFunction

            cmdUpdateSysFunction = new MySqlCommand(" update sys_function set idsys_function = @IdsysFunction,function_name = @FunctionName,father_id = @FatherId,mark = @Mark,function_url = @FunctionUrl,function_code = @FunctionCode,level = @Level where idsys_function = @IdsysFunction");
            cmdUpdateSysFunction.Parameters.Add("@IdsysFunction", MySqlDbType.Int32);
            cmdUpdateSysFunction.Parameters.Add("@FunctionName", MySqlDbType.String);
            cmdUpdateSysFunction.Parameters.Add("@FatherId", MySqlDbType.Int32);
            cmdUpdateSysFunction.Parameters.Add("@Mark", MySqlDbType.String);
            cmdUpdateSysFunction.Parameters.Add("@FunctionUrl", MySqlDbType.String);
            cmdUpdateSysFunction.Parameters.Add("@FunctionCode", MySqlDbType.String);
            cmdUpdateSysFunction.Parameters.Add("@Level", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteSysFunction

            cmdDeleteSysFunction = new MySqlCommand("delete from sys_function where idsys_function = @IdsysFunction");
            cmdDeleteSysFunction.Parameters.Add("@IdsysFunction", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysFunction

            cmdLoadSysFunction = new MySqlCommand(@" select idsys_function,function_name,father_id,mark,function_url,function_code,level from sys_function limit @PageIndex,@PageSize");
            cmdLoadSysFunction.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysFunction.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysFunctionCount

            cmdGetSysFunctionCount = new MySqlCommand(" select count(*)  from sys_function ");

            #endregion

            #region cmdLoadAllSysFunction

            cmdLoadAllSysFunction = new MySqlCommand("select idsys_function,function_name,father_id,mark,function_url,function_code,level from sys_function");

            #endregion

            #region cmdGetSysFunction

            cmdGetSysFunction = new MySqlCommand("select idsys_function,function_name,father_id,mark,function_url,function_code,level from sys_function where idsys_function = @IdsysFunction");
            cmdGetSysFunction.Parameters.Add("@IdsysFunction", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysUserRolePermisson 获取某个用户所有权限列表信息
            cmdGetSysUserRolePermisson = new MySqlCommand("SELECT * from sys_function f RIGHT JOIN( SELECT s.function_id from sys_role_function s RIGHT JOIN ( select *  from sys_role_user r WHERE r.user_id=@UserId) p ON p.role_id=s.role_id) o ON o.function_id=f.idsys_function");
            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(SysFunction e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysFunction = cmdInsertSysFunction.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdInsertSysFunction.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysFunction.Parameters["@IdsysFunction"].Value = e.IdsysFunction;
                _cmdInsertSysFunction.Parameters["@FunctionName"].Value = e.FunctionName;
                _cmdInsertSysFunction.Parameters["@FatherId"].Value = e.FatherId;
                _cmdInsertSysFunction.Parameters["@Mark"].Value = e.Mark;
                _cmdInsertSysFunction.Parameters["@FunctionUrl"].Value = e.FunctionUrl;
                _cmdInsertSysFunction.Parameters["@FunctionCode"].Value = e.FunctionCode;
                _cmdInsertSysFunction.Parameters["@Level"].Value = e.Level;

                _cmdInsertSysFunction.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysFunction.Dispose();
                _cmdInsertSysFunction = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int IdsysFunction)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysFunction = cmdDeleteSysFunction.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysFunction.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysFunction.Parameters["@IdsysFunction"].Value = IdsysFunction;


                _cmdDeleteSysFunction.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysFunction.Dispose();
                _cmdDeleteSysFunction = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysFunction e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysFunction = cmdUpdateSysFunction.Clone() as MySqlCommand;
            _cmdUpdateSysFunction.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysFunction.Parameters["@IdsysFunction"].Value = e.IdsysFunction;
                _cmdUpdateSysFunction.Parameters["@FunctionName"].Value = e.FunctionName;
                _cmdUpdateSysFunction.Parameters["@FatherId"].Value = e.FatherId;
                _cmdUpdateSysFunction.Parameters["@Mark"].Value = e.Mark;
                _cmdUpdateSysFunction.Parameters["@FunctionUrl"].Value = e.FunctionUrl;
                _cmdUpdateSysFunction.Parameters["@FunctionCode"].Value = e.FunctionCode;
                _cmdUpdateSysFunction.Parameters["@Level"].Value = e.Level;

                _cmdUpdateSysFunction.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysFunction.Dispose();
                _cmdUpdateSysFunction = null;
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
        public PageEntity<SysFunction> Search(Int32 IdsysFunction, String FunctionName, Int32 FatherId, String Mark, String FunctionUrl, String FunctionCode, Int32 Level, int pageIndex, int pageSize)
        {
            PageEntity<SysFunction> returnValue = new PageEntity<SysFunction>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysFunction = cmdLoadSysFunction.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysFunctionCount = cmdGetSysFunctionCount.Clone() as MySqlCommand;
            _cmdLoadSysFunction.Connection = oc;
            _cmdGetSysFunctionCount.Connection = oc;

            try
            {
                _cmdLoadSysFunction.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysFunction.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysFunction.Parameters["@IdsysFunction"].Value = IdsysFunction;
                _cmdLoadSysFunction.Parameters["@FunctionName"].Value = FunctionName;
                _cmdLoadSysFunction.Parameters["@FatherId"].Value = FatherId;
                _cmdLoadSysFunction.Parameters["@Mark"].Value = Mark;
                _cmdLoadSysFunction.Parameters["@FunctionUrl"].Value = FunctionUrl;
                _cmdLoadSysFunction.Parameters["@FunctionCode"].Value = FunctionCode;
                _cmdLoadSysFunction.Parameters["@Level"].Value = Level;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysFunction.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysFunction().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetSysFunctionCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysFunction.Dispose();
                _cmdLoadSysFunction = null;
                _cmdGetSysFunctionCount.Dispose();
                _cmdGetSysFunctionCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysFunction> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysFunction = cmdLoadAllSysFunction.Clone() as MySqlCommand;
            _cmdLoadAllSysFunction.Connection = oc; List<SysFunction> returnValue = new List<SysFunction>();
            try
            {
                _cmdLoadAllSysFunction.CommandText = string.Format(_cmdLoadAllSysFunction.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysFunction.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysFunction().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysFunction.Dispose();
                _cmdLoadAllSysFunction = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysFunction Get(int IdsysFunction)
        {
            SysFunction returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysFunction = cmdGetSysFunction.Clone() as MySqlCommand;

            _cmdGetSysFunction.Connection = oc;
            try
            {
                _cmdGetSysFunction.Parameters["@IdsysFunction"].Value = IdsysFunction;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysFunction.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysFunction().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysFunction.Dispose();
                _cmdGetSysFunction = null;
                GC.Collect();
            }
            return returnValue;

        }
        /// <summary>
        /// 加载某个用户所有权限列表（包括所有级）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<SysFunction> SearchSysUserRolePermisson(int userId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysUserRolePermisson = cmdGetSysUserRolePermisson.Clone() as MySqlCommand;
            _cmdGetSysUserRolePermisson.Connection = oc;
            List<SysFunction> returnValue = new List<SysFunction>();
            try
            {
                
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysUserRolePermisson.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysFunction().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysUserRolePermisson.Dispose();
                _cmdGetSysUserRolePermisson = null;
                GC.Collect();
            }
            return returnValue;
        }
        private static readonly SysFunctionAccessor instance = new SysFunctionAccessor();
        public static SysFunctionAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
