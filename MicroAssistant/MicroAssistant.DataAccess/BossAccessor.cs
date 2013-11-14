/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 10:29:43
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
    public class BossAccessor
    {
        private MySqlCommand cmdGetMarketingChanceCount;
        private MySqlCommand cmdGetFirstAndMoreVisitCount;
        private MySqlCommand cmdGetContractInfoCount;
        private MySqlCommand cmdLoadChanceByEntId;
        private MySqlCommand cmdLoadPayOrRecDatas;
        private MySqlCommand cmdDelAllTable;

        private BossAccessor()
        {
            #region cmdGetMarketingChanceCount

            cmdGetMarketingChanceCount = new MySqlCommand(" select count(1)  from marketing_chance where  ent_id = @EntId and @IsVisit=0 or isvisit = @IsVisit and add_time >= @StartTime and add_time < @EndTime ");
            cmdGetMarketingChanceCount.Parameters.Add("@IsVisit", MySqlDbType.Int32);
            cmdGetMarketingChanceCount.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdGetMarketingChanceCount.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdGetMarketingChanceCount.Parameters.Add("@EndTime", MySqlDbType.DateTime);
            #endregion


            #region cmdGetFirstAndMoreVisitCount

            cmdGetFirstAndMoreVisitCount = new MySqlCommand(@" select 
    count(*)
from
    (SELECT 
        *, count(*) num
    FROM
        microassistantdb.marketing_visit
    where
        ent_id = @EntId and visit_time >=@StartTime and visit_time < @EndTime
    group by chance_id) a {0} ");
            cmdGetFirstAndMoreVisitCount.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdGetFirstAndMoreVisitCount.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdGetFirstAndMoreVisitCount.Parameters.Add("@EndTime", MySqlDbType.DateTime);

            #endregion


            #region cmdGetContractInfoCount

            cmdGetContractInfoCount = new MySqlCommand(" select count(*)  from contract_info where ent_id = @EntId and contract_time >= @StartTime and contract_time < @EndTime ");
            cmdGetContractInfoCount.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdGetContractInfoCount.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdGetContractInfoCount.Parameters.Add("@EndTime", MySqlDbType.DateTime);

            #endregion

            #region cmdLoadChanceByEntId

            cmdLoadChanceByEntId = new MySqlCommand(@" select idmarketing_chance,chance_type,customer_type,contact_name,remark,add_time,qq,email,tel,phone,rate,ent_id,user_id,IsVisit from marketing_chance  where  ent_id = @EntId and add_time >= @StartTime and add_time < @EndTime ");
            cmdLoadChanceByEntId.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdLoadChanceByEntId.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdLoadChanceByEntId.Parameters.Add("@EndTime", MySqlDbType.DateTime);

            #endregion

            #region cmdLoadPayOrRecDatas

            cmdLoadPayOrRecDatas = new MySqlCommand(@" SELECT 
    a.amount, a.pay_time ptime, 1 ptype
FROM
    contract_howtopay a
where
    a.isreceived = 1 and a.ent_id = @EntId
        and a.pay_time >= @StartTime
        and a.pay_time < @EndTime 
union SELECT 
    b.amount, b.received_time, 2
FROM
    contract_howtopay b
where
    b.isreceived = 2 and b.ent_id = @EntId
        and b.received_time >= @StartTime
        and b.received_time < @EndTime 
union SELECT 
    c.price * c.p_num, c.create_time, 3
FROM
    pro_producton_detail c
where
    c.is_pay = 1 and c.ent_id = @EntId
        and c.create_time >= @StartTime
        and c.create_time < @EndTime 
union SELECT 
    d.price * d.p_num, d.pay_time, 4
FROM
    pro_producton_detail d
where
    d.is_pay = 2 and d.ent_id = @EntId
        and d.pay_time >= @StartTime
        and d.pay_time < @EndTime
");
            cmdLoadPayOrRecDatas.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdLoadPayOrRecDatas.Parameters.Add("@StartTime", MySqlDbType.DateTime);
            cmdLoadPayOrRecDatas.Parameters.Add("@EndTime", MySqlDbType.DateTime);

            #endregion

            #region cmdDelAllTable

            cmdDelAllTable = new MySqlCommand(@" select idmarketing_chance,chance_type,customer_type,contact_name,remark,add_time,qq,email,tel,phone,rate,ent_id,user_id,IsVisit from marketing_chance  where  ent_id = @EntId and add_time >= @StartTime and add_time < @EndTime ");
            cmdDelAllTable.Parameters.Add("@EntId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 获取销售机会数量
        /// </summary>
        /// <param name="IsVisit"></param>
        /// <returns></returns>
        public int GetMarketingChanceCount(int entId,int IsVisit,DateTime startTime,DateTime endTime)
        {
            int returnValue =0;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetMarketingChanceCount = cmdGetMarketingChanceCount.Clone() as MySqlCommand;
            _cmdGetMarketingChanceCount.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdGetMarketingChanceCount.Parameters["@IsVisit"].Value = IsVisit;
                _cmdGetMarketingChanceCount.Parameters["@EntId"].Value = entId;
                _cmdGetMarketingChanceCount.Parameters["@StartTime"].Value = startTime;
                _cmdGetMarketingChanceCount.Parameters["@EndTime"].Value = endTime;

                returnValue = Convert.ToInt32(_cmdGetMarketingChanceCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetMarketingChanceCount.Dispose();
                _cmdGetMarketingChanceCount = null;
                GC.Collect();
            }
            return returnValue;

        }


        /// <summary>
        /// 获取老板模式 vcountType 1:获取多次拜访数量，2：获取初次拜访数量 3: 获取有报价的机会数
        /// </summary>
        /// <param name="entId"></param>
        /// <param name="vcountType">1:获取多次拜访数量，2：获取初次拜访数量 3: 获取有报价的机会数</param>
        /// <returns></returns>
        public int GetFirstAndMoreVisitCount(Int32 entId, int vcountType, DateTime startTime, DateTime endTime)
        {
            int returnValue = 0;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetFirstAndMoreVisitCount = cmdGetFirstAndMoreVisitCount.Clone() as MySqlCommand;
            _cmdGetFirstAndMoreVisitCount.Connection = oc;

            try
            {
                string strsql = string.Empty;
                switch (vcountType)
                {
                    case 1:
                        strsql = " where a.num > 1 ";
                        break;
                    case 2:
                        strsql = " where a.num = 1 ";
                        break;
                    case 3:
                        strsql = " where a.amount > 0 ";
                        break;
                    default:
                        strsql = string.Empty;
                        break;
                }
                _cmdGetFirstAndMoreVisitCount.CommandText = string.Format(_cmdGetFirstAndMoreVisitCount.CommandText, strsql);

                _cmdGetFirstAndMoreVisitCount.Parameters["@EntId"].Value = entId;
                _cmdGetFirstAndMoreVisitCount.Parameters["@StartTime"].Value = startTime;
                _cmdGetFirstAndMoreVisitCount.Parameters["@EndTime"].Value = endTime;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                returnValue = Convert.ToInt32(_cmdGetFirstAndMoreVisitCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetFirstAndMoreVisitCount.Dispose();
                _cmdGetFirstAndMoreVisitCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        public int GetContractInfoCount(Int32 EntId, DateTime startTime, DateTime endTime)
        {
            int returnValue = 0;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetContractInfoCount = cmdGetContractInfoCount.Clone() as MySqlCommand;
            _cmdGetContractInfoCount.Connection = oc;

            try
            {

                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdGetContractInfoCount.Parameters["@EntId"].Value = EntId;
                _cmdGetContractInfoCount.Parameters["@StartTime"].Value = startTime;
                _cmdGetContractInfoCount.Parameters["@EndTime"].Value = endTime;
                returnValue = Convert.ToInt32(_cmdGetContractInfoCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetContractInfoCount.Dispose();
                _cmdGetContractInfoCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        public List<MarketingChance> LoadChanceByEntId(int entId, DateTime startTime, DateTime endTime)
        {
            List<MarketingChance> returnValue = new List<MarketingChance>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadChanceByEntId = cmdLoadChanceByEntId.Clone() as MySqlCommand;
            _cmdLoadChanceByEntId.Connection = oc;

            try
            {
                _cmdLoadChanceByEntId.Parameters["@EntId"].Value = entId;
                _cmdLoadChanceByEntId.Parameters["@StartTime"].Value = startTime;
                _cmdLoadChanceByEntId.Parameters["@EndTime"].Value = endTime;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadChanceByEntId.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new MarketingChance().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadChanceByEntId.Dispose();
                _cmdLoadChanceByEntId = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取企业收付款详细日志
        /// </summary>
        /// <param name="entId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<BossFinancial> LoadBossFinancialList(int entId, DateTime startTime, DateTime endTime)
        {
            List<BossFinancial> returnValue = new List<BossFinancial>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadPayOrRecDatas = cmdLoadPayOrRecDatas.Clone() as MySqlCommand;
            _cmdLoadPayOrRecDatas.Connection = oc;

            try
            {
                _cmdLoadPayOrRecDatas.Parameters["@EntId"].Value = entId;
                _cmdLoadPayOrRecDatas.Parameters["@StartTime"].Value = startTime;
                _cmdLoadPayOrRecDatas.Parameters["@EndTime"].Value = endTime;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadPayOrRecDatas.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new BossFinancial().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadPayOrRecDatas.Dispose();
                _cmdLoadPayOrRecDatas = null;
                GC.Collect();
            }
            return returnValue;

        }

        private static readonly BossAccessor instance = new BossAccessor();
        public static BossAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
