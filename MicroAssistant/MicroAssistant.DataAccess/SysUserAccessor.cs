/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/15 10:34:54
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
    public class SysUserAccessor
    {
        private MySqlCommand cmdInsertSysUser;
        private MySqlCommand cmdDeleteSysUser;
        private MySqlCommand cmdUpdateSysUser;
        private MySqlCommand cmdLoadSysUser;
        private MySqlCommand cmdLoadAllSysUser;
        private MySqlCommand cmdGetSysUserCount;
        private MySqlCommand cmdGetSysUser;
        private MySqlCommand cmdGetSysUserByAcount;
        private MySqlCommand cmdGetSysUserByAcountAndPwd;
        private MySqlCommand cmdUpdateSysUserEntId;

        private SysUserAccessor()
        {
            #region cmdInsertSysUser

            cmdInsertSysUser = new MySqlCommand("INSERT INTO sys_user(user_name,user_account,pwd,mobile,email,create_time,end_time,ent_admin_id,is_enable,type,ent_id) values (@UserName,@UserAccount,@Pwd,@Mobile,@Email,@CreateTime,@EndTime,@EntAdminId,@IsEnable,@Type,@EntId)");

            cmdInsertSysUser.Parameters.Add("@UserName", MySqlDbType.String);
            cmdInsertSysUser.Parameters.Add("@UserAccount", MySqlDbType.String);
            cmdInsertSysUser.Parameters.Add("@Pwd", MySqlDbType.String);
            cmdInsertSysUser.Parameters.Add("@Mobile", MySqlDbType.String);
            cmdInsertSysUser.Parameters.Add("@Email", MySqlDbType.String);
            cmdInsertSysUser.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdInsertSysUser.Parameters.Add("@EndTime", MySqlDbType.DateTime);
            cmdInsertSysUser.Parameters.Add("@EntAdminId", MySqlDbType.Int32);
            cmdInsertSysUser.Parameters.Add("@IsEnable", MySqlDbType.Int32);
            cmdInsertSysUser.Parameters.Add("@Type", MySqlDbType.Int32);
            cmdInsertSysUser.Parameters.Add("@EntId", MySqlDbType.Int32);
            #endregion
            #region cmdUpdateSysUserEntId

            cmdUpdateSysUserEntId = new MySqlCommand(" update sys_user set ent_id = @EntId where user_id = @UserId");
            cmdUpdateSysUserEntId.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateSysUserEntId.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion

            #region cmdUpdateSysUser

            cmdUpdateSysUser = new MySqlCommand(" update sys_user set user_name = @UserName,user_account = @UserAccount,pwd = @Pwd,mobile = @Mobile,email = @Email,create_time = @CreateTime,end_time = @EndTime,ent_admin_id = @EntAdminId,is_enable = @IsEnable,type = @Type,ent_id = @EntId where user_id = @UserId");
            cmdUpdateSysUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateSysUser.Parameters.Add("@UserName", MySqlDbType.String);
            cmdUpdateSysUser.Parameters.Add("@UserAccount", MySqlDbType.String);
            cmdUpdateSysUser.Parameters.Add("@Pwd", MySqlDbType.String);
            cmdUpdateSysUser.Parameters.Add("@Mobile", MySqlDbType.String);
            cmdUpdateSysUser.Parameters.Add("@Email", MySqlDbType.String);
            cmdUpdateSysUser.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdUpdateSysUser.Parameters.Add("@EndTime", MySqlDbType.DateTime);
            cmdUpdateSysUser.Parameters.Add("@EntAdminId", MySqlDbType.Int32);
            cmdUpdateSysUser.Parameters.Add("@IsEnable", MySqlDbType.Int32);
            cmdUpdateSysUser.Parameters.Add("@Type", MySqlDbType.Int32);
            cmdUpdateSysUser.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteSysUser

            cmdDeleteSysUser = new MySqlCommand(" delete from sys_user where user_id = @UserId");
            cmdDeleteSysUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysUser

            cmdLoadSysUser = new MySqlCommand(@" select user_id,user_name,user_account,pwd,mobile,email,create_time,end_time,ent_admin_id,is_enable,type,ent_id from sys_user limit @PageIndex,@PageSize");
            cmdLoadSysUser.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysUser.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysUserCount

            cmdGetSysUserCount = new MySqlCommand(" select count(*)  from sys_user ");

            #endregion

            #region cmdLoadAllSysUser

            cmdLoadAllSysUser = new MySqlCommand(" select user_id,user_name,user_account,pwd,mobile,email,create_time,end_time,ent_admin_id,is_enable,type,ent_id from sys_user");

            #endregion

            #region cmdGetSysUser

            cmdGetSysUser = new MySqlCommand(" select user_id,user_name,user_account,pwd,mobile,email,create_time,end_time,ent_admin_id,is_enable,type,ent_id from sys_user where user_id = @UserId");
            cmdGetSysUser.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysUserByAcount

            cmdGetSysUserByAcount = new MySqlCommand(" select user_id,user_name,user_account,pwd,mobile,email,create_time,end_time,ent_admin_id,is_enable,type,ent_id from sys_user where user_account = @UserAccount");
            cmdGetSysUserByAcount.Parameters.Add("@UserAccount", MySqlDbType.String);

            #endregion

            #region cmdGetSysUserByAcountAndPwd

            cmdGetSysUserByAcountAndPwd = new MySqlCommand("  select user_id,user_name,user_account,pwd,mobile,email,create_time,end_time,ent_admin_id,is_enable,type,ent_id from sys_user where user_account = @UserAccount and pwd = @Pwd and is_enable=1");
            cmdGetSysUserByAcountAndPwd.Parameters.Add("@UserAccount", MySqlDbType.String);
            cmdGetSysUserByAcountAndPwd.Parameters.Add("@Pwd", MySqlDbType.String);

            #endregion
        }
        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(SysUser e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysUser = cmdInsertSysUser.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertSysUser.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysUser.Parameters["@UserName"].Value = e.UserName;
                _cmdInsertSysUser.Parameters["@UserAccount"].Value = e.UserAccount;
                _cmdInsertSysUser.Parameters["@Pwd"].Value = e.Pwd;
                _cmdInsertSysUser.Parameters["@Mobile"].Value = e.Mobile;
                _cmdInsertSysUser.Parameters["@Email"].Value = e.Email;
                _cmdInsertSysUser.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdInsertSysUser.Parameters["@EndTime"].Value = e.EndTime;
                _cmdInsertSysUser.Parameters["@EntAdminId"].Value = e.EntAdminId;
                _cmdInsertSysUser.Parameters["@IsEnable"].Value = e.IsEnable;
                _cmdInsertSysUser.Parameters["@Type"].Value = e.Type;
                _cmdInsertSysUser.Parameters["@EntId"].Value = e.EntId;

                _cmdInsertSysUser.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertSysUser.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysUser.Dispose();
                _cmdInsertSysUser = null;
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
            MySqlCommand _cmdDeleteSysUser = cmdDeleteSysUser.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysUser.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysUser.Parameters["@UserId"].Value = UserId;


                _cmdDeleteSysUser.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysUser.Dispose();
                _cmdDeleteSysUser = null;
            }
        }

/// <summary>
/// 修改用户所属企业ID
/// </summary>
/// <param name="entid"></param>
/// <param name="userid"></param>
        public void UpdateUserEntId(int entid,int userid)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysUserEntId = cmdUpdateSysUserEntId.Clone() as MySqlCommand;
            _cmdUpdateSysUserEntId.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysUserEntId.Parameters["@UserId"].Value = userid;
                _cmdUpdateSysUserEntId.Parameters["@EntId"].Value = entid;

                _cmdUpdateSysUserEntId.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysUserEntId.Dispose();
                _cmdUpdateSysUserEntId = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysUser e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysUser = cmdUpdateSysUser.Clone() as MySqlCommand;
            _cmdUpdateSysUser.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysUser.Parameters["@UserId"].Value = e.UserId;
                _cmdUpdateSysUser.Parameters["@UserName"].Value = e.UserName;
                _cmdUpdateSysUser.Parameters["@UserAccount"].Value = e.UserAccount;
                _cmdUpdateSysUser.Parameters["@Pwd"].Value = e.Pwd;
                _cmdUpdateSysUser.Parameters["@Mobile"].Value = e.Mobile;
                _cmdUpdateSysUser.Parameters["@Email"].Value = e.Email;
                _cmdUpdateSysUser.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdUpdateSysUser.Parameters["@EndTime"].Value = e.EndTime;
                _cmdUpdateSysUser.Parameters["@EntAdminId"].Value = e.EntAdminId;
                _cmdUpdateSysUser.Parameters["@IsEnable"].Value = e.IsEnable;
                _cmdUpdateSysUser.Parameters["@Type"].Value = e.Type;
                _cmdUpdateSysUser.Parameters["@EntId"].Value = e.EntId;

                _cmdUpdateSysUser.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysUser.Dispose();
                _cmdUpdateSysUser = null;
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
        public PageEntity<SysUser> Search(Int32 UserId, String UserName, String UserAccount, String Pwd, String Mobile, String Email, DateTime CreateTime, DateTime EndTime, Int32 EntAdminId, Int32 IsEnable, Int32 Type, Int32 EntId, int pageIndex, int pageSize)
        {
            PageEntity<SysUser> returnValue = new PageEntity<SysUser>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysUser = cmdLoadSysUser.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysUserCount = cmdGetSysUserCount.Clone() as MySqlCommand;
            _cmdLoadSysUser.Connection = oc;
            _cmdGetSysUserCount.Connection = oc;

            try
            {
                _cmdLoadSysUser.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysUser.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysUser.Parameters["@UserId"].Value = UserId;
                _cmdLoadSysUser.Parameters["@UserName"].Value = UserName;
                _cmdLoadSysUser.Parameters["@UserAccount"].Value = UserAccount;
                _cmdLoadSysUser.Parameters["@Pwd"].Value = Pwd;
                _cmdLoadSysUser.Parameters["@Mobile"].Value = Mobile;
                _cmdLoadSysUser.Parameters["@Email"].Value = Email;
                _cmdLoadSysUser.Parameters["@CreateTime"].Value = CreateTime;
                _cmdLoadSysUser.Parameters["@EndTime"].Value = EndTime;
                _cmdLoadSysUser.Parameters["@EntAdminId"].Value = EntAdminId;
                _cmdLoadSysUser.Parameters["@IsEnable"].Value = IsEnable;
                _cmdLoadSysUser.Parameters["@Type"].Value = Type;
                _cmdLoadSysUser.Parameters["@EntId"].Value = EntId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysUser.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysUser().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetSysUserCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysUser.Dispose();
                _cmdLoadSysUser = null;
                _cmdGetSysUserCount.Dispose();
                _cmdGetSysUserCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysUser> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysUser = cmdLoadAllSysUser.Clone() as MySqlCommand;
            _cmdLoadAllSysUser.Connection = oc; List<SysUser> returnValue = new List<SysUser>();
            try
            {
                _cmdLoadAllSysUser.CommandText = string.Format(_cmdLoadAllSysUser.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysUser.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysUser().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysUser.Dispose();
                _cmdLoadAllSysUser = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysUser Get(int UserId)
        {
            SysUser returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysUser = cmdGetSysUser.Clone() as MySqlCommand;

            _cmdGetSysUser.Connection = oc;
            try
            {
                _cmdGetSysUser.Parameters["@UserId"].Value = UserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysUser.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysUser().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysUser.Dispose();
                _cmdGetSysUser = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 通过账户获取用户
        /// </summary>
        public SysUser GetSysUserByAcount(string UserAccount)
        {
            SysUser returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysUserByAcount = cmdGetSysUserByAcount.Clone() as MySqlCommand;

            _cmdGetSysUserByAcount.Connection = oc;
            try
            {
                _cmdGetSysUserByAcount.Parameters["@UserAccount"].Value = UserAccount;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysUserByAcount.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysUser().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysUserByAcount.Dispose();
                _cmdGetSysUserByAcount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 通过账号密码获取用户
        /// </summary>
        public SysUser GetSysUserByAcountAndPwd(string UserAccount,string Pwd)
        {
            SysUser returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysUserByAcountAndPwd = cmdGetSysUserByAcountAndPwd.Clone() as MySqlCommand;

            _cmdGetSysUserByAcountAndPwd.Connection = oc;
            try
            {
                _cmdGetSysUserByAcountAndPwd.Parameters["@UserAccount"].Value = UserAccount;
                _cmdGetSysUserByAcountAndPwd.Parameters["@Pwd"].Value = Pwd;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysUserByAcountAndPwd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysUser().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysUserByAcountAndPwd.Dispose();
                _cmdGetSysUserByAcountAndPwd = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysUserAccessor instance = new SysUserAccessor();
        public static SysUserAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
