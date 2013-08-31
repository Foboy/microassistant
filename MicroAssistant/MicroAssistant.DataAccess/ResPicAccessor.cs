/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/7/7 21:34:06
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
using MicroAssistant.DataStructure;


namespace MicroAssistant.DataAccess
{
    public class ResPicAccessor
    {
        private MySqlCommand cmdInsertResPic;
        private MySqlCommand cmdDeleteResPic;
        private MySqlCommand cmdUpdateResPic;
        private MySqlCommand cmdLoadResPic;
        private MySqlCommand cmdLoadAllResPic;
        private MySqlCommand cmdGetResPicCount;
        private MySqlCommand cmdGetResPic;
        private MySqlCommand cmdSearchResPicByProId;

        private ResPicAccessor()
        {
            #region cmdInsertResPic

            cmdInsertResPic = new MySqlCommand("INSERT INTO res_pic(pic_description,obj_id,obj_type,pic_url,pic_height,pic_width,create_time) values (@PicDescription,@ObjId,@ObjType,@PicUrl,@PicHeight,@PicWidth,now())");

            cmdInsertResPic.Parameters.Add("@PicDescription", MySqlDbType.String);
            cmdInsertResPic.Parameters.Add("@ObjId", MySqlDbType.Int32);
            cmdInsertResPic.Parameters.Add("@ObjType", MySqlDbType.Int32);
            cmdInsertResPic.Parameters.Add("@PicUrl", MySqlDbType.String);
            cmdInsertResPic.Parameters.Add("@PicHeight", MySqlDbType.Int32);
            cmdInsertResPic.Parameters.Add("@PicWidth", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateResPic

            cmdUpdateResPic = new MySqlCommand(" update res_pic set pic_description = @PicDescription,obj_id = @ObjId,obj_type = @ObjType,pic_url = @PicUrl,pic_height = @PicHeight,pic_width = @PicWidth,state = @State,create_time=@CreateTime where pic_id = @PicId");
            cmdUpdateResPic.Parameters.Add("@PicId", MySqlDbType.Int32);
            cmdUpdateResPic.Parameters.Add("@PicDescription", MySqlDbType.String);
            cmdUpdateResPic.Parameters.Add("@ObjId", MySqlDbType.Int32);
            cmdUpdateResPic.Parameters.Add("@ObjType", MySqlDbType.Int32);
            cmdUpdateResPic.Parameters.Add("@PicUrl", MySqlDbType.String);
            cmdUpdateResPic.Parameters.Add("@PicHeight", MySqlDbType.Int32);
            cmdUpdateResPic.Parameters.Add("@PicWidth", MySqlDbType.Int32);
            cmdUpdateResPic.Parameters.Add("@State", MySqlDbType.Int32);
            cmdUpdateResPic.Parameters.Add("@CreateTime", MySqlDbType.DateTime);

            #endregion

            #region cmdLoadResPic

            cmdLoadResPic = new MySqlCommand(@" select pic_id,pic_description,obj_id,obj_type,pic_url,pic_height,pic_width,state,create_time from res_pic limit @PageIndex,@PageSize");
            cmdLoadResPic.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadResPic.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetResPicCount

            cmdGetResPicCount = new MySqlCommand(" select count(*)  from res_pic ");

            #endregion

            #region cmdLoadAllResPic

            cmdLoadAllResPic = new MySqlCommand(" select pic_id,pic_description,obj_id,obj_type,pic_url,pic_height,pic_width,state,create_time from res_pic");

            #endregion

            #region cmdGetResPic

            cmdGetResPic = new MySqlCommand(" select pic_id,pic_description,obj_id,obj_type,pic_url,pic_height,pic_width,state,create_time from res_pic where pic_id = @PicId");
            cmdGetResPic.Parameters.Add("@PicId", MySqlDbType.Int32);

            #endregion

            #region cmdGetResPic

            cmdSearchResPicByProId = new MySqlCommand(" select pic_id,pic_description,obj_id,obj_type,pic_url,pic_height,pic_width,state,create_time from res_pic where obj_id = @ObjId and obj_type = @ObjType");
            cmdSearchResPicByProId.Parameters.Add("@ObjId", MySqlDbType.Int32);
            cmdSearchResPicByProId.Parameters.Add("@ObjType", MySqlDbType.Int32);

            #endregion


            #region cmdDeleteResPic

            cmdDeleteResPic = new MySqlCommand(" delete from res_pic where pic_id = @PicId");
            cmdDeleteResPic.Parameters.Add("@PicId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ResPic e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertResPic = cmdInsertResPic.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertResPic.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertResPic.Parameters["@PicDescription"].Value = e.PicDescription;
                _cmdInsertResPic.Parameters["@ObjId"].Value = e.ObjId;
                _cmdInsertResPic.Parameters["@ObjType"].Value = e.ObjType;
                _cmdInsertResPic.Parameters["@PicUrl"].Value = e.PicUrl;
                _cmdInsertResPic.Parameters["@PicHeight"].Value = e.PicHeight;
                _cmdInsertResPic.Parameters["@PicWidth"].Value = e.PicWidth;
                _cmdInsertResPic.ExecuteNonQuery();
                returnValue = Convert.ToInt32(_cmdInsertResPic.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertResPic.Dispose();
                _cmdInsertResPic = null;
            }
        }


        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int picId)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdDeleteResPic = cmdDeleteResPic.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdDeleteResPic.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdDeleteResPic.Parameters["@PicId"].Value = picId;

                _cmdDeleteResPic.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdDeleteResPic.Dispose();
                _cmdDeleteResPic = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ResPic e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateResPic = cmdUpdateResPic.Clone() as MySqlCommand;
            _cmdUpdateResPic.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateResPic.Parameters["@PicId"].Value = e.PicId;
                _cmdUpdateResPic.Parameters["@PicDescription"].Value = e.PicDescription;
                _cmdUpdateResPic.Parameters["@ObjId"].Value = e.ObjId;
                _cmdUpdateResPic.Parameters["@ObjType"].Value = e.ObjType;
                _cmdUpdateResPic.Parameters["@PicUrl"].Value = e.PicUrl;
                _cmdUpdateResPic.Parameters["@PicHeight"].Value = e.PicHeight;
                _cmdUpdateResPic.Parameters["@PicWidth"].Value = e.PicWidth;
                _cmdUpdateResPic.Parameters["@State"].Value = e.State;
                _cmdUpdateResPic.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdUpdateResPic.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateResPic.Dispose();
                _cmdUpdateResPic = null;
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
        public PageEntity<ResPic> Search(Int32 PicId, String PicDescription, Int32 ObjId, Int32 ObjType, String PicUrl, Int32 PicHeight, Int32 PicWidth, Int32 State, int pageIndex, int pageSize)
        {
            PageEntity<ResPic> returnValue = new PageEntity<ResPic>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadResPic = cmdLoadResPic.Clone() as MySqlCommand;
            MySqlCommand _cmdGetResPicCount = cmdGetResPicCount.Clone() as MySqlCommand;
            _cmdLoadResPic.Connection = oc;
            _cmdGetResPicCount.Connection = oc;

            try
            {
                _cmdLoadResPic.Parameters["@PageIndex"].Value = pageIndex * pageSize; ;
                _cmdLoadResPic.Parameters["@PageSize"].Value = (pageIndex + 1) * pageSize; ;
                _cmdLoadResPic.Parameters["@PicId"].Value = PicId;
                _cmdLoadResPic.Parameters["@PicDescription"].Value = PicDescription;
                _cmdLoadResPic.Parameters["@ObjId"].Value = ObjId;
                _cmdLoadResPic.Parameters["@ObjType"].Value = ObjType;
                _cmdLoadResPic.Parameters["@PicUrl"].Value = PicUrl;
                _cmdLoadResPic.Parameters["@PicHeight"].Value = PicHeight;
                _cmdLoadResPic.Parameters["@PicWidth"].Value = PicWidth;
                _cmdLoadResPic.Parameters["@State"].Value = State;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadResPic.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new ResPic().BuildSampleEntity(reader));
                }
                reader.Close();
                returnValue.PageIndex = pageIndex;
                returnValue.PageSize = pageSize;
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetResPicCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadResPic.Dispose();
                _cmdLoadResPic = null;
                _cmdGetResPicCount.Dispose();
                _cmdGetResPicCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ResPic> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllResPic = cmdLoadAllResPic.Clone() as MySqlCommand;
            _cmdLoadAllResPic.Connection = oc; List<ResPic> returnValue = new List<ResPic>();
            try
            {
                _cmdLoadAllResPic.CommandText = string.Format(_cmdLoadAllResPic.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllResPic.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ResPic().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllResPic.Dispose();
                _cmdLoadAllResPic = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public ResPic Get(int PicId)
        {
            ResPic returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetResPic = cmdGetResPic.Clone() as MySqlCommand;

            _cmdGetResPic.Connection = oc;
            try
            {
                _cmdGetResPic.Parameters["@PicId"].Value = PicId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetResPic.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ResPic().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetResPic.Dispose();
                _cmdGetResPic = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public List<ResPic> Search(int ObjId,PicType objType)
        {
            List<ResPic> returnValue = new List<ResPic>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdSearchResPicByProId = cmdSearchResPicByProId.Clone() as MySqlCommand;

            _cmdSearchResPicByProId.Connection = oc;
            try
            {
                _cmdSearchResPicByProId.Parameters["@ObjId"].Value = ObjId;
                _cmdSearchResPicByProId.Parameters["@ObjType"].Value = (int)objType;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdSearchResPicByProId.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ResPic().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdSearchResPicByProId.Dispose();
                _cmdSearchResPicByProId = null;
                GC.Collect();
            }
            return returnValue;

        }

        private static readonly ResPicAccessor instance = new ResPicAccessor();
        public static ResPicAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
