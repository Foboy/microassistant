/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/14 18:57:26
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
    public class ProProductionAccessor
    {
        private MySqlCommand cmdInsertProProduction;
        private MySqlCommand cmdDeleteProProduction;
        private MySqlCommand cmdUpdateProProduction;
        private MySqlCommand cmdLoadProProduction;
        private MySqlCommand cmdLoadAllProProduction;
        private MySqlCommand cmdLoadProProductionCount;
        private MySqlCommand cmdGetProProduction;

        private MySqlCommand cmdUpdateStockCount;

        private ProProductionAccessor()
        {
            #region cmdInsertProProduction

            cmdInsertProProduction = new MySqlCommand("INSERT INTO pro_production(p_name,p_info,unit,p_type_id,lowest_price,market_price,user_id) values (@PName,@PInfo,@Unit,@PTypeId,@LowestPrice,@MarketPrice,@UserId)");

            cmdInsertProProduction.Parameters.Add("@PName", MySqlDbType.String);
            cmdInsertProProduction.Parameters.Add("@PInfo", MySqlDbType.String);
            cmdInsertProProduction.Parameters.Add("@Unit", MySqlDbType.String);
            cmdInsertProProduction.Parameters.Add("@PTypeId", MySqlDbType.Int32);
            cmdInsertProProduction.Parameters.Add("@LowestPrice", MySqlDbType.Decimal);
            cmdInsertProProduction.Parameters.Add("@MarketPrice", MySqlDbType.Decimal);
            cmdInsertProProduction.Parameters.Add("@UserId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateProProduction

            cmdUpdateProProduction = new MySqlCommand("update pro_production set p_name = @PName,p_info = @PInfo,unit = @Unit,p_type_id = @PTypeId,lowest_price = @LowestPrice,market_price = @MarketPrice,user_id = @UserId where p_id = @PId");
            cmdUpdateProProduction.Parameters.Add("@PId", MySqlDbType.Int32);
            cmdUpdateProProduction.Parameters.Add("@PName", MySqlDbType.String);
            cmdUpdateProProduction.Parameters.Add("@PInfo", MySqlDbType.String);
            cmdUpdateProProduction.Parameters.Add("@Unit", MySqlDbType.String);
            cmdUpdateProProduction.Parameters.Add("@PTypeId", MySqlDbType.Int32);
            cmdUpdateProProduction.Parameters.Add("@LowestPrice", MySqlDbType.Decimal);
            cmdUpdateProProduction.Parameters.Add("@MarketPrice", MySqlDbType.Decimal);
            cmdUpdateProProduction.Parameters.Add("@UserId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteProProduction

            cmdDeleteProProduction = new MySqlCommand("delete from pro_production where p_id = @PId");
            cmdDeleteProProduction.Parameters.Add("@PId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadProProduction

            cmdLoadProProduction = new MySqlCommand(@"select p_id,p_name,p_info,unit,p_type_id,lowest_price,market_price,user_id,stock_count,ent_id from pro_production where (@PName='' or PName=@PName) and (@PTypeId=0 or PTypeId=@PTypeId) and (@UserId=0 or UserId=@UserId) limit @PageIndex,@PageSize");
            cmdLoadProProduction.Parameters.Add("@PName", MySqlDbType.String);
            cmdLoadProProduction.Parameters.Add("@PTypeId", MySqlDbType.Int32);
            cmdLoadProProduction.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdLoadProProduction.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadProProduction.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdLoadProProductionCount

            cmdLoadProProductionCount = new MySqlCommand("select count(*)  from pro_production where (@PName=0 or PName=@PName) and (@PTypeId=0 or PTypeId=@PTypeId) and (@UserId=0 or UserId=@UserId) ");

            #endregion

            #region cmdLoadAllProProduction

            cmdLoadAllProProduction = new MySqlCommand("select p_id,p_name,p_info,unit,p_type_id,lowest_price,market_price,user_id,stock_count,ent_id from pro_production");

            #endregion

            #region cmdGetProProduction

            cmdGetProProduction = new MySqlCommand("select p_id,p_name,p_info,unit,p_type_id,lowest_price,market_price,user_id,stock_count,ent_id from pro_production where p_id = @PId");
            cmdGetProProduction.Parameters.Add("@PId", MySqlDbType.Int32);

            #endregion

            #region cmdUpdateStockCount

            cmdUpdateStockCount = new MySqlCommand("update pro_production set stock_count = @StockCount where p_id = @PId");
            cmdUpdateStockCount.Parameters.Add("@PId", MySqlDbType.Int32);
            cmdUpdateStockCount.Parameters.Add("@StockCount", MySqlDbType.Int32);
            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ProProduction e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertProProduction = cmdInsertProProduction.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertProProduction.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertProProduction.Parameters["@PName"].Value = e.PName;
                _cmdInsertProProduction.Parameters["@PInfo"].Value = e.PInfo;
                _cmdInsertProProduction.Parameters["@Unit"].Value = e.Unit;
                _cmdInsertProProduction.Parameters["@PTypeId"].Value = e.PTypeId;
                _cmdInsertProProduction.Parameters["@LowestPrice"].Value = e.LowestPrice;
                _cmdInsertProProduction.Parameters["@MarketPrice"].Value = e.MarketPrice;
                _cmdInsertProProduction.Parameters["@UserId"].Value = e.UserId;

                _cmdInsertProProduction.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertProProduction.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertProProduction.Dispose();
                _cmdInsertProProduction = null;
            }
        }

        /// <summary>
        /// 删除商品
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int PId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteProProduction = cmdDeleteProProduction.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteProProduction.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteProProduction.Parameters["@PId"].Value = PId;


                returnValue = _cmdDeleteProProduction.ExecuteNonQuery() > 0 ? true : returnValue;
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteProProduction.Dispose();
                _cmdDeleteProProduction = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ProProduction e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateProProduction = cmdUpdateProProduction.Clone() as MySqlCommand;
            _cmdUpdateProProduction.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateProProduction.Parameters["@PId"].Value = e.PId;
                _cmdUpdateProProduction.Parameters["@PName"].Value = e.PName;
                _cmdUpdateProProduction.Parameters["@PInfo"].Value = e.PInfo;
                _cmdUpdateProProduction.Parameters["@Unit"].Value = e.Unit;
                _cmdUpdateProProduction.Parameters["@PTypeId"].Value = e.PTypeId;
                _cmdUpdateProProduction.Parameters["@LowestPrice"].Value = e.LowestPrice;
                _cmdUpdateProProduction.Parameters["@MarketPrice"].Value = e.MarketPrice;
                _cmdUpdateProProduction.Parameters["@UserId"].Value = e.UserId;

                _cmdUpdateProProduction.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateProProduction.Dispose();
                _cmdUpdateProProduction = null;
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
        public PageEntity<ProProduction> Search(String PName, Int32 PTypeId, Int32 UserId, int pageIndex, int pageSize)
        {
            PageEntity<ProProduction> returnValue = new PageEntity<ProProduction>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadProProduction = cmdLoadProProduction.Clone() as MySqlCommand;
            MySqlCommand _cmdLoadProProductionCount = cmdLoadProProductionCount.Clone() as MySqlCommand;
            _cmdLoadProProduction.Connection = oc;
            _cmdLoadProProductionCount.Connection = oc;

            try
            {
                _cmdLoadProProduction.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadProProduction.Parameters["@PageSize"].Value = pageSize;
                //  _cmdLoadProProduction.Parameters["@PId"].Value = PId;
                _cmdLoadProProduction.Parameters["@PName"].Value = PName;
                // _cmdLoadProProduction.Parameters["@PInfo"].Value = PInfo;
                // _cmdLoadProProduction.Parameters["@Unit"].Value = Unit;
                _cmdLoadProProduction.Parameters["@PTypeId"].Value = PTypeId;
                // _cmdLoadProProduction.Parameters["@LowestPrice"].Value = LowestPrice;
                // _cmdLoadProProduction.Parameters["@MarketPrice"].Value = MarketPrice;
                _cmdLoadProProduction.Parameters["@UserId"].Value = UserId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadProProduction.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new ProProduction().BuildSampleEntity(reader));
                }
                _cmdLoadProProductionCount.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadProProductionCount.Parameters["@PageSize"].Value = pageSize;
                // _cmdLoadProProductionCount.Parameters["@PId"].Value = PId;
                _cmdLoadProProductionCount.Parameters["@PName"].Value = PName;
                // _cmdLoadProProductionCount.Parameters["@PInfo"].Value = PInfo;
                // _cmdLoadProProductionCount.Parameters["@Unit"].Value = Unit;
                _cmdLoadProProduction.Parameters["@PTypeId"].Value = PTypeId;
                // _cmdLoadProProductionCount.Parameters["@LowestPrice"].Value = LowestPrice;
                // _cmdLoadProProductionCount.Parameters["@MarketPrice"].Value = MarketPrice;
                _cmdLoadProProductionCount.Parameters["@UserId"].Value = UserId;
                returnValue.RecordsCount = (int)_cmdLoadProProductionCount.ExecuteScalar();
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadProProduction.Dispose();
                _cmdLoadProProduction = null;
                _cmdLoadProProductionCount.Dispose();
                _cmdLoadProProductionCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ProProduction> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllProProduction = cmdLoadAllProProduction.Clone() as MySqlCommand;
            _cmdLoadAllProProduction.Connection = oc; List<ProProduction> returnValue = new List<ProProduction>();
            try
            {
                _cmdLoadAllProProduction.CommandText = string.Format(_cmdLoadAllProProduction.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllProProduction.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ProProduction().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllProProduction.Dispose();
                _cmdLoadAllProProduction = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public ProProduction Get(int PId)
        {
            ProProduction returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetProProduction = cmdGetProProduction.Clone() as MySqlCommand;

            _cmdGetProProduction.Connection = oc;
            try
            {
                _cmdGetProProduction.Parameters["@PId"].Value = PId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetProProduction.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ProProduction().BuildSampleEntity(reader);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetProProduction.Dispose();
                _cmdGetProProduction = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 更新产品库存
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="stockCount"></param>
        public void UpdateStockCount(int pid, int stockCount)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateStockCount = cmdUpdateStockCount.Clone() as MySqlCommand;
            _cmdUpdateStockCount.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateStockCount.Parameters["@PId"].Value = pid;
                _cmdUpdateStockCount.Parameters["@StockCount"].Value = stockCount;

                _cmdUpdateStockCount.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateStockCount.Dispose();
                _cmdUpdateStockCount = null;
                GC.Collect();
            }
        }



        private static readonly ProProductionAccessor instance = new ProProductionAccessor();
        public static ProProductionAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
