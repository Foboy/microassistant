/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/12/28 12:32:38
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
    public class SysEntExtraAccessor
    {
        private MySqlCommand cmdInsertSysEntExtra;
        private MySqlCommand cmdDeleteSysEntExtra;
        private MySqlCommand cmdUpdateSysEntExtra;
        private MySqlCommand cmdLoadSysEntExtra;
        private MySqlCommand cmdLoadAllSysEntExtra;
        private MySqlCommand cmdGetSysEntExtraCount;
        private MySqlCommand cmdGetSysEntExtra;

        private SysEntExtraAccessor()
        {
            #region cmdInsertSysEntExtra

            cmdInsertSysEntExtra = new MySqlCommand("INSERT INTO sys_ent_extra(ent_id,artificial_person,registered_capital,date_of_establishment,address,province,city,contact_phone,web,weibo,weixin,main_business) values (@EntId,@ArtificialPerson,@RegisteredCapital,@DateOfEstablishment,@Address,@Province,@City,@ContactPhone,@Web,@Weibo,@Weixin,@MainBusiness)");

            cmdInsertSysEntExtra.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertSysEntExtra.Parameters.Add("@ArtificialPerson", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@RegisteredCapital", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@DateOfEstablishment", MySqlDbType.DateTime);
            cmdInsertSysEntExtra.Parameters.Add("@Address", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@Province", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@City", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@ContactPhone", MySqlDbType.Int32);
            cmdInsertSysEntExtra.Parameters.Add("@Web", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@Weibo", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@Weixin", MySqlDbType.String);
            cmdInsertSysEntExtra.Parameters.Add("@MainBusiness", MySqlDbType.String);
            #endregion

            #region cmdUpdateSysEntExtra

            cmdUpdateSysEntExtra = new MySqlCommand(" update sys_ent_extra set artificial_person = @ArtificialPerson,registered_capital = @RegisteredCapital,date_of_establishment = @DateOfEstablishment,address = @Address,province = @Province,city = @City,contact_phone = @ContactPhone,web = @Web,weibo = @Weibo,weixin = @Weixin,main_business = @MainBusiness where ent_id = @EntId");
        
            cmdUpdateSysEntExtra.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateSysEntExtra.Parameters.Add("@ArtificialPerson", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@RegisteredCapital", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@DateOfEstablishment", MySqlDbType.DateTime);
            cmdUpdateSysEntExtra.Parameters.Add("@Address", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@Province", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@City", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@ContactPhone", MySqlDbType.Int32);
            cmdUpdateSysEntExtra.Parameters.Add("@Web", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@Weibo", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@Weixin", MySqlDbType.String);
            cmdUpdateSysEntExtra.Parameters.Add("@MainBusiness", MySqlDbType.String);

            #endregion

            #region cmdDeleteSysEntExtra

            cmdDeleteSysEntExtra = new MySqlCommand(" delete from sys_ent_extra where ent_id = @EntId");
            cmdDeleteSysEntExtra.Parameters.Add("@EntId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadSysEntExtra

            cmdLoadSysEntExtra = new MySqlCommand(@" select idsys_ent_extra,ent_id,artificial_person,registered_capital,date_of_establishment,address,province,city,contact_phone,web,weibo,weixin,main_business from sys_ent_extra limit @PageIndex,@PageSize");
            cmdLoadSysEntExtra.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadSysEntExtra.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetSysEntExtraCount

            cmdGetSysEntExtraCount = new MySqlCommand(" select count(*)  from sys_ent_extra ");

            #endregion

            #region cmdLoadAllSysEntExtra

            cmdLoadAllSysEntExtra = new MySqlCommand(" select idsys_ent_extra,ent_id,artificial_person,registered_capital,date_of_establishment,address,province,city,contact_phone,web,weibo,weixin,main_business from sys_ent_extra");

            #endregion

            #region cmdGetSysEntExtra

            cmdGetSysEntExtra = new MySqlCommand(" select idsys_ent_extra,ent_id,artificial_person,registered_capital,date_of_establishment,address,province,city,contact_phone,web,weibo,weixin,main_business from sys_ent_extra where ent_id = @EntId");
            cmdGetSysEntExtra.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param> 
        /// <returns></returns>
        /// </summary>
        public int Insert(SysEntExtra e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertSysEntExtra = cmdInsertSysEntExtra.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertSysEntExtra.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertSysEntExtra.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertSysEntExtra.Parameters["@ArtificialPerson"].Value = e.ArtificialPerson;
                _cmdInsertSysEntExtra.Parameters["@RegisteredCapital"].Value = e.RegisteredCapital;
                _cmdInsertSysEntExtra.Parameters["@DateOfEstablishment"].Value = e.DateOfEstablishment;
                _cmdInsertSysEntExtra.Parameters["@Address"].Value = e.Address;
                _cmdInsertSysEntExtra.Parameters["@Province"].Value = e.Province;
                _cmdInsertSysEntExtra.Parameters["@City"].Value = e.City;
                _cmdInsertSysEntExtra.Parameters["@ContactPhone"].Value = e.ContactPhone;
                _cmdInsertSysEntExtra.Parameters["@Web"].Value = e.Web;
                _cmdInsertSysEntExtra.Parameters["@Weibo"].Value = e.Weibo;
                _cmdInsertSysEntExtra.Parameters["@Weixin"].Value = e.Weixin;
                _cmdInsertSysEntExtra.Parameters["@MainBusiness"].Value = e.MainBusiness;

                _cmdInsertSysEntExtra.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertSysEntExtra.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertSysEntExtra.Dispose();
                _cmdInsertSysEntExtra = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int EntId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteSysEntExtra = cmdDeleteSysEntExtra.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteSysEntExtra.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteSysEntExtra.Parameters["@EntId"].Value = EntId;


                _cmdDeleteSysEntExtra.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteSysEntExtra.Dispose();
                _cmdDeleteSysEntExtra = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysEntExtra e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateSysEntExtra = cmdUpdateSysEntExtra.Clone() as MySqlCommand;
            _cmdUpdateSysEntExtra.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateSysEntExtra.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateSysEntExtra.Parameters["@ArtificialPerson"].Value = e.ArtificialPerson;
                _cmdUpdateSysEntExtra.Parameters["@RegisteredCapital"].Value = e.RegisteredCapital;
                _cmdUpdateSysEntExtra.Parameters["@DateOfEstablishment"].Value = e.DateOfEstablishment;
                _cmdUpdateSysEntExtra.Parameters["@Address"].Value = e.Address;
                _cmdUpdateSysEntExtra.Parameters["@Province"].Value = e.Province;
                _cmdUpdateSysEntExtra.Parameters["@City"].Value = e.City;
                _cmdUpdateSysEntExtra.Parameters["@ContactPhone"].Value = e.ContactPhone;
                _cmdUpdateSysEntExtra.Parameters["@Web"].Value = e.Web;
                _cmdUpdateSysEntExtra.Parameters["@Weibo"].Value = e.Weibo;
                _cmdUpdateSysEntExtra.Parameters["@Weixin"].Value = e.Weixin;
                _cmdUpdateSysEntExtra.Parameters["@MainBusiness"].Value = e.MainBusiness;

                _cmdUpdateSysEntExtra.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateSysEntExtra.Dispose();
                _cmdUpdateSysEntExtra = null;
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
        public PageEntity<SysEntExtra> Search(Int32 IdsysEntExtra, Int32 EntId, String ArtificialPerson, String RegisteredCapital, DateTime DateOfEstablishment, String Address, String Province, String City, Int32 ContactPhone, String Web, String Weibo, String Weixin, String MainBusiness, int pageIndex, int pageSize)
        {
            PageEntity<SysEntExtra> returnValue = new PageEntity<SysEntExtra>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadSysEntExtra = cmdLoadSysEntExtra.Clone() as MySqlCommand;
            MySqlCommand _cmdGetSysEntExtraCount = cmdGetSysEntExtraCount.Clone() as MySqlCommand;
            _cmdLoadSysEntExtra.Connection = oc;
            _cmdGetSysEntExtraCount.Connection = oc;

            try
            {
                _cmdLoadSysEntExtra.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadSysEntExtra.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadSysEntExtra.Parameters["@IdsysEntExtra"].Value = IdsysEntExtra;
                _cmdLoadSysEntExtra.Parameters["@EntId"].Value = EntId;
                _cmdLoadSysEntExtra.Parameters["@ArtificialPerson"].Value = ArtificialPerson;
                _cmdLoadSysEntExtra.Parameters["@RegisteredCapital"].Value = RegisteredCapital;
                _cmdLoadSysEntExtra.Parameters["@DateOfEstablishment"].Value = DateOfEstablishment;
                _cmdLoadSysEntExtra.Parameters["@Address"].Value = Address;
                _cmdLoadSysEntExtra.Parameters["@Province"].Value = Province;
                _cmdLoadSysEntExtra.Parameters["@City"].Value = City;
                _cmdLoadSysEntExtra.Parameters["@ContactPhone"].Value = ContactPhone;
                _cmdLoadSysEntExtra.Parameters["@Web"].Value = Web;
                _cmdLoadSysEntExtra.Parameters["@Weibo"].Value = Weibo;
                _cmdLoadSysEntExtra.Parameters["@Weixin"].Value = Weixin;
                _cmdLoadSysEntExtra.Parameters["@MainBusiness"].Value = MainBusiness;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadSysEntExtra.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new SysEntExtra().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetSysEntExtraCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadSysEntExtra.Dispose();
                _cmdLoadSysEntExtra = null;
                _cmdGetSysEntExtraCount.Dispose();
                _cmdGetSysEntExtraCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysEntExtra> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllSysEntExtra = cmdLoadAllSysEntExtra.Clone() as MySqlCommand;
            _cmdLoadAllSysEntExtra.Connection = oc; List<SysEntExtra> returnValue = new List<SysEntExtra>();
            try
            {
                _cmdLoadAllSysEntExtra.CommandText = string.Format(_cmdLoadAllSysEntExtra.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllSysEntExtra.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new SysEntExtra().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllSysEntExtra.Dispose();
                _cmdLoadAllSysEntExtra = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysEntExtra Get(int entId)
        {
            SysEntExtra returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetSysEntExtra = cmdGetSysEntExtra.Clone() as MySqlCommand;

            _cmdGetSysEntExtra.Connection = oc;
            try
            {
                _cmdGetSysEntExtra.Parameters["@EntId"].Value = entId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetSysEntExtra.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new SysEntExtra().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetSysEntExtra.Dispose();
                _cmdGetSysEntExtra = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly SysEntExtraAccessor instance = new SysEntExtraAccessor();
        public static SysEntExtraAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
