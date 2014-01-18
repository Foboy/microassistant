/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/12/28 12:31:46
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
    public class SysUserExtraAccessor
    {
        private MySqlCommand cmdInsertSysUserExtra;
        private MySqlCommand cmdDeleteSysUserExtra;
        private MySqlCommand cmdUpdateSysUserExtra;
        private MySqlCommand cmdLoadSysUserExtra;
        private MySqlCommand cmdLoadAllSysUserExtra;
        private MySqlCommand cmdGetSysUserExtraCount;
        private MySqlCommand cmdGetSysUserExtra;

        private SysUserExtraAccessor()
        {
            #region cmdInsertSysUserExtra

            cmdInsertSysUserExtra = new MySqlCommand("INSERT INTO sys_user_extra(diploma,school,major,graduation_time,detail,sys_user_id) values (@Diploma,@School,@Major,@GraduationTime,@Detail,@SysUserId)");

            cmdInsertSysUserExtra.Parameters.Add("@Diploma", MySqlDbType.String);
            cmdInsertSysUserExtra.Parameters.Add("@School", MySqlDbType.String);
            cmdInsertSysUserExtra.Parameters.Add("@Major", MySqlDbType.String);
            cmdInsertSysUserExtra.Parameters.Add("@GraduationTime", MySqlDbType.DateTime);
            cmdInsertSysUserExtra.Parameters.Add("@Detail", MySqlDbType.String);
            cmdInsertSysUserExtra.Parameters.Add("@SysUserId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateSysUserExtra

            cmdUpdateSysUserExtra = new MySqlCommand(" update sys_user_extra set diploma = @Diploma,school = @School,major = @Major,graduation_time = @GraduationTime,detail = @Detail where sys_user_id = @SysUserId");
            cmdUpdateSysUserExtra.Parameters.Add("@Diploma", MySqlDbType.String);
            cmdUpdateSysUserExtra.Parameters.Add("@School", MySqlDbType.String);
            cmdUpdateSysUserExtra.Parameters.Add("@Major", MySqlDbType.String);
            cmdUpdateSysUserExtra.Parameters.Add("@GraduationTime", MySqlDbType.DateTime);
            cmdUpdateSysUserExtra.Parameters.Add("@Detail", MySqlDbType.String);
            cmdUpdateSysUserExtra.Parameters.Add("@SysUserId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteSysUserExtra

            cmdDeleteSysUserExtra = new MySqlCommand(" delete from sys_user_extra where sys_user_id = @SysUserId");
            cmdDeleteSysUserExtra.Parameters.Add("@SysUserId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysUserExtra

            cmdLoadSysUserExtra = new MySqlCommand(@" select idsys_user_extra,diploma,school,major,graduation_time,detail,sys_user_id from sys_user_extra limit @PageIndex,@PageSize");
            cmdLoadSysUserExtra.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysUserExtra.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysUserExtraCount

            cmdGetSysUserExtraCount = new MySqlCommand(" select count(*)  from sys_user_extra ");

            #endregion

            #region cmdLoadAllSysUserExtra

            cmdLoadAllSysUserExtra = new MySqlCommand(" select idsys_user_extra,diploma,school,major,graduation_time,detail,sys_user_id from sys_user_extra");

            #endregion

            #region cmdGetSysUserExtra

            cmdGetSysUserExtra = new MySqlCommand(" select idsys_user_extra,diploma,school,major,graduation_time,detail,sys_user_id from sys_user_extra where sys_user_id = @UserId");
            cmdGetSysUserExtra.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(SysUserExtra e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysUserExtra = cmdInsertSysUserExtra.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertSysUserExtra.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysUserExtra.Parameters["@Diploma"].Value = e.Diploma;
                _cmdInsertSysUserExtra.Parameters["@School"].Value = e.School;
                _cmdInsertSysUserExtra.Parameters["@Major"].Value = e.Major;
                _cmdInsertSysUserExtra.Parameters["@GraduationTime"].Value = e.GraduationTime;
                _cmdInsertSysUserExtra.Parameters["@Detail"].Value = e.Detail;
                _cmdInsertSysUserExtra.Parameters["@SysUserId"].Value = e.SysUserId;

                _cmdInsertSysUserExtra.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertSysUserExtra.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysUserExtra.Dispose();
                _cmdInsertSysUserExtra = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int SysUserId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysUserExtra = cmdDeleteSysUserExtra.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysUserExtra.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysUserExtra.Parameters["@SysUserId"].Value = SysUserId;


                _cmdDeleteSysUserExtra.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysUserExtra.Dispose();
                _cmdDeleteSysUserExtra = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysUserExtra e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysUserExtra = cmdUpdateSysUserExtra.Clone() as MySqlCommand;
            _cmdUpdateSysUserExtra.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysUserExtra.Parameters["@Diploma"].Value = e.Diploma;
                _cmdUpdateSysUserExtra.Parameters["@School"].Value = e.School;
                _cmdUpdateSysUserExtra.Parameters["@Major"].Value = e.Major;
                _cmdUpdateSysUserExtra.Parameters["@GraduationTime"].Value = e.GraduationTime;
                _cmdUpdateSysUserExtra.Parameters["@Detail"].Value = e.Detail;
                _cmdUpdateSysUserExtra.Parameters["@SysUserId"].Value = e.SysUserId;

                _cmdUpdateSysUserExtra.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysUserExtra.Dispose();
                _cmdUpdateSysUserExtra = null;
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
        public PageEntity<SysUserExtra> Search(Int32 IdsysUserExtra, String Diploma, String School, String Major, DateTime GraduationTime, String Detail, Int32 SysUserId, int pageIndex, int pageSize)
        {
            PageEntity<SysUserExtra> returnValue = new PageEntity<SysUserExtra>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysUserExtra = cmdLoadSysUserExtra.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysUserExtraCount = cmdGetSysUserExtraCount.Clone() as MySqlCommand;
            _cmdLoadSysUserExtra.Connection = oc;
            _cmdGetSysUserExtraCount.Connection = oc;

            try
            {
                _cmdLoadSysUserExtra.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysUserExtra.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysUserExtra.Parameters["@IdsysUserExtra"].Value = IdsysUserExtra;
                _cmdLoadSysUserExtra.Parameters["@Diploma"].Value = Diploma;
                _cmdLoadSysUserExtra.Parameters["@School"].Value = School;
                _cmdLoadSysUserExtra.Parameters["@Major"].Value = Major;
                _cmdLoadSysUserExtra.Parameters["@GraduationTime"].Value = GraduationTime;
                _cmdLoadSysUserExtra.Parameters["@Detail"].Value = Detail;
                _cmdLoadSysUserExtra.Parameters["@SysUserId"].Value = SysUserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysUserExtra.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysUserExtra().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetSysUserExtraCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysUserExtra.Dispose();
                _cmdLoadSysUserExtra = null;
                _cmdGetSysUserExtraCount.Dispose();
                _cmdGetSysUserExtraCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysUserExtra> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysUserExtra = cmdLoadAllSysUserExtra.Clone() as MySqlCommand;
            _cmdLoadAllSysUserExtra.Connection = oc; List<SysUserExtra> returnValue = new List<SysUserExtra>();
            try
            {
                _cmdLoadAllSysUserExtra.CommandText = string.Format(_cmdLoadAllSysUserExtra.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysUserExtra.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysUserExtra().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysUserExtra.Dispose();
                _cmdLoadAllSysUserExtra = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysUserExtra Get(int userid)
        {
            SysUserExtra returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysUserExtra = cmdGetSysUserExtra.Clone() as MySqlCommand;

            _cmdGetSysUserExtra.Connection = oc;
            try
            {
                _cmdGetSysUserExtra.Parameters["@UserId"].Value = userid;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysUserExtra.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysUserExtra().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysUserExtra.Dispose();
                _cmdGetSysUserExtra = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysUserExtraAccessor instance = new SysUserExtraAccessor();
        public static SysUserExtraAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
