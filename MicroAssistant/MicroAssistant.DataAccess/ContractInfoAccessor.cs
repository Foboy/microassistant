/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/31 14:47:10
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
    public class ContractInfoAccessor
    {
        private MySqlCommand cmdInsertContractInfo;
        private MySqlCommand cmdDeleteContractInfo;
        private MySqlCommand cmdUpdateContractInfo;
        private MySqlCommand cmdLoadContractInfo;
        private MySqlCommand cmdLoadAllContractInfo;
        private MySqlCommand cmdGetContractInfoCount;
        private MySqlCommand cmdGetContractInfo;

        private ContractInfoAccessor()
        {
            #region cmdInsertContractInfo

            cmdInsertContractInfo = new MySqlCommand("INSERT INTO contract_info(contract_no,c_name,customer_name,start_time,end_time,owner_id,contract_time,amount,howtopay,howtopay_id,ent_id) values (@ContractNo,@CName,@CustomerName,@StartTime,@EndTime,@OwnerId,@ContractTime,@Amount,@Howtopay,@HowtopayId,@EntId)");

            cmdInsertContractInfo.Parameters.Add("@ContractNo", MySqlDbType.String);
            cmdInsertContractInfo.Parameters.Add("@CName", MySqlDbType.String);
            cmdInsertContractInfo.Parameters.Add("@CustomerName", MySqlDbType.String);
            cmdInsertContractInfo.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdInsertContractInfo.Parameters.Add("@EndTime", MySqlDbType.DateTime);
            cmdInsertContractInfo.Parameters.Add("@OwnerId", MySqlDbType.Int32);
            cmdInsertContractInfo.Parameters.Add("@ContractTime", MySqlDbType.DateTime);
            cmdInsertContractInfo.Parameters.Add("@Amount", MySqlDbType.Decimal);
            cmdInsertContractInfo.Parameters.Add("@Howtopay", MySqlDbType.Int32);
            cmdInsertContractInfo.Parameters.Add("@HowtopayId", MySqlDbType.Int32);
            cmdInsertContractInfo.Parameters.Add("@EntId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateContractInfo

            cmdUpdateContractInfo = new MySqlCommand(" update contract_info set contract_no = @ContractNo,c_name = @CName,customer_name = @CustomerName,start_time = @StartTime,end_time = @EndTime,owner_id = @OwnerId,contract_time = @ContractTime,amount = @Amount,howtopay = @Howtopay,howtopay_id = @HowtopayId,ent_id = @EntId where contract_info_id = @ContractInfoId");
            cmdUpdateContractInfo.Parameters.Add("@ContractInfoId", MySqlDbType.Int32);
            cmdUpdateContractInfo.Parameters.Add("@ContractNo", MySqlDbType.String);
            cmdUpdateContractInfo.Parameters.Add("@CName", MySqlDbType.String);
            cmdUpdateContractInfo.Parameters.Add("@CustomerName", MySqlDbType.String);
            cmdUpdateContractInfo.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdUpdateContractInfo.Parameters.Add("@EndTime", MySqlDbType.DateTime);
            cmdUpdateContractInfo.Parameters.Add("@OwnerId", MySqlDbType.Int32);
            cmdUpdateContractInfo.Parameters.Add("@ContractTime", MySqlDbType.DateTime);
            cmdUpdateContractInfo.Parameters.Add("@Amount", MySqlDbType.Decimal);
            cmdUpdateContractInfo.Parameters.Add("@Howtopay", MySqlDbType.Int32);
            cmdUpdateContractInfo.Parameters.Add("@HowtopayId", MySqlDbType.Int32);
            cmdUpdateContractInfo.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteContractInfo

            cmdDeleteContractInfo = new MySqlCommand(" delete from contract_info where contract_info_id = @ContractInfoId");
            cmdDeleteContractInfo.Parameters.Add("@ContractInfoId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadContractInfo

            cmdLoadContractInfo = new MySqlCommand(@" select contract_info_id,contract_no,c_name,customer_name,start_time,end_time,owner_id,contract_time,amount,howtopay,howtopay_id,ent_id from contract_info where ent_id = @EntId limit @PageIndex,@PageSize");
            cmdLoadContractInfo.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdLoadContractInfo.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadContractInfo.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetContractInfoCount

            cmdGetContractInfoCount = new MySqlCommand(" select count(*)  from contract_info ");

            #endregion

            #region cmdLoadAllContractInfo

            cmdLoadAllContractInfo = new MySqlCommand(" select contract_info_id,contract_no,c_name,customer_name,start_time,end_time,owner_id,contract_time,amount,howtopay,howtopay_id,ent_id from contract_info");

            #endregion

            #region cmdGetContractInfo

            cmdGetContractInfo = new MySqlCommand(" select contract_info_id,contract_no,c_name,customer_name,start_time,end_time,owner_id,contract_time,amount,howtopay,howtopay_id,ent_id from contract_info where contract_no = @ContractNo");
            cmdGetContractInfo.Parameters.Add("@ContractNo", MySqlDbType.String);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ContractInfo e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertContractInfo = cmdInsertContractInfo.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertContractInfo.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertContractInfo.Parameters["@ContractNo"].Value = e.ContractNo;
                _cmdInsertContractInfo.Parameters["@CName"].Value = e.CName;
                _cmdInsertContractInfo.Parameters["@CustomerName"].Value = e.CustomerName;
                _cmdInsertContractInfo.Parameters["@StartTime"].Value = e.StartTime;
                _cmdInsertContractInfo.Parameters["@EndTime"].Value = e.EndTime;
                _cmdInsertContractInfo.Parameters["@OwnerId"].Value = e.OwnerId;
                _cmdInsertContractInfo.Parameters["@ContractTime"].Value = e.ContractTime;
                _cmdInsertContractInfo.Parameters["@Amount"].Value = e.Amount;
                _cmdInsertContractInfo.Parameters["@Howtopay"].Value = e.Howtopay;
                _cmdInsertContractInfo.Parameters["@HowtopayId"].Value = e.HowtopayId;
                _cmdInsertContractInfo.Parameters["@EntId"].Value = e.EntId;

                _cmdInsertContractInfo.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertContractInfo.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertContractInfo.Dispose();
                _cmdInsertContractInfo = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int ContractInfoId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteContractInfo = cmdDeleteContractInfo.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteContractInfo.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteContractInfo.Parameters["@ContractInfoId"].Value = ContractInfoId;


                _cmdDeleteContractInfo.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteContractInfo.Dispose();
                _cmdDeleteContractInfo = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ContractInfo e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateContractInfo = cmdUpdateContractInfo.Clone() as MySqlCommand;
            _cmdUpdateContractInfo.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateContractInfo.Parameters["@ContractInfoId"].Value = e.ContractInfoId;
                _cmdUpdateContractInfo.Parameters["@ContractNo"].Value = e.ContractNo;
                _cmdUpdateContractInfo.Parameters["@CName"].Value = e.CName;
                _cmdUpdateContractInfo.Parameters["@CustomerName"].Value = e.CustomerName;
                _cmdUpdateContractInfo.Parameters["@StartTime"].Value = e.StartTime;
                _cmdUpdateContractInfo.Parameters["@EndTime"].Value = e.EndTime;
                _cmdUpdateContractInfo.Parameters["@OwnerId"].Value = e.OwnerId;
                _cmdUpdateContractInfo.Parameters["@ContractTime"].Value = e.ContractTime;
                _cmdUpdateContractInfo.Parameters["@Amount"].Value = e.Amount;
                _cmdUpdateContractInfo.Parameters["@Howtopay"].Value = e.Howtopay;
                _cmdUpdateContractInfo.Parameters["@HowtopayId"].Value = e.HowtopayId;
                _cmdUpdateContractInfo.Parameters["@EntId"].Value = e.EntId;

                _cmdUpdateContractInfo.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateContractInfo.Dispose();
                _cmdUpdateContractInfo = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 根据条件分页获取指定数据
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>Int32 ContractInfoId, String ContractNo, String CName, Int32 CustomerName, DateTime StartTime, DateTime EndTime, Int32 OwnerId, DateTime ContractTime, Decimal Amount, Int32 Howtopay, Int32 HowtopayId, 
        public PageEntity<ContractInfo> Search(Int32 EntId, int pageIndex, int pageSize)
        {
            PageEntity<ContractInfo> returnValue = new PageEntity<ContractInfo>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadContractInfo = cmdLoadContractInfo.Clone() as MySqlCommand;
            MySqlCommand _cmdGetContractInfoCount = cmdGetContractInfoCount.Clone() as MySqlCommand;
            _cmdLoadContractInfo.Connection = oc;
            _cmdGetContractInfoCount.Connection = oc;

            try
            {
                _cmdLoadContractInfo.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadContractInfo.Parameters["@PageSize"].Value = pageSize;
                //_cmdLoadContractInfo.Parameters["@ContractInfoId"].Value = ContractInfoId;
                //_cmdLoadContractInfo.Parameters["@ContractNo"].Value = ContractNo;
                //_cmdLoadContractInfo.Parameters["@CName"].Value = CName;
                //_cmdLoadContractInfo.Parameters["@CustomerName"].Value = CustomerName;
                //_cmdLoadContractInfo.Parameters["@StartTime"].Value = StartTime;
                //_cmdLoadContractInfo.Parameters["@EndTime"].Value = EndTime;
                //_cmdLoadContractInfo.Parameters["@OwnerId"].Value = OwnerId;
                //_cmdLoadContractInfo.Parameters["@ContractTime"].Value = ContractTime;
                //_cmdLoadContractInfo.Parameters["@Amount"].Value = Amount;
                //_cmdLoadContractInfo.Parameters["@Howtopay"].Value = Howtopay;
                //_cmdLoadContractInfo.Parameters["@HowtopayId"].Value = HowtopayId;
                _cmdLoadContractInfo.Parameters["@EntId"].Value = EntId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadContractInfo.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new ContractInfo().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = (int)_cmdGetContractInfoCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadContractInfo.Dispose();
                _cmdLoadContractInfo = null;
                _cmdGetContractInfoCount.Dispose();
                _cmdGetContractInfoCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ContractInfo> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllContractInfo = cmdLoadAllContractInfo.Clone() as MySqlCommand;
            _cmdLoadAllContractInfo.Connection = oc; List<ContractInfo> returnValue = new List<ContractInfo>();
            try
            {
                _cmdLoadAllContractInfo.CommandText = string.Format(_cmdLoadAllContractInfo.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllContractInfo.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ContractInfo().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllContractInfo.Dispose();
                _cmdLoadAllContractInfo = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="ContractNo">合同编号</param>
        /// </summary>
        public ContractInfo Get(string ContractNo)
        {
            ContractInfo returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetContractInfo = cmdGetContractInfo.Clone() as MySqlCommand;

            _cmdGetContractInfo.Connection = oc;
            try
            {
                _cmdGetContractInfo.Parameters["@ContractNo"].Value = ContractNo;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetContractInfo.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ContractInfo().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetContractInfo.Dispose();
                _cmdGetContractInfo = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly ContractInfoAccessor instance = new ContractInfoAccessor();
        public static ContractInfoAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
