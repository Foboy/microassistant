/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/11/4 0:06:04
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
    public class SysUserTimemachineAccessor
    {
        private MySqlCommand cmdInsertSysUserTimemachine;
        private MySqlCommand cmdDeleteSysUserTimemachine;
        private MySqlCommand cmdUpdateSysUserTimemachine;
        private MySqlCommand cmdLoadSysUserTimemachine;
        private MySqlCommand cmdLoadAllSysUserTimemachine;
        private MySqlCommand cmdGetSysUserTimemachineCount;
        private MySqlCommand cmdGetSysUserTimemachine;

        private SysUserTimemachineAccessor()
        {
            #region cmdInsertSysUserTimemachine

            cmdInsertSysUserTimemachine = new MySqlCommand("INSERT INTO sys_user_timemachine(user_id,user_name,role_name,ent_name,ent_id,start_time) values (@UserId,@UserName,@RoleName,@EntName,@EntId,@StartTime)");

            cmdInsertSysUserTimemachine.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdInsertSysUserTimemachine.Parameters.Add("@UserName", MySqlDbType.String);
            cmdInsertSysUserTimemachine.Parameters.Add("@RoleName", MySqlDbType.String);
            cmdInsertSysUserTimemachine.Parameters.Add("@EntName", MySqlDbType.String);
            cmdInsertSysUserTimemachine.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertSysUserTimemachine.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            #endregion

            #region cmdUpdateSysUserTimemachine

            cmdUpdateSysUserTimemachine = new MySqlCommand(" update sys_user_timemachine set user_id = @UserId,user_name = @UserName,role_name = @RoleName,ent_name = @EntName,start_time = @StartTime,end_time = @EndTime where user_id = @UserId and ent_id = @EntId");
            
            cmdUpdateSysUserTimemachine.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateSysUserTimemachine.Parameters.Add("@UserName", MySqlDbType.String);
            cmdUpdateSysUserTimemachine.Parameters.Add("@RoleName", MySqlDbType.String);
            cmdUpdateSysUserTimemachine.Parameters.Add("@EntName", MySqlDbType.String);
            cmdUpdateSysUserTimemachine.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateSysUserTimemachine.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdUpdateSysUserTimemachine.Parameters.Add("@EndTime", MySqlDbType.DateTime);

            #endregion

            #region cmdDeleteSysUserTimemachine

            cmdDeleteSysUserTimemachine = new MySqlCommand(" delete from sys_user_timemachine where idsys_user_timemachine = @IdsysUserTimemachine");
            cmdDeleteSysUserTimemachine.Parameters.Add("@IdsysUserTimemachine", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysUserTimemachine

            cmdLoadSysUserTimemachine = new MySqlCommand(@" select idsys_user_timemachine,user_id,user_name,role_name,ent_name,ent_id,start_time,end_time from sys_user_timemachine where user_id=@UserId order by start_time desc");
            cmdLoadSysUserTimemachine.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysUserTimemachineCount

            cmdGetSysUserTimemachineCount = new MySqlCommand(" select count(*)  from sys_user_timemachine ");

            #endregion

            #region cmdLoadAllSysUserTimemachine

            cmdLoadAllSysUserTimemachine = new MySqlCommand(" select idsys_user_timemachine,user_id,user_name,role_name,ent_name,ent_id,start_time,end_time from sys_user_timemachine");

            #endregion

            #region cmdGetSysUserTimemachine

            cmdGetSysUserTimemachine = new MySqlCommand(" select idsys_user_timemachine,user_id,user_name,role_name,ent_name,ent_id,start_time,end_time from sys_user_timemachine where user_id = @UserId and ent_id = @EntId");
            cmdGetSysUserTimemachine.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdGetSysUserTimemachine.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(SysUserTimemachine e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysUserTimemachine = cmdInsertSysUserTimemachine.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertSysUserTimemachine.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysUserTimemachine.Parameters["@UserId"].Value = e.UserId;
                _cmdInsertSysUserTimemachine.Parameters["@UserName"].Value = e.UserName;
                _cmdInsertSysUserTimemachine.Parameters["@RoleName"].Value = e.RoleName;
                _cmdInsertSysUserTimemachine.Parameters["@EntName"].Value = e.EntName;
                _cmdInsertSysUserTimemachine.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertSysUserTimemachine.Parameters["@StartTime"].Value = e.StartTime;

                _cmdInsertSysUserTimemachine.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertSysUserTimemachine.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysUserTimemachine.Dispose();
                _cmdInsertSysUserTimemachine = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int IdsysUserTimemachine)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysUserTimemachine = cmdDeleteSysUserTimemachine.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysUserTimemachine.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysUserTimemachine.Parameters["@IdsysUserTimemachine"].Value = IdsysUserTimemachine;


                _cmdDeleteSysUserTimemachine.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysUserTimemachine.Dispose();
                _cmdDeleteSysUserTimemachine = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysUserTimemachine e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysUserTimemachine = cmdUpdateSysUserTimemachine.Clone() as MySqlCommand;
            _cmdUpdateSysUserTimemachine.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysUserTimemachine.Parameters["@IdsysUserTimemachine"].Value = e.IdsysUserTimemachine;
                _cmdUpdateSysUserTimemachine.Parameters["@UserId"].Value = e.UserId;
                _cmdUpdateSysUserTimemachine.Parameters["@UserName"].Value = e.UserName;
                _cmdUpdateSysUserTimemachine.Parameters["@RoleName"].Value = e.RoleName;
                _cmdUpdateSysUserTimemachine.Parameters["@EntName"].Value = e.EntName;
                _cmdUpdateSysUserTimemachine.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateSysUserTimemachine.Parameters["@StartTime"].Value = e.StartTime;
                _cmdUpdateSysUserTimemachine.Parameters["@EndTime"].Value = e.EndTime;

                _cmdUpdateSysUserTimemachine.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysUserTimemachine.Dispose();
                _cmdUpdateSysUserTimemachine = null;
                GC.Collect();
            }
        }

        
        public List<SysUserTimemachine> Search(Int32 UserId)
        {
            List<SysUserTimemachine> returnValue = new List<SysUserTimemachine>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysUserTimemachine = cmdLoadSysUserTimemachine.Clone() as MySqlCommand;
            _cmdLoadSysUserTimemachine.Connection = oc;

            try
            {
                _cmdLoadSysUserTimemachine.Parameters["@UserId"].Value = UserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysUserTimemachine.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysUserTimemachine().BuildSampleEntity(reader));
                }
              
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysUserTimemachine.Dispose();
                _cmdLoadSysUserTimemachine = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysUserTimemachine> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysUserTimemachine = cmdLoadAllSysUserTimemachine.Clone() as MySqlCommand;
            _cmdLoadAllSysUserTimemachine.Connection = oc; List<SysUserTimemachine> returnValue = new List<SysUserTimemachine>();
            try
            {
                _cmdLoadAllSysUserTimemachine.CommandText = string.Format(_cmdLoadAllSysUserTimemachine.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysUserTimemachine.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysUserTimemachine().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysUserTimemachine.Dispose();
                _cmdLoadAllSysUserTimemachine = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysUserTimemachine Get(int userid,int entid)
        {
            SysUserTimemachine returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysUserTimemachine = cmdGetSysUserTimemachine.Clone() as MySqlCommand;

            _cmdGetSysUserTimemachine.Connection = oc;
            try
            {
                _cmdGetSysUserTimemachine.Parameters["@UserId"].Value = userid;
                _cmdGetSysUserTimemachine.Parameters["@EntId"].Value = entid;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysUserTimemachine.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysUserTimemachine().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysUserTimemachine.Dispose();
                _cmdGetSysUserTimemachine = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysUserTimemachineAccessor instance = new SysUserTimemachineAccessor();
        public static SysUserTimemachineAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
