/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 15:37:56
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
    public class ProProductonDetailAccessor
    {
        private MySqlCommand cmdInsertProProductonDetail;
        private MySqlCommand cmdDeleteProProductonDetail;
        private MySqlCommand cmdUpdateProProductonDetail;
        private MySqlCommand cmdLoadProProductonDetail;
        private MySqlCommand cmdLoadAllProProductonDetail;
        private MySqlCommand cmdGetProProductonDetailCount;
        private MySqlCommand cmdGetProProductonDetail;

        private ProProductonDetailAccessor()
        {
            #region cmdInsertProProductonDetail

            cmdInsertProProductonDetail = new MySqlCommand("INSERT INTO pro_producton_detail(price,p_num,p_code,create_time,user_id,p_id,ent_id,is_pay) values (@PDId,@Price,@PNum,@PCode,@CreateTime,@UserId,@PId,@EntId)");

            cmdInsertProProductonDetail.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdInsertProProductonDetail.Parameters.Add("@PNum", MySqlDbType.Int32);
            cmdInsertProProductonDetail.Parameters.Add("@PCode", MySqlDbType.String);
            cmdInsertProProductonDetail.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdInsertProProductonDetail.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdInsertProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);
            cmdInsertProProductonDetail.Parameters.Add("@EntId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateProProductonDetail

            cmdUpdateProProductonDetail = new MySqlCommand(" update pro_producton_detail set price = @Price,p_num = @PNum,p_code = @PCode,create_time = @CreateTime,user_id = @UserId,p_id = @PId,ent_id = @EntId,is_pay = @IsPay where p_d_id = @PDId");
            cmdUpdateProProductonDetail.Parameters.Add("@PDId", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdUpdateProProductonDetail.Parameters.Add("@PNum", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@PCode", MySqlDbType.String);
            cmdUpdateProProductonDetail.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdUpdateProProductonDetail.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@IsPay", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteProProductonDetail

            cmdDeleteProProductonDetail = new MySqlCommand(" delete from pro_producton_detail where p_d_id = @PDId");
            cmdDeleteProProductonDetail.Parameters.Add("@PDId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadProProductonDetail

            cmdLoadProProductonDetail = new MySqlCommand(@" select p_d_id,price,p_num,p_code,create_time,user_id,p_id,ent_id,is_pay from pro_producton_detail limit @PageIndex,@PageSize");
            cmdLoadProProductonDetail.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadProProductonDetail.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetProProductonDetailCount

            cmdGetProProductonDetailCount = new MySqlCommand(" select count(*)  from pro_producton_detail ");

            #endregion

            #region cmdLoadAllProProductonDetail

            cmdLoadAllProProductonDetail = new MySqlCommand(" select p_d_id,price,p_num,p_code,create_time,user_id,p_id,ent_id,is_pay from pro_producton_detail");

            #endregion

            #region cmdGetProProductonDetail

            cmdGetProProductonDetail = new MySqlCommand(" select p_d_id,price,p_num,p_code,create_time,user_id,p_id,ent_id,is_pay from pro_producton_detail where p_d_id = @PDId");
            cmdGetProProductonDetail.Parameters.Add("@PDId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ProProductonDetail e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertProProductonDetail = cmdInsertProProductonDetail.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertProProductonDetail.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertProProductonDetail.Parameters["@Price"].Value = e.Price;
                _cmdInsertProProductonDetail.Parameters["@PNum"].Value = e.PNum;
                _cmdInsertProProductonDetail.Parameters["@PCode"].Value = e.PCode;
                _cmdInsertProProductonDetail.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdInsertProProductonDetail.Parameters["@UserId"].Value = e.UserId;
                _cmdInsertProProductonDetail.Parameters["@PId"].Value = e.PId;
                _cmdInsertProProductonDetail.Parameters["@EntId"].Value = e.EntId;

                _cmdInsertProProductonDetail.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertProProductonDetail.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertProProductonDetail.Dispose();
                _cmdInsertProProductonDetail = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int PDId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteProProductonDetail = cmdDeleteProProductonDetail.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteProProductonDetail.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteProProductonDetail.Parameters["@PDId"].Value = PDId;


                _cmdDeleteProProductonDetail.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteProProductonDetail.Dispose();
                _cmdDeleteProProductonDetail = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ProProductonDetail e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateProProductonDetail = cmdUpdateProProductonDetail.Clone() as MySqlCommand;
            _cmdUpdateProProductonDetail.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateProProductonDetail.Parameters["@PDId"].Value = e.PDId;
                _cmdUpdateProProductonDetail.Parameters["@Price"].Value = e.Price;
                _cmdUpdateProProductonDetail.Parameters["@PNum"].Value = e.PNum;
                _cmdUpdateProProductonDetail.Parameters["@PCode"].Value = e.PCode;
                _cmdUpdateProProductonDetail.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdUpdateProProductonDetail.Parameters["@UserId"].Value = e.UserId;
                _cmdUpdateProProductonDetail.Parameters["@PId"].Value = e.PId;
                _cmdUpdateProProductonDetail.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateProProductonDetail.Parameters["@IsPay"].Value = e.IsPay;

                _cmdUpdateProProductonDetail.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateProProductonDetail.Dispose();
                _cmdUpdateProProductonDetail = null;
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
        public PageEntity<ProProductonDetail> Search(Int32 UserId, Int32 PId, Int32 EntId, int pageIndex, int pageSize)
        {
            PageEntity<ProProductonDetail> returnValue = new PageEntity<ProProductonDetail>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadProProductonDetail = cmdLoadProProductonDetail.Clone() as MySqlCommand;
            MySqlCommand _cmdGetProProductonDetailCount = cmdGetProProductonDetailCount.Clone() as MySqlCommand;
            _cmdLoadProProductonDetail.Connection = oc;
            _cmdGetProProductonDetailCount.Connection = oc;

            try
            {
                _cmdLoadProProductonDetail.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadProProductonDetail.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadProProductonDetail.Parameters["@UserId"].Value = UserId;
                _cmdLoadProProductonDetail.Parameters["@PId"].Value = PId;
                _cmdLoadProProductonDetail.Parameters["@EntId"].Value = EntId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadProProductonDetail.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new ProProductonDetail().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetProProductonDetailCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadProProductonDetail.Dispose();
                _cmdLoadProProductonDetail = null;
                _cmdGetProProductonDetailCount.Dispose();
                _cmdGetProProductonDetailCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ProProductonDetail> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllProProductonDetail = cmdLoadAllProProductonDetail.Clone() as MySqlCommand;
            _cmdLoadAllProProductonDetail.Connection = oc; List<ProProductonDetail> returnValue = new List<ProProductonDetail>();
            try
            {
                _cmdLoadAllProProductonDetail.CommandText = string.Format(_cmdLoadAllProProductonDetail.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllProProductonDetail.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ProProductonDetail().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllProProductonDetail.Dispose();
                _cmdLoadAllProProductonDetail = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public ProProductonDetail Get(int PDId)
        {
            ProProductonDetail returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetProProductonDetail = cmdGetProProductonDetail.Clone() as MySqlCommand;

            _cmdGetProProductonDetail.Connection = oc;
            try
            {
                _cmdGetProProductonDetail.Parameters["@PDId"].Value = PDId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetProProductonDetail.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ProProductonDetail().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetProProductonDetail.Dispose();
                _cmdGetProProductonDetail = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly ProProductonDetailAccessor instance = new ProProductonDetailAccessor();
        public static ProProductonDetailAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
