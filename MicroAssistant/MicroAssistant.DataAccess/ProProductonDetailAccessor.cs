/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/14 18:59:02
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
        private MySqlCommand cmdLoadProProductonDetailCount;
        private MySqlCommand cmdLoadAllProProductonDetail;
        private MySqlCommand cmdGetProProductonDetail;

        private ProProductonDetailAccessor()
        {
            #region cmdInsertProProductonDetail

            cmdInsertProProductonDetail = new MySqlCommand("INSERT INTO pro_producton_detail(price,p_num,p_code,create_time,user_id,pid) values (@Price,@PNum,@PCode,@CreateTime,@UserId,@PId)");

            cmdInsertProProductonDetail.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdInsertProProductonDetail.Parameters.Add("@PNum", MySqlDbType.Int32);
            cmdInsertProProductonDetail.Parameters.Add("@PCode", MySqlDbType.String);
            cmdInsertProProductonDetail.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdInsertProProductonDetail.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdInsertProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateProProductonDetail

            cmdUpdateProProductonDetail = new MySqlCommand(" update pro_producton_detail set p_d_id = @PDId,price = @Price,p_num = @PNum,p_code = @PCode,create_time = @CreateTime,user_id = @UserId,pid=@PId where p_d_id = @PDId");

            cmdUpdateProProductonDetail.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdUpdateProProductonDetail.Parameters.Add("@PNum", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@PCode", MySqlDbType.String);
            cmdUpdateProProductonDetail.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdUpdateProProductonDetail.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteProProductonDetail

            cmdDeleteProProductonDetail = new MySqlCommand("delete from pro_producton_detail where p_d_id = @PId");
            cmdDeleteProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadProProductonDetail

            cmdLoadProProductonDetail = new MySqlCommand(@"select p_d_id,price,p_num,p_code,create_time,user_id,pid from pro_producton_detail where (@PDId=0 or p_d_id=@PDId) and (@Price=0 or price=@Price) and (@PNum=0 or p_num=@PNum) and (@PCode='' or p_code=@PCode) and (@UserId=0 or user_id=@UserId) and (@PId=0 or pid=@PId ) {0} {1} limit @PageIndex,@PageSize");

            cmdLoadProProductonDetail.Parameters.Add("@PageIndex", MySqlDbType.Int32);
            cmdLoadProProductonDetail.Parameters.Add("@PageSize", MySqlDbType.Int32);
            cmdLoadProProductonDetail.Parameters.Add("@PDId", MySqlDbType.Int32);
            cmdLoadProProductonDetail.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdLoadProProductonDetail.Parameters.Add("@PNum", MySqlDbType.Int32);
            cmdLoadProProductonDetail.Parameters.Add("@PCode", MySqlDbType.String);
            //  cmdLoadProProductonDetail.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdLoadProProductonDetail.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdLoadProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);

            #endregion

            #region cmdLoadProProductonDetailCount

            cmdLoadProProductonDetailCount = new MySqlCommand(@"select count(*) from pro_producton_detail where (@PDId =0 or p_d_id=@PDId) and (@Price=0 or price=@Price) and (@PNum=0 or p_num=@PNum) and (@PCode='' or p_code=@PCode) and (@UserId=0 or user_id=@UserId) and (@PId=0 or pid=@PId){0} {1}");
            cmdLoadProProductonDetailCount.Parameters.Add("@PDId", MySqlDbType.Int32);
            cmdLoadProProductonDetailCount.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdLoadProProductonDetailCount.Parameters.Add("@PNum", MySqlDbType.Int32);
            cmdLoadProProductonDetailCount.Parameters.Add("@PCode", MySqlDbType.String);
            cmdLoadProProductonDetailCount.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdLoadProProductonDetailCount.Parameters.Add("@PId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadAllProProductonDetail

            cmdLoadAllProProductonDetail = new MySqlCommand("select p_d_id,price,p_num,p_code,create_time,user_id from pro_producton_detail");

            #endregion

            #region cmdGetProProductonDetail

            cmdGetProProductonDetail = new MySqlCommand("select p_d_id,price,p_num,p_code,create_time,user_id,pid from pro_producton_detail where p_d_id = @PId");
            cmdGetProProductonDetail.Parameters.Add("@PId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(ProProductonDetail e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertProProductonDetail = cmdInsertProProductonDetail.Clone() as MySqlCommand;
            bool returnValue = false;
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
                _cmdInsertProProductonDetail.Parameters["@PId"].Value = e.Pid;
                returnValue = _cmdInsertProProductonDetail.ExecuteNonQuery() > 0 ? true : returnValue;
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
                _cmdDeleteProProductonDetail.Parameters["@PId"].Value = PDId;


                returnValue = _cmdDeleteProProductonDetail.ExecuteNonQuery() > 0 ? true : returnValue;
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
                _cmdUpdateProProductonDetail.Parameters["@PId"].Value = e.UserId;
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
        public PageEntity<ProProductonDetail> Search(Int32 PDId, Decimal Price, Int32 PNum, String PCode, string StartTime, string EndTime, Int32 UserId, Int32 PId, int pageIndex, int pageSize)
        {
            PageEntity<ProProductonDetail> returnValue = new PageEntity<ProProductonDetail>();
            List<ProProductonDetail> prolist = new List<ProProductonDetail>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadProProductonDetail = cmdLoadProProductonDetail.Clone() as MySqlCommand;
            MySqlCommand _cmdLoadProProductonDetailCount = cmdLoadProProductonDetailCount.Clone() as MySqlCommand;
            _cmdLoadProProductonDetail.Connection = oc;
            _cmdLoadProProductonDetailCount.Connection = oc;
            try
            {
                _cmdLoadProProductonDetail.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadProProductonDetail.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadProProductonDetail.Parameters["@PDId"].Value = PDId;
                _cmdLoadProProductonDetail.Parameters["@Price"].Value = Price;
                _cmdLoadProProductonDetail.Parameters["@PNum"].Value = PNum;
                _cmdLoadProProductonDetail.Parameters["@PCode"].Value = PCode;
                _cmdLoadProProductonDetail.Parameters["@UserId"].Value = UserId;
                _cmdLoadProProductonDetail.Parameters["@PId"].Value = PId;

                _cmdLoadProProductonDetailCount.Parameters["@PDId"].Value = PDId;
                _cmdLoadProProductonDetailCount.Parameters["@Price"].Value = Price;
                _cmdLoadProProductonDetailCount.Parameters["@PNum"].Value = PNum;
                _cmdLoadProProductonDetailCount.Parameters["@PCode"].Value = PCode;
                _cmdLoadProProductonDetailCount.Parameters["@UserId"].Value = UserId;
                _cmdLoadProProductonDetailCount.Parameters["@PId"].Value = PId;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                if (!string.IsNullOrEmpty(StartTime))
                {
                    StartTime = "   AND  create_time >= DATE_FORMAT('" + StartTime + "', 'yyyy-mm-dd hh24:mi:ss')";
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    EndTime = "  AND create_time < DATE_FORMAT('" + EndTime + "', 'yyyy-mm-dd hh24:mi:ss')";
                }
                _cmdLoadProProductonDetail.CommandText = string.Format(_cmdLoadProProductonDetail.CommandText, StartTime, EndTime);
                MySqlDataReader reader = _cmdLoadProProductonDetail.ExecuteReader();
                while (reader.Read())
                {
                    prolist.Add(new ProProductonDetail().BuildSampleEntity(reader));
                    
                }
                reader.Close();
                returnValue.Items = prolist;
                returnValue.PageIndex = pageIndex;
                returnValue.PageSize = pageSize;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdLoadProProductonDetailCount.CommandText = string.Format(_cmdLoadProProductonDetailCount.CommandText, StartTime, EndTime);
                returnValue.RecordsCount =Convert.ToInt32( _cmdLoadProProductonDetailCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadProProductonDetail.Dispose();
                _cmdLoadProProductonDetail = null;
                _cmdLoadProProductonDetailCount.Dispose();
                _cmdLoadProProductonDetailCount = null;
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
