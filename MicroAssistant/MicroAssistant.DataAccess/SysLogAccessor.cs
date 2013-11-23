/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/11/23 15:30:27
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
    public class SysLogAccessor
    {
        private MySqlCommand cmdInsertSysLog;
        private MySqlCommand cmdDeleteSysLog;
        private MySqlCommand cmdUpdateSysLog;
        private MySqlCommand cmdLoadSysLog;
        private MySqlCommand cmdLoadAllSysLog;
        private MySqlCommand cmdGetSysLogCount;
        private MySqlCommand cmdGetSysLog;

        private SysLogAccessor()
        {
            #region cmdInsertSysLog

            cmdInsertSysLog = new MySqlCommand("INSERT INTO sys_log(user_id,add_time,action,parameter,result) values (@UserId,@AddTime,@Action,@Parameter,@Result)");

            cmdInsertSysLog.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdInsertSysLog.Parameters.Add("@AddTime", MySqlDbType.DateTime);
            cmdInsertSysLog.Parameters.Add("@Action", MySqlDbType.String);
            cmdInsertSysLog.Parameters.Add("@Parameter", MySqlDbType.String);
            cmdInsertSysLog.Parameters.Add("@Result", MySqlDbType.String);
            #endregion

            #region cmdUpdateSysLog

            cmdUpdateSysLog = new MySqlCommand(" update sys_log set user_id = @UserId,add_time = @AddTime,action = @Action,parameter = @Parameter,result = @Result where idsys_log = @IdsysLog");
            cmdUpdateSysLog.Parameters.Add("@IdsysLog", MySqlDbType.Int32);
            cmdUpdateSysLog.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateSysLog.Parameters.Add("@AddTime", MySqlDbType.DateTime);
            cmdUpdateSysLog.Parameters.Add("@Action", MySqlDbType.String);
            cmdUpdateSysLog.Parameters.Add("@Parameter", MySqlDbType.String);
            cmdUpdateSysLog.Parameters.Add("@Result", MySqlDbType.String);

            #endregion

            #region cmdDeleteSysLog

            cmdDeleteSysLog = new MySqlCommand(" delete from sys_log where idsys_log = @IdsysLog");
            cmdDeleteSysLog.Parameters.Add("@IdsysLog", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysLog

            cmdLoadSysLog = new MySqlCommand(@" select idsys_log,user_id,add_time,action,parameter,result from sys_log limit @PageIndex,@PageSize");
            cmdLoadSysLog.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysLog.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysLogCount

            cmdGetSysLogCount = new MySqlCommand(" select count(*)  from sys_log ");

            #endregion

            #region cmdLoadAllSysLog

            cmdLoadAllSysLog = new MySqlCommand(" select idsys_log,user_id,add_time,action,parameter,result from sys_log");

            #endregion

            #region cmdGetSysLog

            cmdGetSysLog = new MySqlCommand(" select idsys_log,user_id,add_time,action,parameter,result from sys_log where idsys_log = @IdsysLog");
            cmdGetSysLog.Parameters.Add("@IdsysLog", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(SysLog e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysLog = cmdInsertSysLog.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertSysLog.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysLog.Parameters["@UserId"].Value = e.UserId;
                _cmdInsertSysLog.Parameters["@AddTime"].Value = e.AddTime;
                _cmdInsertSysLog.Parameters["@Action"].Value = e.Action;
                _cmdInsertSysLog.Parameters["@Parameter"].Value = e.Parameter;
                _cmdInsertSysLog.Parameters["@Result"].Value = e.Result;

                _cmdInsertSysLog.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertSysLog.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysLog.Dispose();
                _cmdInsertSysLog = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int IdsysLog)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysLog = cmdDeleteSysLog.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysLog.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysLog.Parameters["@IdsysLog"].Value = IdsysLog;


                _cmdDeleteSysLog.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysLog.Dispose();
                _cmdDeleteSysLog = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysLog e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysLog = cmdUpdateSysLog.Clone() as MySqlCommand;
            _cmdUpdateSysLog.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysLog.Parameters["@IdsysLog"].Value = e.IdsysLog;
                _cmdUpdateSysLog.Parameters["@UserId"].Value = e.UserId;
                _cmdUpdateSysLog.Parameters["@AddTime"].Value = e.AddTime;
                _cmdUpdateSysLog.Parameters["@Action"].Value = e.Action;
                _cmdUpdateSysLog.Parameters["@Parameter"].Value = e.Parameter;
                _cmdUpdateSysLog.Parameters["@Result"].Value = e.Result;

                _cmdUpdateSysLog.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysLog.Dispose();
                _cmdUpdateSysLog = null;
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
        public PageEntity<SysLog> Search(Int32 IdsysLog, Int32 UserId, DateTime AddTime, String Action, String Parameter, String Result, int pageIndex, int pageSize)
        {
            PageEntity<SysLog> returnValue = new PageEntity<SysLog>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysLog = cmdLoadSysLog.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysLogCount = cmdGetSysLogCount.Clone() as MySqlCommand;
            _cmdLoadSysLog.Connection = oc;
            _cmdGetSysLogCount.Connection = oc;

            try
            {
                _cmdLoadSysLog.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysLog.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysLog.Parameters["@IdsysLog"].Value = IdsysLog;
                _cmdLoadSysLog.Parameters["@UserId"].Value = UserId;
                _cmdLoadSysLog.Parameters["@AddTime"].Value = AddTime;
                _cmdLoadSysLog.Parameters["@Action"].Value = Action;
                _cmdLoadSysLog.Parameters["@Parameter"].Value = Parameter;
                _cmdLoadSysLog.Parameters["@Result"].Value = Result;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysLog.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysLog().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetSysLogCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysLog.Dispose();
                _cmdLoadSysLog = null;
                _cmdGetSysLogCount.Dispose();
                _cmdGetSysLogCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysLog> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysLog = cmdLoadAllSysLog.Clone() as MySqlCommand;
            _cmdLoadAllSysLog.Connection = oc; List<SysLog> returnValue = new List<SysLog>();
            try
            {
                _cmdLoadAllSysLog.CommandText = string.Format(_cmdLoadAllSysLog.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysLog.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysLog().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysLog.Dispose();
                _cmdLoadAllSysLog = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysLog Get(int IdsysLog)
        {
            SysLog returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysLog = cmdGetSysLog.Clone() as MySqlCommand;

            _cmdGetSysLog.Connection = oc;
            try
            {
                _cmdGetSysLog.Parameters["@IdsysLog"].Value = IdsysLog;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysLog.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysLog().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysLog.Dispose();
                _cmdGetSysLog = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysLogAccessor instance = new SysLogAccessor();
        public static SysLogAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
