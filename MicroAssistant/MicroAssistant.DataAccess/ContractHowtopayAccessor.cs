/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 14:27:38
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
    public class ContractHowtopayAccessor
    {
        private MySqlCommand cmdInsertContractHowtopay;
        private MySqlCommand cmdDeleteContractHowtopay;
        private MySqlCommand cmdUpdateContractHowtopay;
        private MySqlCommand cmdLoadContractHowtopay;
        private MySqlCommand cmdLoadAllContractHowtopay;
        private MySqlCommand cmdGetContractHowtopayCount;
        private MySqlCommand cmdGetContractHowtopay;
        private MySqlCommand cmdUpdateIsreceived;

        private ContractHowtopayAccessor()
        {
            #region cmdInsertContractHowtopay

            cmdInsertContractHowtopay = new MySqlCommand("INSERT INTO contract_howtopay(instalments_no,amount,pay_time,IsReceived,contract_no,ent_id) values (@InstalmentsNo,@Amount,@PayTime,@Isreceived,@ContractNo,@EntId)");

            cmdInsertContractHowtopay.Parameters.Add("@InstalmentsNo", MySqlDbType.Int32);
            cmdInsertContractHowtopay.Parameters.Add("@Amount", MySqlDbType.Decimal);
            cmdInsertContractHowtopay.Parameters.Add("@PayTime", MySqlDbType.DateTime);
           // cmdInsertContractHowtopay.Parameters.Add("@ReceivedTime", MySqlDbType.DateTime);
            cmdInsertContractHowtopay.Parameters.Add("@Isreceived", MySqlDbType.Int32);
            cmdInsertContractHowtopay.Parameters.Add("@ContractNo", MySqlDbType.String);
            cmdInsertContractHowtopay.Parameters.Add("@EntId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateContractHowtopay

            cmdUpdateContractHowtopay = new MySqlCommand(" update contract_howtopay set ent_id=@EntId, instalments_no = @InstalmentsNo,amount = @Amount,pay_time = @PayTime,received_time = @ReceivedTime,IsReceived = @Isreceived,contract_no = @ContractNo where howtopay_id = @HowtopayId");
            cmdUpdateContractHowtopay.Parameters.Add("@HowtopayId", MySqlDbType.Int32);
            cmdUpdateContractHowtopay.Parameters.Add("@InstalmentsNo", MySqlDbType.Int32);
            cmdUpdateContractHowtopay.Parameters.Add("@Amount", MySqlDbType.Decimal);
            cmdUpdateContractHowtopay.Parameters.Add("@PayTime", MySqlDbType.DateTime);
            cmdUpdateContractHowtopay.Parameters.Add("@ReceivedTime", MySqlDbType.DateTime);
            cmdUpdateContractHowtopay.Parameters.Add("@Isreceived", MySqlDbType.Int32);
            cmdUpdateContractHowtopay.Parameters.Add("@ContractNo", MySqlDbType.String);
            cmdUpdateContractHowtopay.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteContractHowtopay

            cmdDeleteContractHowtopay = new MySqlCommand(" delete from contract_howtopay where howtopay_id = @HowtopayId");
            cmdDeleteContractHowtopay.Parameters.Add("@HowtopayId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadContractHowtopay

            cmdLoadContractHowtopay = new MySqlCommand(@" select ent_id,howtopay_id,instalments_no,amount,pay_time,received_time,IsReceived,contract_no from contract_howtopay where contract_no = @ContractNo and (IsReceived = @IsReceived or @IsReceived = 0 ) order by pay_time ");
            cmdLoadContractHowtopay.Parameters.Add("@ContractNo", MySqlDbType.String);
            cmdLoadContractHowtopay.Parameters.Add("@IsReceived", MySqlDbType.Int32);

            #endregion

            #region cmdGetContractHowtopayCount

            cmdGetContractHowtopayCount = new MySqlCommand(" select count(*)  from contract_howtopay ");

            #endregion

            #region cmdLoadAllContractHowtopay

            cmdLoadAllContractHowtopay = new MySqlCommand(" select ent_id,howtopay_id,instalments_no,amount,pay_time,received_time,IsReceived,contract_no from contract_howtopay");

            #endregion

            #region cmdGetContractHowtopay

            cmdGetContractHowtopay = new MySqlCommand(" select ent_id,howtopay_id,instalments_no,amount,pay_time,received_time,IsReceived,contract_no from contract_howtopay where howtopay_id = @HowtopayId");
            cmdGetContractHowtopay.Parameters.Add("@HowtopayId", MySqlDbType.Int32);

            #endregion

            #region cmdUpdateIsreceived

            cmdUpdateIsreceived = new MySqlCommand(" update contract_howtopay set IsReceived = @Isreceived,received_time =@ReceivedTime  where instalments_no = @InstalmentsNo and contract_no = @ContractNo");

            cmdUpdateIsreceived.Parameters.Add("@InstalmentsNo", MySqlDbType.Int32);
            cmdUpdateIsreceived.Parameters.Add("@Isreceived", MySqlDbType.Int32);
            cmdUpdateIsreceived.Parameters.Add("@ReceivedTime", MySqlDbType.DateTime);
            cmdUpdateIsreceived.Parameters.Add("@ContractNo", MySqlDbType.String);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ContractHowtopay e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertContractHowtopay = cmdInsertContractHowtopay.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertContractHowtopay.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertContractHowtopay.Parameters["@InstalmentsNo"].Value = e.InstalmentsNo;
                _cmdInsertContractHowtopay.Parameters["@Amount"].Value = e.Amount;
                _cmdInsertContractHowtopay.Parameters["@PayTime"].Value = e.PayTime;
               // _cmdInsertContractHowtopay.Parameters["@ReceivedTime"].Value = e.ReceivedTime;
                _cmdInsertContractHowtopay.Parameters["@Isreceived"].Value = e.Isreceived;
                _cmdInsertContractHowtopay.Parameters["@ContractNo"].Value = e.ContractNo;
                _cmdInsertContractHowtopay.Parameters["@EntId"].Value = e.EntId;

                _cmdInsertContractHowtopay.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertContractHowtopay.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertContractHowtopay.Dispose();
                _cmdInsertContractHowtopay = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int HowtopayId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteContractHowtopay = cmdDeleteContractHowtopay.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteContractHowtopay.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteContractHowtopay.Parameters["@HowtopayId"].Value = HowtopayId;


                _cmdDeleteContractHowtopay.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteContractHowtopay.Dispose();
                _cmdDeleteContractHowtopay = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ContractHowtopay e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateContractHowtopay = cmdUpdateContractHowtopay.Clone() as MySqlCommand;
            _cmdUpdateContractHowtopay.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateContractHowtopay.Parameters["@HowtopayId"].Value = e.HowtopayId;
                _cmdUpdateContractHowtopay.Parameters["@InstalmentsNo"].Value = e.InstalmentsNo;
                _cmdUpdateContractHowtopay.Parameters["@Amount"].Value = e.Amount;
                _cmdUpdateContractHowtopay.Parameters["@PayTime"].Value = e.PayTime;
                _cmdUpdateContractHowtopay.Parameters["@ReceivedTime"].Value = e.ReceivedTime;
                _cmdUpdateContractHowtopay.Parameters["@Isreceived"].Value = e.Isreceived;
                _cmdUpdateContractHowtopay.Parameters["@ContractNo"].Value = e.ContractNo;
                _cmdUpdateContractHowtopay.Parameters["@EntId"].Value = e.EntId ;

                _cmdUpdateContractHowtopay.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateContractHowtopay.Dispose();
                _cmdUpdateContractHowtopay = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 根据条件分页获取指定数据
        /// <param name="IsReceived">1:没确认收款 2：已收款</param>
        /// <param name="ContractNo">合同编号</param>
        /// </summary>
        public List<ContractHowtopay> Search(String ContractNo, int IsReceived)
        {
            List<ContractHowtopay> returnValue = new List<ContractHowtopay>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadContractHowtopay = cmdLoadContractHowtopay.Clone() as MySqlCommand;
            _cmdLoadContractHowtopay.Connection = oc;

            try
            {
                _cmdLoadContractHowtopay.Parameters["@ContractNo"].Value = ContractNo;
                _cmdLoadContractHowtopay.Parameters["@IsReceived"].Value = IsReceived;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadContractHowtopay.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ContractHowtopay().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadContractHowtopay.Dispose();
                _cmdLoadContractHowtopay = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ContractHowtopay> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllContractHowtopay = cmdLoadAllContractHowtopay.Clone() as MySqlCommand;
            _cmdLoadAllContractHowtopay.Connection = oc; List<ContractHowtopay> returnValue = new List<ContractHowtopay>();
            try
            {
                _cmdLoadAllContractHowtopay.CommandText = string.Format(_cmdLoadAllContractHowtopay.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllContractHowtopay.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ContractHowtopay().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllContractHowtopay.Dispose();
                _cmdLoadAllContractHowtopay = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public ContractHowtopay Get(int HowtopayId)
        {
            ContractHowtopay returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetContractHowtopay = cmdGetContractHowtopay.Clone() as MySqlCommand;

            _cmdGetContractHowtopay.Connection = oc;
            try
            {
                _cmdGetContractHowtopay.Parameters["@HowtopayId"].Value = HowtopayId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetContractHowtopay.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ContractHowtopay().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetContractHowtopay.Dispose();
                _cmdGetContractHowtopay = null;
                GC.Collect();
            }
            return returnValue;

        }

     /// <summary>
     /// 修改应收款状态
     /// </summary>
     /// <param name="contractNo">合同编号</param>
     /// <param name="rNum">收款序号</param>
        /// <param name="isReceived">1:没确认收款 2：已收款</param>
        public void UpdateIsReceived(string contractNo, int rNum, int isReceived)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateIsreceived = cmdUpdateIsreceived.Clone() as MySqlCommand;
            _cmdUpdateIsreceived.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateIsreceived.Parameters["@InstalmentsNo"].Value = rNum;
                _cmdUpdateIsreceived.Parameters["@Isreceived"].Value = isReceived;
                _cmdUpdateIsreceived.Parameters["@ContractNo"].Value = contractNo;
                _cmdUpdateIsreceived.Parameters["@ReceivedTime"].Value = DateTime.Now;

                int i = _cmdUpdateIsreceived.ExecuteNonQuery();

            }
            catch(Exception e)
            {
 
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateIsreceived.Dispose();
                _cmdUpdateIsreceived = null;
                GC.Collect();
            }
        }


        private static readonly ContractHowtopayAccessor instance = new ContractHowtopayAccessor();
        public static ContractHowtopayAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
