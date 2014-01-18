/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/12/28 12:33:06
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
    public class MarketingAfterAccessor
    {
        private MySqlCommand cmdInsertMarketingAfter;
        private MySqlCommand cmdDeleteMarketingAfter;
        private MySqlCommand cmdUpdateMarketingAfter;
        private MySqlCommand cmdLoadMarketingAfter;
        private MySqlCommand cmdLoadAllMarketingAfter;
        private MySqlCommand cmdGetMarketingAfterCount;
        private MySqlCommand cmdGetMarketingAfter;

        private MarketingAfterAccessor()
        {
            #region cmdInsertMarketingAfter

            cmdInsertMarketingAfter = new MySqlCommand("INSERT INTO marketing_after(chance_id,contract_id,detail,ent_id,add_time,op_user_id,op_user_name) values (@ChanceId,@ContractId,@Detail,@EntId,@AddTime,@OpUserId,@OpUserName)");

            cmdInsertMarketingAfter.Parameters.Add("@ChanceId", MySqlDbType.Int32);
            cmdInsertMarketingAfter.Parameters.Add("@ContractId", MySqlDbType.Int32);
            cmdInsertMarketingAfter.Parameters.Add("@Detail", MySqlDbType.String);
            cmdInsertMarketingAfter.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertMarketingAfter.Parameters.Add("@AddTime", MySqlDbType.DateTime);
            cmdInsertMarketingAfter.Parameters.Add("@OpUserId", MySqlDbType.String);
            cmdInsertMarketingAfter.Parameters.Add("@OpUserName", MySqlDbType.String);
            #endregion

            #region cmdUpdateMarketingAfter

            cmdUpdateMarketingAfter = new MySqlCommand(" update marketing_after set chance_id = @ChanceId,contract_id = @ContractId,detail = @Detail,ent_id = @EntId,add_time = @AddTime,op_user_id = @OpUserId,op_user_name = @OpUserName where idmarketing_after = @IdmarketingAfter");
            cmdUpdateMarketingAfter.Parameters.Add("@IdmarketingAfter", MySqlDbType.Int32);
            cmdUpdateMarketingAfter.Parameters.Add("@ChanceId", MySqlDbType.Int32);
            cmdUpdateMarketingAfter.Parameters.Add("@ContractId", MySqlDbType.Int32);
            cmdUpdateMarketingAfter.Parameters.Add("@Detail", MySqlDbType.String);
            cmdUpdateMarketingAfter.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateMarketingAfter.Parameters.Add("@AddTime", MySqlDbType.DateTime);
            cmdUpdateMarketingAfter.Parameters.Add("@OpUserId", MySqlDbType.String);
            cmdUpdateMarketingAfter.Parameters.Add("@OpUserName", MySqlDbType.String);

            #endregion

            #region cmdDeleteMarketingAfter

            cmdDeleteMarketingAfter = new MySqlCommand(" delete from marketing_after where idmarketing_after = @IdmarketingAfter");
            cmdDeleteMarketingAfter.Parameters.Add("@IdmarketingAfter", MySqlDbType.Int32);
            #endregion

            #region cmdLoadMarketingAfter

            cmdLoadMarketingAfter = new MySqlCommand(@" select idmarketing_after,chance_id,contract_id,detail,ent_id,add_time,op_user_id,op_user_name from marketing_after limit @PageIndex,@PageSize");
            cmdLoadMarketingAfter.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadMarketingAfter.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetMarketingAfterCount

            cmdGetMarketingAfterCount = new MySqlCommand(" select count(*)  from marketing_after ");

            #endregion

            #region cmdLoadAllMarketingAfter

            cmdLoadAllMarketingAfter = new MySqlCommand(" select idmarketing_after,chance_id,contract_id,detail,ent_id,add_time,op_user_id,op_user_name from marketing_after");

            #endregion

            #region cmdGetMarketingAfter

            cmdGetMarketingAfter = new MySqlCommand(" select idmarketing_after,chance_id,contract_id,detail,ent_id,add_time,op_user_id,op_user_name from marketing_after where idmarketing_after = @IdmarketingAfter");
            cmdGetMarketingAfter.Parameters.Add("@IdmarketingAfter", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(MarketingAfter e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertMarketingAfter = cmdInsertMarketingAfter.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertMarketingAfter.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertMarketingAfter.Parameters["@ChanceId"].Value = e.ChanceId;
                _cmdInsertMarketingAfter.Parameters["@ContractId"].Value = e.ContractId;
                _cmdInsertMarketingAfter.Parameters["@Detail"].Value = e.Detail;
                _cmdInsertMarketingAfter.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertMarketingAfter.Parameters["@AddTime"].Value = e.AddTime;
                _cmdInsertMarketingAfter.Parameters["@OpUserId"].Value = e.OpUserId;
                _cmdInsertMarketingAfter.Parameters["@OpUserName"].Value = e.OpUserName;

                _cmdInsertMarketingAfter.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertMarketingAfter.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertMarketingAfter.Dispose();
                _cmdInsertMarketingAfter = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int IdmarketingAfter)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteMarketingAfter = cmdDeleteMarketingAfter.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteMarketingAfter.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteMarketingAfter.Parameters["@IdmarketingAfter"].Value = IdmarketingAfter;


                _cmdDeleteMarketingAfter.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteMarketingAfter.Dispose();
                _cmdDeleteMarketingAfter = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(MarketingAfter e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateMarketingAfter = cmdUpdateMarketingAfter.Clone() as MySqlCommand;
            _cmdUpdateMarketingAfter.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateMarketingAfter.Parameters["@IdmarketingAfter"].Value = e.IdmarketingAfter;
                _cmdUpdateMarketingAfter.Parameters["@ChanceId"].Value = e.ChanceId;
                _cmdUpdateMarketingAfter.Parameters["@ContractId"].Value = e.ContractId;
                _cmdUpdateMarketingAfter.Parameters["@Detail"].Value = e.Detail;
                _cmdUpdateMarketingAfter.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateMarketingAfter.Parameters["@AddTime"].Value = e.AddTime;
                _cmdUpdateMarketingAfter.Parameters["@OpUserId"].Value = e.OpUserId;
                _cmdUpdateMarketingAfter.Parameters["@OpUserName"].Value = e.OpUserName;

                _cmdUpdateMarketingAfter.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateMarketingAfter.Dispose();
                _cmdUpdateMarketingAfter = null;
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
        public PageEntity<MarketingAfter> Search(Int32 IdmarketingAfter, Int32 ChanceId, Int32 ContractId, String Detail, Int32 EntId, DateTime AddTime, String OpUserId, String OpUserName, int pageIndex, int pageSize)
        {
            PageEntity<MarketingAfter> returnValue = new PageEntity<MarketingAfter>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadMarketingAfter = cmdLoadMarketingAfter.Clone() as MySqlCommand;
            MySqlCommand _cmdGetMarketingAfterCount = cmdGetMarketingAfterCount.Clone() as MySqlCommand;
            _cmdLoadMarketingAfter.Connection = oc;
            _cmdGetMarketingAfterCount.Connection = oc;

            try
            {
                _cmdLoadMarketingAfter.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadMarketingAfter.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadMarketingAfter.Parameters["@IdmarketingAfter"].Value = IdmarketingAfter;
                _cmdLoadMarketingAfter.Parameters["@ChanceId"].Value = ChanceId;
                _cmdLoadMarketingAfter.Parameters["@ContractId"].Value = ContractId;
                _cmdLoadMarketingAfter.Parameters["@Detail"].Value = Detail;
                _cmdLoadMarketingAfter.Parameters["@EntId"].Value = EntId;
                _cmdLoadMarketingAfter.Parameters["@AddTime"].Value = AddTime;
                _cmdLoadMarketingAfter.Parameters["@OpUserId"].Value = OpUserId;
                _cmdLoadMarketingAfter.Parameters["@OpUserName"].Value = OpUserName;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadMarketingAfter.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new MarketingAfter().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetMarketingAfterCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadMarketingAfter.Dispose();
                _cmdLoadMarketingAfter = null;
                _cmdGetMarketingAfterCount.Dispose();
                _cmdGetMarketingAfterCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<MarketingAfter> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllMarketingAfter = cmdLoadAllMarketingAfter.Clone() as MySqlCommand;
            _cmdLoadAllMarketingAfter.Connection = oc; List<MarketingAfter> returnValue = new List<MarketingAfter>();
            try
            {
                _cmdLoadAllMarketingAfter.CommandText = string.Format(_cmdLoadAllMarketingAfter.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllMarketingAfter.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new MarketingAfter().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllMarketingAfter.Dispose();
                _cmdLoadAllMarketingAfter = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public MarketingAfter Get(int IdmarketingAfter)
        {
            MarketingAfter returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetMarketingAfter = cmdGetMarketingAfter.Clone() as MySqlCommand;

            _cmdGetMarketingAfter.Connection = oc;
            try
            {
                _cmdGetMarketingAfter.Parameters["@IdmarketingAfter"].Value = IdmarketingAfter;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetMarketingAfter.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new MarketingAfter().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetMarketingAfter.Dispose();
                _cmdGetMarketingAfter = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly MarketingAfterAccessor instance = new MarketingAfterAccessor();
        public static MarketingAfterAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
