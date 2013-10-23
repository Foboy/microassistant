/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/31 14:50:55
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
    public class ProProductionTypeAccessor
    {
        private MySqlCommand cmdInsertProProductionType;
        private MySqlCommand cmdDeleteProProductionType;
        private MySqlCommand cmdUpdateProProductionType;
        private MySqlCommand cmdLoadProProductionType;
        private MySqlCommand cmdLoadAllProProductionType;
        private MySqlCommand cmdLoadProProductionTypeCount;
        private MySqlCommand cmdGetProProductionType;

        private ProProductionTypeAccessor()
        {
            #region cmdInsertProProductionType

            cmdInsertProProductionType = new MySqlCommand("INSERT INTO pro_production_type(p_type_name,ent_id,father_id,pic_id) values (@PTypeName,@EntId,@FatherId,@PicId)");

            cmdInsertProProductionType.Parameters.Add("@PTypeName", MySqlDbType.String);
            cmdInsertProProductionType.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdInsertProProductionType.Parameters.Add("@FatherId", MySqlDbType.Int32);
            cmdInsertProProductionType.Parameters.Add("@PicId", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateProProductionType

            cmdUpdateProProductionType = new MySqlCommand(" update pro_production_type set p_type_name = @PTypeName,ent_id = @EntId,father_id = @FatherId,pic_id = @PicId where p_type_id = @PTypeId");
            cmdUpdateProProductionType.Parameters.Add("@PTypeId", MySqlDbType.Int32);
            cmdUpdateProProductionType.Parameters.Add("@PTypeName", MySqlDbType.String);
            cmdUpdateProProductionType.Parameters.Add("@EntId", MySqlDbType.Int32);
            cmdUpdateProProductionType.Parameters.Add("@FatherId", MySqlDbType.Int32);
            cmdUpdateProProductionType.Parameters.Add("@PicId", MySqlDbType.Int32);

            #endregion

            #region cmdDeleteProProductionType

            cmdDeleteProProductionType = new MySqlCommand(" delete from pro_production_type where p_type_id = @PTypeId");
            cmdDeleteProProductionType.Parameters.Add("@PTypeId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadProProductionType

            cmdLoadProProductionType = new MySqlCommand(@"select p_type_id,p_type_name,ent_id,father_id,t.pic_id,r.pic_url from pro_production_type as t LEFT JOIN res_pic as r on t.pic_id=r.pic_id
            WHERE  t.ent_id=@entid and (0=@fatherid or t.father_id=@fatherid) and (0=@ptypeid or t.p_type_id=@ptypeid) AND (''=@ptypename or t.p_type_name =@ptypename) LIMIT @PageIndex,@PageSize");
            cmdLoadProProductionType.Parameters.Add("@PageIndex", MySqlDbType.Int32);
            cmdLoadProProductionType.Parameters.Add("@PageSize", MySqlDbType.Int32);
            cmdLoadProProductionType.Parameters.Add("@entid", MySqlDbType.Int32);
            cmdLoadProProductionType.Parameters.Add("@fatherid", MySqlDbType.Int32);
            cmdLoadProProductionType.Parameters.Add("@ptypeid", MySqlDbType.Int32);
            cmdLoadProProductionType.Parameters.Add("@ptypename", MySqlDbType.String); 

            #endregion

            #region cmdLoadProProductionTypeCount

            cmdLoadProProductionTypeCount = new MySqlCommand(@"select count(1) from pro_production_type as t LEFT JOIN res_pic as r on t.pic_id=r.pic_id WHERE t.ent_id=@entid and (@fatherid=0 or t.father_id=@fatherid) and (@ptypeid=0 or t.p_type_id=@ptypeid) AND (@ptypename='' or t.p_type_name =@ptypename)");
            cmdLoadProProductionTypeCount.Parameters.Add("@entid", MySqlDbType.Int32);
            cmdLoadProProductionTypeCount.Parameters.Add("@fatherid", MySqlDbType.Int32);
            cmdLoadProProductionTypeCount.Parameters.Add("@ptypeid", MySqlDbType.Int32);
            cmdLoadProProductionTypeCount.Parameters.Add("@ptypename", MySqlDbType.String); 
            #endregion

            #region cmdLoadAllProProductionType

            cmdLoadAllProProductionType = new MySqlCommand(" select p_type_id,p_type_name,ent_id,father_id,pic_id from pro_production_type");

            #endregion

            #region cmdGetProProductionType

            cmdGetProProductionType = new MySqlCommand(" select p_type_id,p_type_name,ent_id,father_id,pic_id from pro_production_type where p_type_id = @PTypeId");
            cmdGetProProductionType.Parameters.Add("@PTypeId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ProProductionType e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertProProductionType = cmdInsertProProductionType.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertProProductionType.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdInsertProProductionType.Parameters["@PTypeName"].Value = e.PTypeName;
                _cmdInsertProProductionType.Parameters["@EntId"].Value = e.EntId;
                _cmdInsertProProductionType.Parameters["@FatherId"].Value = e.FatherId;
                _cmdInsertProProductionType.Parameters["@PicId"].Value = e.PicId;

                _cmdInsertProProductionType.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertProProductionType.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertProProductionType.Dispose();
                _cmdInsertProProductionType = null;
            }
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int PTypeId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteProProductionType = cmdDeleteProProductionType.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteProProductionType.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteProProductionType.Parameters["@PTypeId"].Value = PTypeId;


                _cmdDeleteProProductionType.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteProProductionType.Dispose();
                _cmdDeleteProProductionType = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ProProductionType e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateProProductionType = cmdUpdateProProductionType.Clone() as MySqlCommand;
            _cmdUpdateProProductionType.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateProProductionType.Parameters["@PTypeId"].Value = e.PTypeId;
                _cmdUpdateProProductionType.Parameters["@PTypeName"].Value = e.PTypeName;
                _cmdUpdateProProductionType.Parameters["@EntId"].Value = e.EntId;
                _cmdUpdateProProductionType.Parameters["@FatherId"].Value = e.FatherId;
                _cmdUpdateProProductionType.Parameters["@PicId"].Value = e.PicId;

                _cmdUpdateProProductionType.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateProProductionType.Dispose();
                _cmdUpdateProProductionType = null;
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
        public PageEntity<ProProductionType> Search(Int32 PTypeId, String PTypeName, Int32 EntId, Int32 FatherId, int pageIndex, int pageSize)
        {
            PageEntity<ProProductionType> returnValue = new PageEntity<ProProductionType>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadProProductionType = cmdLoadProProductionType.Clone() as MySqlCommand;
            MySqlCommand _cmdLoadProProductionTypeCount = cmdLoadProProductionTypeCount.Clone() as MySqlCommand;
            _cmdLoadProProductionType.Connection = oc;
            _cmdLoadProProductionTypeCount.Connection = oc;
            try
            {
                _cmdLoadProProductionType.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadProProductionType.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadProProductionType.Parameters["@ptypeid"].Value = PTypeId;
                _cmdLoadProProductionType.Parameters["@ptypename"].Value = PTypeName;
                _cmdLoadProProductionType.Parameters["@entid"].Value = EntId;
                _cmdLoadProProductionType.Parameters["@fatherid"].Value = FatherId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadProProductionType.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new ProProductionType().BuildSampleEntity(reader));
                }
                reader.Close();
                _cmdLoadProProductionTypeCount.Parameters["@ptypeid"].Value = PTypeId;
                _cmdLoadProProductionTypeCount.Parameters["@ptypename"].Value = PTypeName;
                _cmdLoadProProductionTypeCount.Parameters["@entid"].Value = EntId;
                _cmdLoadProProductionTypeCount.Parameters["@fatherid"].Value = FatherId;

                returnValue.RecordsCount = Convert.ToInt32( _cmdLoadProProductionTypeCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadProProductionType.Dispose();
                _cmdLoadProProductionType = null;
                _cmdLoadProProductionTypeCount.Dispose();
                _cmdLoadProProductionTypeCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ProProductionType> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllProProductionType = cmdLoadAllProProductionType.Clone() as MySqlCommand;
            _cmdLoadAllProProductionType.Connection = oc; List<ProProductionType> returnValue = new List<ProProductionType>();
            try
            {
                _cmdLoadAllProProductionType.CommandText = string.Format(_cmdLoadAllProProductionType.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllProProductionType.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ProProductionType().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllProProductionType.Dispose();
                _cmdLoadAllProProductionType = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public ProProductionType Get(int PTypeId)
        {
            ProProductionType returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetProProductionType = cmdGetProProductionType.Clone() as MySqlCommand;

            _cmdGetProProductionType.Connection = oc;
            try
            {
                _cmdGetProProductionType.Parameters["@PTypeId"].Value = PTypeId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetProProductionType.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ProProductionType().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetProProductionType.Dispose();
                _cmdGetProProductionType = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly ProProductionTypeAccessor instance = new ProProductionTypeAccessor();
        public static ProProductionTypeAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
