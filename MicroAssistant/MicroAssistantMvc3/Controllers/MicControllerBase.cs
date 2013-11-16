using MicroAssistant.Cache;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MicroAssistantMvc.Controllers
{
    public class MicControllerBase :Controller,IController
    {
        #region token
        private string currentToken;
        public string token {
            get {
                if (currentToken != null)
                    return currentToken;
                if (FormsAuthentication.CookiesSupported)
                {
                    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        try
                        {
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
                              Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                            currentToken = ticket.UserData;
                        }
                        catch (Exception e)
                        {
                            // 票据解密失败
                            
                        }
                    }
                }
                else
                {
                      //客户端不支持Cookie
                }
                return currentToken;
            }
        }
        #endregion

        #region currentUser
        private SysUser currentUser;
        public SysUser CurrentUser
        {
            get
            {
                if (token == null)
                {
                    return null;
                }
                if (currentUser != null)
                    return currentUser;

                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    currentUser = SysUserAccessor.Instance.Get(userid);
                    currentUser.userFuns = SysFunctionAccessor.Instance.SearchSysUserRolePermisson(userid);
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
                return currentUser;
            }
        }
        /// <summary>
        /// 判断用户是否有此页面权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        protected bool CheckUserFunction(string functionCode)
        {
            bool result = false;
            List<SysFunction> fs = CurrentUser.userFuns;

           foreach(SysFunction sf in fs)
           {
               if(sf.FunctionCode==functionCode)
               {
                   result=true;
               }
           }

            return result;
        }
        #endregion

        #region common

        /// <summary>
        /// 生成随机验证码
        /// </summary>
        /// <param name="num">验证码位数</param>
        /// <returns></returns>
        protected string GenCode(int num)
        {
            string[] source ={"0","1","2","3","4","5","6","7","8","9",
                         "A","B","C","D","E","F","G","H","I","J","K","L","M","N",
                       "O","P","Q","R","S","T","U","V","W","X","Y","Z"};
            string code = "";
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            return code;
        }

        /// <summary>
        /// 获取当前年月季度的第一天
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected DateTime GetStartTime(TimeType type)
        {
            DateTime now = DateTime.Now;
            switch (type)
            {
                case TimeType.month:
                    now = new DateTime(now.Year, now.Month, 1,0,0,0,0); //本月第一天

                    break;
                case TimeType.quarter:
                    now = new DateTime(now.Year, now.Month, 1,0,0,0,0);  //本季度初
                    now = now.AddMonths(0 - (now.Month - 1) % 3).AddDays(1 - now.Day);
                    now =now.AddDays(1 - now.Day);
                    break;
                case TimeType.year:
                    now = new DateTime(now.Year, 1, 1,0,0,0,0); //本月第一天
                    break;
                default:
                    break;
            }
            return now;
        }
        /// <summary>
        /// 获取当前年份的第二年第一天，获取当前月份的第二个月的第一天,获取当前季度下个季度的最后一天
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected DateTime GetEndTime(TimeType type)
        {
            DateTime now = DateTime.Now;
            switch (type)
            {
                case TimeType.month:
                    DateTime d1 = new DateTime(now.Year, now.Month, 1,0,0,0,0); //本月第一天
                    now = d1.AddMonths(1);
                    break;
                case TimeType.quarter:
                      DateTime startQuarter = new DateTime(now.Year, now.Month, 1,0,0,0,0);  //本季度初
                      startQuarter = startQuarter.AddMonths(0 - (startQuarter.Month - 1) % 3).AddDays(1 - startQuarter.Day);
                      startQuarter = startQuarter.AddDays(1 - startQuarter.Day);

                    now = startQuarter.AddMonths(3);  //本季度末
                    break;
                case TimeType.year:
                    DateTime d3 = new DateTime(now.Year, 1, 1,0,0,0,0); //本月第一天
                    now = d3.AddYears(1);
                    break;
                default:
                    break;
            }
            return now;
        }

#endregion common

        //添加用户入职记录
        protected void AddUserTimeMachine(int userId, int changeType, int roleId)
        {
            /*
             * changeType
             * 1用户注册
             * 2企业注册
             * 3用户绑定企业code 或者是有权限变没权限
             * 4未审核修改为有权限时
             * 
             */
            SysUserTimemachine ut = new SysUserTimemachine();
            SysUser user = new SysUser();
            user = SysUserAccessor.Instance.Get(userId);
            switch (changeType)
            {
                case 1:
                    if (!string.IsNullOrEmpty(user.EntCode))
                    {
                        ut.EntId = user.EntId;
                        ut.EntName = SysUserAccessor.Instance.Get(user.EntId).UserName;
                        ut.RoleName = "未审核";
                    }
                        ut.UserId = userId;
                        ut.UserName = user.UserName;
                       
                    ut.StartTime = DateTime.Now;
                    SysUserTimemachineAccessor.Instance.Insert(ut);
                    break;
                case 2:
                    ut.EntId = user.EntId;
                    ut.EntName = user.UserName;
                    ut.RoleName = "管理员";
                    ut.StartTime = DateTime.Now;
                    ut.UserId = userId;
                    ut.UserName = user.UserName;
                    SysUserTimemachineAccessor.Instance.Insert(ut);
                    break;
                case 3:
                    ut.EntId = user.EntId;
                    ut.EntName = SysUserAccessor.Instance.Get(user.EntId).UserName;
                    ut.RoleName = "未审核";
                    ut.StartTime = DateTime.Now;
                    ut.UserId = userId;
                    ut.UserName = user.UserName;
                    SysUserTimemachineAccessor.Instance.Insert(ut);
                    break;
                case 4:
                    ut.EntId = user.EntId;
                    ut.EntName = SysUserAccessor.Instance.Get(user.EntId).UserName;
                    ut.RoleName = SysRoleAccessor.Instance.Get(roleId).RoleName;
                    ut.StartTime = DateTime.Now;
                    ut.UserId = user.UserId;
                    ut.UserName = user.UserName;
                    SysUserTimemachineAccessor.Instance.Insert(ut);
                    break;
                default:
                    break;
            }



        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            ViewBag.LoginUser = CurrentUser;
            ViewBag.CurrentToken = token;
            base.OnAuthorization(filterContext);
        }


    }
}