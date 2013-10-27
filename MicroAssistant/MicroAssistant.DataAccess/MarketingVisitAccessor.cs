/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 10:29:59
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
    public class MarketingVisitAccessor
    {
        private MySqlCommand cmdInsertMarketingVisit;
        private MySqlCommand cmdDeleteMarketingVisit;
        private MySqlCommand cmdUpdateMarketingVisit;
        private MySqlCommand cmdLoadMarketingVisit;
        private MySqlCommand cmdLoadAllMarketingVisit;
        private MySqlCommand cmdGetMarketingVisitCount;
        private MySqlCommand cmdGetMarketingVisit;

        private MarketingVisitAccessor()
        {
            #region cmdInsertMarketingVisit

            cmdInsertMarketingVisit = new MySqlCommand("INSERT INTO marketing_visit(visit_type,amount,address,remark,visit_time,chance_id) values (@VisitType,@Amount,@Address,@Remark,@VisitTime,@ChanceId)");

            cmdInsertMarketingVisit.Parameters.Add("@VisitType", MySqlDbType.Int32);
            cmdInsertMarketingVisit.Parameters.Add("@Amount", MySqlDbType.Decimal);
            cmdInsertMarketingVisit.Parameters.Add("@Address", MySqlDbType.String);
            cmdInsertMarketingVisit.Parameters.Add("@Remark", MySqlDbType.String);
            cmdInsertMarketingVisit.Parameters.Add("@VisitTime", MySqlDbType.DateTime);
            cmdInsertMarketingVisit.Parameters.Add("@ChanceId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateMarketingVisit

            cmdUpdateMarketingVisit = new MySqlCommand(" update marketing_visit set visit_type = @VisitType,amount = @Amount,address = @Address,remark = @Remark,visit_time = @VisitTime,chance_id = @ChanceId where idmarketing_visit = @IdmarketingVisit");
            cmdUpdateMarketingVisit.Parameters.Add("@IdmarketingVisit", MySqlDbType.Int32);
            cmdUpdateMarketingVisit.Parameters.Add("@VisitType", MySqlDbType.Int32);
            cmdUpdateMarketingVisit.Parameters.Add("@Amount", MySqlDbType.Decimal);
            cmdUpdateMarketingVisit.Parameters.Add("@Address", MySqlDbType.String);
            cmdUpdateMarketingVisit.Parameters.Add("@Remark", MySqlDbType.String);
            cmdUpdateMarketingVisit.Parameters.Add("@VisitTime", MySqlDbType.DateTime);
            cmdUpdateMarketingVisit.Parameters.Add("@ChanceId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteMarketingVisit

            cmdDeleteMarketingVisit = new MySqlCommand(" delete from marketing_visit where idmarketing_visit = @IdmarketingVisit");
            cmdDeleteMarketingVisit.Parameters.Add("@IdmarketingVisit", MySqlDbType.Int32);
            #endregion

            #region cmdLoadMarketingVisit

            cmdLoadMarketingVisit = new MySqlCommand(@" select idmarketing_visit,visit_type,amount,address,remark,visit_time,chance_id from marketing_visit where chance_id=@ChanceId order by visit_time desc limit @PageIndex,@PageSize");
            cmdLoadMarketingVisit.Parameters.Add("@ChanceId", MySqlDbType.Int32);
            cmdLoadMarketingVisit.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadMarketingVisit.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetMarketingVisitCount

            cmdGetMarketingVisitCount = new MySqlCommand(" select count(*)  from marketing_visit ");

            #endregion

            #region cmdLoadAllMarketingVisit

            cmdLoadAllMarketingVisit = new MySqlCommand(" select idmarketing_visit,visit_type,amount,address,remark,visit_time,chance_id from marketing_visit");

            #endregion

            #region cmdGetMarketingVisit

            cmdGetMarketingVisit = new MySqlCommand(" select idmarketing_visit,visit_type,amount,address,remark,visit_time,chance_id from marketing_visit where idmarketing_visit = @IdmarketingVisit");
            cmdGetMarketingVisit.Parameters.Add("@IdmarketingVisit", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(MarketingVisit e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertMarketingVisit = cmdInsertMarketingVisit.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertMarketingVisit.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertMarketingVisit.Parameters["@VisitType"].Value = e.VisitType;
                _cmdInsertMarketingVisit.Parameters["@Amount"].Value = e.Amount;
                _cmdInsertMarketingVisit.Parameters["@Address"].Value = e.Address;
                _cmdInsertMarketingVisit.Parameters["@Remark"].Value = e.Remark;
                _cmdInsertMarketingVisit.Parameters["@VisitTime"].Value = e.VisitTime;
                _cmdInsertMarketingVisit.Parameters["@ChanceId"].Value = e.ChanceId;

                _cmdInsertMarketingVisit.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertMarketingVisit.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertMarketingVisit.Dispose();
                _cmdInsertMarketingVisit = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int IdmarketingVisit)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteMarketingVisit = cmdDeleteMarketingVisit.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteMarketingVisit.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteMarketingVisit.Parameters["@IdmarketingVisit"].Value = IdmarketingVisit;


                _cmdDeleteMarketingVisit.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteMarketingVisit.Dispose();
                _cmdDeleteMarketingVisit = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(MarketingVisit e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateMarketingVisit = cmdUpdateMarketingVisit.Clone() as MySqlCommand;
            _cmdUpdateMarketingVisit.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateMarketingVisit.Parameters["@IdmarketingVisit"].Value = e.IdmarketingVisit;
                _cmdUpdateMarketingVisit.Parameters["@VisitType"].Value = e.VisitType;
                _cmdUpdateMarketingVisit.Parameters["@Amount"].Value = e.Amount;
                _cmdUpdateMarketingVisit.Parameters["@Address"].Value = e.Address;
                _cmdUpdateMarketingVisit.Parameters["@Remark"].Value = e.Remark;
                _cmdUpdateMarketingVisit.Parameters["@VisitTime"].Value = e.VisitTime;
                _cmdUpdateMarketingVisit.Parameters["@ChanceId"].Value = e.ChanceId;

                _cmdUpdateMarketingVisit.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateMarketingVisit.Dispose();
                _cmdUpdateMarketingVisit = null;
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
        public PageEntity<MarketingVisit> Search( Int32 ChanceId, int pageIndex, int pageSize)
        {
            PageEntity<MarketingVisit> returnValue = new PageEntity<MarketingVisit>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadMarketingVisit = cmdLoadMarketingVisit.Clone() as MySqlCommand;
            MySqlCommand _cmdGetMarketingVisitCount = cmdGetMarketingVisitCount.Clone() as MySqlCommand;
            _cmdLoadMarketingVisit.Connection = oc;
            _cmdGetMarketingVisitCount.Connection = oc;

            try
            {
                _cmdLoadMarketingVisit.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadMarketingVisit.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadMarketingVisit.Parameters["@ChanceId"].Value = ChanceId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadMarketingVisit.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new MarketingVisit().BuildSampleEntity(reader));
                }
                reader.Close();
                returnValue.RecordsCount = Convert.ToInt32( _cmdGetMarketingVisitCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadMarketingVisit.Dispose();
                _cmdLoadMarketingVisit = null;
                _cmdGetMarketingVisitCount.Dispose();
                _cmdGetMarketingVisitCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<MarketingVisit> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllMarketingVisit = cmdLoadAllMarketingVisit.Clone() as MySqlCommand;
            _cmdLoadAllMarketingVisit.Connection = oc; List<MarketingVisit> returnValue = new List<MarketingVisit>();
            try
            {
                _cmdLoadAllMarketingVisit.CommandText = string.Format(_cmdLoadAllMarketingVisit.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllMarketingVisit.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new MarketingVisit().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllMarketingVisit.Dispose();
                _cmdLoadAllMarketingVisit = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public MarketingVisit Get(int IdmarketingVisit)
        {
            MarketingVisit returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetMarketingVisit = cmdGetMarketingVisit.Clone() as MySqlCommand;

            _cmdGetMarketingVisit.Connection = oc;
            try
            {
                _cmdGetMarketingVisit.Parameters["@IdmarketingVisit"].Value = IdmarketingVisit;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetMarketingVisit.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new MarketingVisit().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetMarketingVisit.Dispose();
                _cmdGetMarketingVisit = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly MarketingVisitAccessor instance = new MarketingVisitAccessor();
        public static MarketingVisitAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
