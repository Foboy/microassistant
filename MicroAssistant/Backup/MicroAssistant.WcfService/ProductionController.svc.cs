using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MicroAssistant.Common;
using MicroAssistant.Meta;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;

namespace MicroAssistant.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ProductionController”。
    public class ProductionController : IProductionController
    {
        #region IProductionController 成员
        /// <summary>
        /// 根据分类获取产品列表
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public AdvancedResult<PageEntity<ProProduction>> SearchProductionSByType(string pName,int typeid,int pageIndex,int pageSize)
        {
            AdvancedResult<PageEntity<ProProduction>> result = new AdvancedResult<PageEntity<ProProduction>>();
            try
            {
                PageEntity<ProProduction> list = new PageEntity<ProProduction>();
                list = ProProductionAccessor.Instance.Search(pName,typeid,0, pageIndex, pageSize);
                result.Error = AppError.ERROR_SUCCESS;
                result.Data =list;

            }
            catch (Exception e)
            {

                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 根据用户获取分类列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public AdvancedResult<PageEntity<ProProductionType>> SearchProductTypeListByuserID(int userid,int pageIndex,int pageSize)
        {
            AdvancedResult<PageEntity<ProProductionType>> result = new AdvancedResult<PageEntity<ProProductionType>>();
            try
            {
                PageEntity<ProProductionType> list = new PageEntity<ProProductionType>();
                list = ProProductionTypeAccessor.Instance.Search(0, string.Empty, userid, pageIndex,pageSize);
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;

            }
            catch (Exception e)
            {

                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 根据产品ID获取产品信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public AdvancedResult<ProProduction> GetProductInfoByPID(int pid)
        {
            AdvancedResult<ProProduction> result = new AdvancedResult<ProProduction>();
            try
            {
                result.Data = ProProductionAccessor.Instance.Get(pid);
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 添加入库单
        /// </summary>
        /// <param name="ppd"></param>
        /// <returns></returns>
        public RespResult AddProductonDetail(ProProductonDetail ppd)
        {
            RespResult result = new RespResult();
            try
            {
                result.Error = ProProductonDetailAccessor.Instance.Insert(ppd) ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 获取产品入库单列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public AdvancedResult<PageEntity<ProProductonDetail>> SearchProductonDetailList(int pid, string startTime, string endTime, int pageIndex, int pageSize)
        {
            AdvancedResult<PageEntity<ProProductonDetail>> result = new AdvancedResult<PageEntity<ProProductonDetail>>();
            try
            {
                PageEntity<ProProductonDetail> list = new PageEntity<ProProductonDetail>();
                list = ProProductonDetailAccessor.Instance.Search(0, 0, 0, string.Empty,startTime,endTime,0,pid,pageIndex,pageSize);
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;

            }
            catch (Exception e)
            {

                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        ///添加产品
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public RespResult AddProduction(ProProduction pro)
        {
            RespResult result = new RespResult();
            try
            {
                result.Error = ProProductionAccessor.Instance.Insert(pro) ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        ///添加产品分类
        /// </summary>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public RespResult AddProductionType(ProProductionType ptype)
        {
            RespResult result = new RespResult();
            try
            {
                result.Error = ProProductionTypeAccessor.Instance.Insert(ptype) ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public RespResult DeleteProduction(int pid)
        {
            RespResult result = new RespResult();
            try
            {
                result.Error = ProProductionAccessor.Instance.Delete(pid) ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        #endregion
    }
}
