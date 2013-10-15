/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/28 15:34:35
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
    public class CustomerPrivateAccessor
    {
        private MySqlCommand cmdInsertCustomerPrivate;
        private MySqlCommand cmdDeleteCustomerPrivate;
        private MySqlCommand cmdUpdateCustomerPrivate;
        private MySqlCommand cmdLoadCustomerPrivate;
        private MySqlCommand cmdLoadAllCustomerPrivate;
        private MySqlCommand cmdGetCustomerPrivateCount;
        private MySqlCommand cmdGetCustomerPrivate;

        private MySqlCommand cmdSearchCustomerPrivByOwnerId;
        private MySqlCommand cmdSearchCustomerPrivByName;

        private CustomerPrivateAccessor()
        {
            #region cmdInsertCustomerPrivate

            cmdInsertCustomerPrivate = new MySqlCommand("INSERT INTO customer_private(name,sex,birthday,industy,mobile,email,qq,phone,address,detail,ent_id,owner_id) values (@Name,@Sex,@Birthday,@Industy,@Mobile,@Email,@Qq,@Phone,@Address,@Detail,@EntId,@OwnerId)");

            cmdInsertCustomerPrivate.Parameters.Add("@Name", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Sex", MySqlDbType.Int32);
            cmdInsertCustomerPrivate.Parameters.Add("@Birthday", MySqlDbType.DateTime);
            cmdInsertCustomerPrivate.Parameters.Add("@Industy", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Mobile", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Email", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Qq", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Phone", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Address", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@Detail", MySqlDbType.String);
            cmdInsertCustomerPrivate.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertCustomerPrivate.Parameters.Add("@OwnerId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateCustomerPrivate

            cmdUpdateCustomerPrivate = new MySqlCommand(" update customer_private set name = @Name,sex = @Sex,birthday = @Birthday,industy = @Industy,mobile = @Mobile,email = @Email,qq = @Qq,phone = @Phone,address = @Address,detail = @Detail,ent_id = @EntId,owner_id = @OwnerId where customer_private_id = @CustomerPrivateId");

            cmdUpdateCustomerPrivate.Parameters.Add("@Name", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Sex", MySqlDbType.Int32);
            cmdUpdateCustomerPrivate.Parameters.Add("@Birthday", MySqlDbType.DateTime);
            cmdUpdateCustomerPrivate.Parameters.Add("@Industy", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Mobile", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Email", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Qq", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Phone", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Address", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@Detail", MySqlDbType.String);
            cmdUpdateCustomerPrivate.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateCustomerPrivate.Parameters.Add("@OwnerId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteCustomerPrivate

            cmdDeleteCustomerPrivate = new MySqlCommand(" delete from customer_private where customer_private_id = @CustomerPrivateId");
            cmdDeleteCustomerPrivate.Parameters.Add("@CustomerPrivateId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadCustomerPrivate

            cmdLoadCustomerPrivate = new MySqlCommand(@" select customer_private_id,name,sex,birthday,industy,mobile,email,qq,phone,address,detail,ent_id,owner_id from customer_private limit @PageIndex,@PageSize");
            cmdLoadCustomerPrivate.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadCustomerPrivate.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetCustomerPrivateCount

            cmdGetCustomerPrivateCount = new MySqlCommand(" select count(*)  from customer_private ");

            #endregion

            #region cmdLoadAllCustomerPrivate

            cmdLoadAllCustomerPrivate = new MySqlCommand(" select customer_private_id,name,sex,birthday,industy,mobile,email,qq,phone,address,detail,ent_id,owner_id from customer_private");

            #endregion

            #region cmdGetCustomerPrivate

            cmdGetCustomerPrivate = new MySqlCommand(" select customer_private_id,name,sex,birthday,industy,mobile,email,qq,phone,address,detail,ent_id,owner_id from customer_private where customer_private_id = @CustomerPrivateId");
            cmdGetCustomerPrivate.Parameters.Add("@CustomerPrivateId", MySqlDbType.Int32);

            #endregion

            #region cmdSearchCustomerPrivByOwnerId

            cmdSearchCustomerPrivByOwnerId = new MySqlCommand(" select customer_private_id,name,sex,birthday,industy,mobile,email,qq,phone,address,detail,ent_id,owner_id from customer_private where owner_id = @OwnerId");
            cmdSearchCustomerPrivByOwnerId.Parameters.Add("@OwnerId", MySqlDbType.Int32);

            #endregion

            #region cmdSearchCustomerPrivByName

            cmdSearchCustomerPrivByName = new MySqlCommand(" select customer_private_id,name,sex,birthday,industy,mobile,email,qq,phone,address,detail,ent_id,owner_id from customer_private where name = @Name");
            cmdSearchCustomerPrivByName.Parameters.Add("@Name", MySqlDbType.String);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(CustomerPrivate e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertCustomerPrivate = cmdInsertCustomerPrivate.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertCustomerPrivate.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
              
                _cmdInsertCustomerPrivate.Parameters["@Name"].Value = e.Name;
                _cmdInsertCustomerPrivate.Parameters["@Sex"].Value = e.Sex;
                _cmdInsertCustomerPrivate.Parameters["@Birthday"].Value = e.Birthday;
                _cmdInsertCustomerPrivate.Parameters["@Industy"].Value = e.Industy;
                _cmdInsertCustomerPrivate.Parameters["@Mobile"].Value = e.Mobile;
                _cmdInsertCustomerPrivate.Parameters["@Email"].Value = e.Email;
                _cmdInsertCustomerPrivate.Parameters["@Qq"].Value = e.Qq;
                _cmdInsertCustomerPrivate.Parameters["@Phone"].Value = e.Phone;
                _cmdInsertCustomerPrivate.Parameters["@Address"].Value = e.Address;
                _cmdInsertCustomerPrivate.Parameters["@Detail"].Value = e.Detail;
                _cmdInsertCustomerPrivate.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertCustomerPrivate.Parameters["@OwnerId"].Value = e.OwnerId;

                _cmdInsertCustomerPrivate.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertCustomerPrivate.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertCustomerPrivate.Dispose();
                _cmdInsertCustomerPrivate = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int CustomerPrivateId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteCustomerPrivate = cmdDeleteCustomerPrivate.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteCustomerPrivate.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteCustomerPrivate.Parameters["@CustomerPrivateId"].Value = CustomerPrivateId;


                _cmdDeleteCustomerPrivate.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteCustomerPrivate.Dispose();
                _cmdDeleteCustomerPrivate = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(CustomerPrivate e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateCustomerPrivate = cmdUpdateCustomerPrivate.Clone() as MySqlCommand;
            _cmdUpdateCustomerPrivate.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateCustomerPrivate.Parameters["@CustomerPrivateId"].Value = e.CustomerPrivateId;
                _cmdUpdateCustomerPrivate.Parameters["@Name"].Value = e.Name;
                _cmdUpdateCustomerPrivate.Parameters["@Sex"].Value = e.Sex;
                _cmdUpdateCustomerPrivate.Parameters["@Birthday"].Value = e.Birthday;
                _cmdUpdateCustomerPrivate.Parameters["@Industy"].Value = e.Industy;
                _cmdUpdateCustomerPrivate.Parameters["@Mobile"].Value = e.Mobile;
                _cmdUpdateCustomerPrivate.Parameters["@Email"].Value = e.Email;
                _cmdUpdateCustomerPrivate.Parameters["@Qq"].Value = e.Qq;
                _cmdUpdateCustomerPrivate.Parameters["@Phone"].Value = e.Phone;
                _cmdUpdateCustomerPrivate.Parameters["@Address"].Value = e.Address;
                _cmdUpdateCustomerPrivate.Parameters["@Detail"].Value = e.Detail;
                _cmdUpdateCustomerPrivate.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateCustomerPrivate.Parameters["@OwnerId"].Value = e.OwnerId;

                _cmdUpdateCustomerPrivate.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateCustomerPrivate.Dispose();
                _cmdUpdateCustomerPrivate = null;
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
        public PageEntity<CustomerPrivate> Search(Int32 CustomerPrivateId, String Name, Int32 Sex, DateTime Birthday, String Industy, String Mobile, String Email, String Qq, String Phone, String Address, String Detail, Int32 EntId, Int32 OwnerId, int pageIndex, int pageSize)
        {
            PageEntity<CustomerPrivate> returnValue = new PageEntity<CustomerPrivate>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadCustomerPrivate = cmdLoadCustomerPrivate.Clone() as MySqlCommand;
            MySqlCommand _cmdGetCustomerPrivateCount = cmdGetCustomerPrivateCount.Clone() as MySqlCommand;
            _cmdLoadCustomerPrivate.Connection = oc;
            _cmdGetCustomerPrivateCount.Connection = oc;

            try
            {
                _cmdLoadCustomerPrivate.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadCustomerPrivate.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadCustomerPrivate.Parameters["@CustomerPrivateId"].Value = CustomerPrivateId;
                _cmdLoadCustomerPrivate.Parameters["@Name"].Value = Name;
                _cmdLoadCustomerPrivate.Parameters["@Sex"].Value = Sex;
                _cmdLoadCustomerPrivate.Parameters["@Birthday"].Value = Birthday;
                _cmdLoadCustomerPrivate.Parameters["@Industy"].Value = Industy;
                _cmdLoadCustomerPrivate.Parameters["@Mobile"].Value = Mobile;
                _cmdLoadCustomerPrivate.Parameters["@Email"].Value = Email;
                _cmdLoadCustomerPrivate.Parameters["@Qq"].Value = Qq;
                _cmdLoadCustomerPrivate.Parameters["@Phone"].Value = Phone;
                _cmdLoadCustomerPrivate.Parameters["@Address"].Value = Address;
                _cmdLoadCustomerPrivate.Parameters["@Detail"].Value = Detail;
                _cmdLoadCustomerPrivate.Parameters["@EntId"].Value = EntId;
                _cmdLoadCustomerPrivate.Parameters["@OwnerId"].Value = OwnerId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadCustomerPrivate.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new CustomerPrivate().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetCustomerPrivateCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadCustomerPrivate.Dispose();
                _cmdLoadCustomerPrivate = null;
                _cmdGetCustomerPrivateCount.Dispose();
                _cmdGetCustomerPrivateCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<CustomerPrivate> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllCustomerPrivate = cmdLoadAllCustomerPrivate.Clone() as MySqlCommand;
            _cmdLoadAllCustomerPrivate.Connection = oc; List<CustomerPrivate> returnValue = new List<CustomerPrivate>();
            try
            {
                _cmdLoadAllCustomerPrivate.CommandText = string.Format(_cmdLoadAllCustomerPrivate.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllCustomerPrivate.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new CustomerPrivate().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllCustomerPrivate.Dispose();
                _cmdLoadAllCustomerPrivate = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public CustomerPrivate Get(int CustomerPrivateId)
        {
            CustomerPrivate returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetCustomerPrivate = cmdGetCustomerPrivate.Clone() as MySqlCommand;

            _cmdGetCustomerPrivate.Connection = oc;
            try
            {
                _cmdGetCustomerPrivate.Parameters["@CustomerPrivateId"].Value = CustomerPrivateId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetCustomerPrivate.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new CustomerPrivate().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetCustomerPrivate.Dispose();
                _cmdGetCustomerPrivate = null;
                GC.Collect();
            }
            return returnValue;

        }

        public List<CustomerPrivate> SearchCustomerPrivByOwnerId(int ownerid)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdSearchCustomerPrivByOwnerId = cmdSearchCustomerPrivByOwnerId.Clone() as MySqlCommand;
            _cmdSearchCustomerPrivByOwnerId.Connection = oc;
            List<CustomerPrivate> returnValue = new List<CustomerPrivate>();
            try
            {
                _cmdSearchCustomerPrivByOwnerId.Parameters["@OwnerId"].Value = ownerid;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdSearchCustomerPrivByOwnerId.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new CustomerPrivate().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdSearchCustomerPrivByOwnerId.Dispose();
                _cmdSearchCustomerPrivByOwnerId = null;
                GC.Collect();
            }
            return returnValue;
        }

        public List<CustomerPrivate> SearchCustomerPrivByName(string name)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdSearchCustomerPrivByName = cmdSearchCustomerPrivByName.Clone() as MySqlCommand;
            _cmdSearchCustomerPrivByName.Connection = oc;
            List<CustomerPrivate> returnValue = new List<CustomerPrivate>();
            try
            {
                _cmdSearchCustomerPrivByName.Parameters["@Name"].Value = name;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdSearchCustomerPrivByName.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new CustomerPrivate().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdSearchCustomerPrivByName.Dispose();
                _cmdSearchCustomerPrivByName = null;
                GC.Collect();
            }
            return returnValue;
        }

        private static readonly CustomerPrivateAccessor instance = new CustomerPrivateAccessor();
        public static CustomerPrivateAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
