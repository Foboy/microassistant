using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MicroAssistant.Meta;
using MicroAssistant.Common;

namespace MicroAssistant.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IProductionController”。
    [ServiceContract]
    public interface IProductionController
    {
        //根据分类获取产品列表
        [OperationContract]
        AdvancedResult<PageEntity<ProProduction>> SearchProductionSByType(string pName,int typeid,int pageIndex,int pageSize);
        //根据用户获取分类列表
        [OperationContract]
        AdvancedResult<PageEntity<ProProductionType>> SearchProductTypeListByuserID(int userid,int pageIndex,int pageSize);
        //根据用户获取列表操作权限

        //根据产品ID获取产品信息
        [OperationContract]
        AdvancedResult<ProProduction> GetProductInfoByPID(int pid);
        //添加入库单
        [OperationContract]
        RespResult AddProductonDetail(ProProductonDetail ppd);
        //获取产品入库单列表
        [OperationContract]
        AdvancedResult<PageEntity<ProProductonDetail>> SearchProductonDetailList(int pid, string startTime, string endTime, int pageIndex, int pageSize);
        //添加产品
        [OperationContract]
        RespResult AddProduction(ProProduction pro);
        //添加产品分类
        [OperationContract]
        RespResult AddProductionType(ProProductionType ptype);
        //删除产品
        [OperationContract]
        RespResult DeleteProduction(int pid);
    }
}
