/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/28 15:34:11
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
    public class CustomerEntAccessor
    {
        private MySqlCommand cmdInsertCustomerEnt;
        private MySqlCommand cmdDeleteCustomerEnt;
        private MySqlCommand cmdUpdateCustomerEnt;
        private MySqlCommand cmdLoadCustomerEnt;
        private MySqlCommand cmdLoadAllCustomerEnt;
        private MySqlCommand cmdGetCustomerEntCount;
        private MySqlCommand cmdGetCustomerEnt;

        private MySqlCommand cmdSearchCustomerEntByOwnerId;
        private MySqlCommand cmdSearchCustomerEntByName;

        private CustomerEntAccessor()
        {
            #region cmdInsertCustomerEnt

            cmdInsertCustomerEnt = new MySqlCommand("INSERT INTO customer_ent(ent_name,industy,contact_username,contact_mobile,contact_phone,contact_email,contact_qq,address,detail,ent_id,owner_id) values (@EntName,@Industy,@ContactUsername,@ContactMobile,@ContactPhone,@ContactEmail,@ContactQq,@Address,@Detail,@EntId,@OwnerId)");

            
            cmdInsertCustomerEnt.Parameters.Add("@EntName", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@Industy", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@ContactUsername", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@ContactMobile", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@ContactPhone", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@ContactEmail", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@ContactQq", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@Address", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@Detail", MySqlDbType.String);
            cmdInsertCustomerEnt.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertCustomerEnt.Parameters.Add("@OwnerId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateCustomerEnt

            cmdUpdateCustomerEnt = new MySqlCommand(" update customer_ent set ent_name = @EntName,industy = @Industy,contact_username = @ContactUsername,contact_mobile = @ContactMobile,contact_phone = @ContactPhone,contact_email = @ContactEmail,contact_qq = @ContactQq,address = @Address,detail = @Detail,ent_id = @EntId,owner_id = @OwnerId where customer_ent_id = @CustomerEntId");

            cmdUpdateCustomerEnt.Parameters.Add("@EntName", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@Industy", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@ContactUsername", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@ContactMobile", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@ContactPhone", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@ContactEmail", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@ContactQq", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@Address", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@Detail", MySqlDbType.String);
            cmdUpdateCustomerEnt.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateCustomerEnt.Parameters.Add("@OwnerId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteCustomerEnt

            cmdDeleteCustomerEnt = new MySqlCommand(" delete from customer_ent where customer_ent_id = @CustomerEntId");
            cmdDeleteCustomerEnt.Parameters.Add("@CustomerEntId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadCustomerEnt

            cmdLoadCustomerEnt = new MySqlCommand(@" select customer_ent_id,ent_name,industy,contact_username,contact_mobile,contact_phone,contact_email,contact_qq,address,detail,ent_id,owner_id from customer_ent limit @PageIndex,@PageSize");
            cmdLoadCustomerEnt.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadCustomerEnt.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetCustomerEntCount

            cmdGetCustomerEntCount = new MySqlCommand(" select count(*)  from customer_ent ");

            #endregion

            #region cmdLoadAllCustomerEnt

            cmdLoadAllCustomerEnt = new MySqlCommand(" select customer_ent_id,ent_name,industy,contact_username,contact_mobile,contact_phone,contact_email,contact_qq,address,detail,ent_id,owner_id from customer_ent");

            #endregion

            #region cmdGetCustomerEnt

            cmdGetCustomerEnt = new MySqlCommand(" select customer_ent_id,ent_name,industy,contact_username,contact_mobile,contact_phone,contact_email,contact_qq,address,detail,ent_id,owner_id from customer_ent where customer_ent_id = @CustomerEntId");
            cmdGetCustomerEnt.Parameters.Add("@CustomerEntId", MySqlDbType.Int32);

            #endregion

            #region cmdSearchCustomerEntByOwnerId

            cmdSearchCustomerEntByOwnerId = new MySqlCommand(" select customer_ent_id,ent_name,industy,contact_username,contact_mobile,contact_phone,contact_email,contact_qq,address,detail,ent_id,owner_id from customer_ent where owner_id = @OwnerId");
            cmdSearchCustomerEntByOwnerId.Parameters.Add("@OwnerId", MySqlDbType.Int32);

            #endregion
            #region cmdSearchCustomerEntByName

            cmdSearchCustomerEntByName = new MySqlCommand(" select customer_ent_id,ent_name,industy,contact_username,contact_mobile,contact_phone,contact_email,contact_qq,address,detail,ent_id,owner_id from customer_ent where ent_name = @EntName");
            cmdSearchCustomerEntByName.Parameters.Add("@EntName", MySqlDbType.String);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(CustomerEnt e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertCustomerEnt = cmdInsertCustomerEnt.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertCustomerEnt.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
             
                _cmdInsertCustomerEnt.Parameters["@EntName"].Value = e.EntName;
                _cmdInsertCustomerEnt.Parameters["@Industy"].Value = e.Industy;
                _cmdInsertCustomerEnt.Parameters["@ContactUsername"].Value = e.ContactUsername;
                _cmdInsertCustomerEnt.Parameters["@ContactMobile"].Value = e.ContactMobile;
                _cmdInsertCustomerEnt.Parameters["@ContactPhone"].Value = e.ContactPhone;
                _cmdInsertCustomerEnt.Parameters["@ContactEmail"].Value = e.ContactEmail;
                _cmdInsertCustomerEnt.Parameters["@ContactQq"].Value = e.ContactQq;
                _cmdInsertCustomerEnt.Parameters["@Address"].Value = e.Address;
                _cmdInsertCustomerEnt.Parameters["@Detail"].Value = e.Detail;
                _cmdInsertCustomerEnt.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertCustomerEnt.Parameters["@OwnerId"].Value = e.OwnerId;

                _cmdInsertCustomerEnt.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertCustomerEnt.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertCustomerEnt.Dispose();
                _cmdInsertCustomerEnt = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int CustomerEntId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteCustomerEnt = cmdDeleteCustomerEnt.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteCustomerEnt.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteCustomerEnt.Parameters["@CustomerEntId"].Value = CustomerEntId;


                _cmdDeleteCustomerEnt.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteCustomerEnt.Dispose();
                _cmdDeleteCustomerEnt = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(CustomerEnt e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateCustomerEnt = cmdUpdateCustomerEnt.Clone() as MySqlCommand;
            _cmdUpdateCustomerEnt.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateCustomerEnt.Parameters["@CustomerEntId"].Value = e.CustomerEntId;
                _cmdUpdateCustomerEnt.Parameters["@EntName"].Value = e.EntName;
                _cmdUpdateCustomerEnt.Parameters["@Industy"].Value = e.Industy;
                _cmdUpdateCustomerEnt.Parameters["@ContactUsername"].Value = e.ContactUsername;
                _cmdUpdateCustomerEnt.Parameters["@ContactMobile"].Value = e.ContactMobile;
                _cmdUpdateCustomerEnt.Parameters["@ContactPhone"].Value = e.ContactPhone;
                _cmdUpdateCustomerEnt.Parameters["@ContactEmail"].Value = e.ContactEmail;
                _cmdUpdateCustomerEnt.Parameters["@ContactQq"].Value = e.ContactQq;
                _cmdUpdateCustomerEnt.Parameters["@Address"].Value = e.Address;
                _cmdUpdateCustomerEnt.Parameters["@Detail"].Value = e.Detail;
                _cmdUpdateCustomerEnt.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateCustomerEnt.Parameters["@OwnerId"].Value = e.OwnerId;

                _cmdUpdateCustomerEnt.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateCustomerEnt.Dispose();
                _cmdUpdateCustomerEnt = null;
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
        public PageEntity<CustomerEnt> Search(Int32 CustomerEntId, String EntName, String Industy, String ContactUsername, String ContactMobile, String ContactPhone, String ContactEmail, String ContactQq, String Address, String Detail, Int32 EntId, Int32 OwnerId, int pageIndex, int pageSize)
        {
            PageEntity<CustomerEnt> returnValue = new PageEntity<CustomerEnt>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadCustomerEnt = cmdLoadCustomerEnt.Clone() as MySqlCommand;
            MySqlCommand _cmdGetCustomerEntCount = cmdGetCustomerEntCount.Clone() as MySqlCommand;
            _cmdLoadCustomerEnt.Connection = oc;
            _cmdGetCustomerEntCount.Connection = oc;

            try
            {
                _cmdLoadCustomerEnt.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadCustomerEnt.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadCustomerEnt.Parameters["@CustomerEntId"].Value = CustomerEntId;
                _cmdLoadCustomerEnt.Parameters["@EntName"].Value = EntName;
                _cmdLoadCustomerEnt.Parameters["@Industy"].Value = Industy;
                _cmdLoadCustomerEnt.Parameters["@ContactUsername"].Value = ContactUsername;
                _cmdLoadCustomerEnt.Parameters["@ContactMobile"].Value = ContactMobile;
                _cmdLoadCustomerEnt.Parameters["@ContactPhone"].Value = ContactPhone;
                _cmdLoadCustomerEnt.Parameters["@ContactEmail"].Value = ContactEmail;
                _cmdLoadCustomerEnt.Parameters["@ContactQq"].Value = ContactQq;
                _cmdLoadCustomerEnt.Parameters["@Address"].Value = Address;
                _cmdLoadCustomerEnt.Parameters["@Detail"].Value = Detail;
                _cmdLoadCustomerEnt.Parameters["@EntId"].Value = EntId;
                _cmdLoadCustomerEnt.Parameters["@OwnerId"].Value = OwnerId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadCustomerEnt.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new CustomerEnt().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetCustomerEntCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadCustomerEnt.Dispose();
                _cmdLoadCustomerEnt = null;
                _cmdGetCustomerEntCount.Dispose();
                _cmdGetCustomerEntCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<CustomerEnt> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllCustomerEnt = cmdLoadAllCustomerEnt.Clone() as MySqlCommand;
            _cmdLoadAllCustomerEnt.Connection = oc; List<CustomerEnt> returnValue = new List<CustomerEnt>();
            try
            {
                _cmdLoadAllCustomerEnt.CommandText = string.Format(_cmdLoadAllCustomerEnt.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllCustomerEnt.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new CustomerEnt().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllCustomerEnt.Dispose();
                _cmdLoadAllCustomerEnt = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public CustomerEnt Get(int CustomerEntId)
        {
            CustomerEnt returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetCustomerEnt = cmdGetCustomerEnt.Clone() as MySqlCommand;

            _cmdGetCustomerEnt.Connection = oc;
            try
            {
                _cmdGetCustomerEnt.Parameters["@CustomerEntId"].Value = CustomerEntId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetCustomerEnt.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new CustomerEnt().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetCustomerEnt.Dispose();
                _cmdGetCustomerEnt = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 通过销售ID获取企业客户
        /// </summary>
        public List<CustomerEnt> SearchCustomerEntByOwnerId(int ownerid)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdSearchCustomerEntByOwnerId = cmdSearchCustomerEntByOwnerId.Clone() as MySqlCommand;
            _cmdSearchCustomerEntByOwnerId.Connection = oc; 
            List<CustomerEnt> returnValue = new List<CustomerEnt>();
            try
            {
                _cmdSearchCustomerEntByOwnerId.Parameters["@OwnerId"].Value = ownerid;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdSearchCustomerEntByOwnerId.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new CustomerEnt().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdSearchCustomerEntByOwnerId.Dispose();
                _cmdSearchCustomerEntByOwnerId = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 通过企业名称获取企业客户
        /// </summary>
        public List<CustomerEnt> SearchCustomerEntByName(string name)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdSearchCustomerEntByName = cmdSearchCustomerEntByName.Clone() as MySqlCommand;
            _cmdSearchCustomerEntByName.Connection = oc;
            List<CustomerEnt> returnValue = new List<CustomerEnt>();
            try
            {
                _cmdSearchCustomerEntByName.Parameters["@EntName"].Value = name;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdSearchCustomerEntByName.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new CustomerEnt().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdSearchCustomerEntByName.Dispose();
                _cmdSearchCustomerEntByName = null;
                GC.Collect();
            }
            return returnValue;
        }


        private static readonly CustomerEntAccessor instance = new CustomerEntAccessor();
        public static CustomerEntAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
