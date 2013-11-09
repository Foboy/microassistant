using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.FileManagement.Controllers
{
    public class FileController : MicControllerBase
    {
        //
        // GET: /FileManagement/File

        public JsonResult UploadImage(int saveSource,List<ImageSaveInfo> saveInfo)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();

            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }


        public JsonResult UploadPic(byte[] fileByte, int picHeight, int picWidth)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    string fileUrl = string.Empty;
                    fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.Ignore);

                    ResPic pic = new ResPic();
                    pic.ObjId = 0;
                    pic.ObjType = PicType.Ignore;
                    pic.PicUrl = fileUrl;
                    pic.PicHeight = picHeight;
                    pic.PicWidth = picWidth;
                    pic.State = StateType.Active;

                    int picid = ResPicAccessor.Instance.Insert(pic);
                    pic.PicId = picid;

                    result.Data = pic;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }

            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        /// <summary>
        /// 添加产品分类图片
        /// </summary>
        /// <param name="fileByte"></param>
        /// <param name="picHeight"></param>
        /// <param name="picWidth"></param>
        /// <returns></returns>
        public JsonResult UploadProTypePic(byte[] fileByte, int picHeight, int picWidth)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    string fileUrl = string.Empty;
                    fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.ProTypePicture);

                    ResPic pic = new ResPic();
                    pic.ObjId = 0;
                    pic.ObjType = PicType.ProTypePicture;
                    pic.PicUrl = fileUrl;
                    pic.PicHeight = picHeight;
                    pic.PicWidth = picWidth;
                    pic.State = StateType.Active;

                    int picid = ResPicAccessor.Instance.Insert(pic);
                    pic.PicId = picid;

                    result.Data = pic;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }

            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 添加用户头像图片
        /// </summary>
        /// <param name="fileByte"></param>
        /// <param name="picHeight"></param>
        /// <param name="picWidth"></param>
        /// <returns></returns>
        public JsonResult UploadUserImage(byte[] fileByte, int picHeight, int picWidth)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    string fileUrl = string.Empty;
                    fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.UserHeadImg);
                    SysUser user = SysUserAccessor.Instance.Get(userid);
                    ResPic pic = new ResPic();
                    pic.ObjId = userid;
                    pic.ObjType = PicType.UserHeadImg;
                    pic.PicUrl = fileUrl;
                    pic.PicHeight = picHeight;
                    pic.PicWidth = picWidth;
                    pic.State = StateType.Active;
                    //if (user.PicId > 0)
                    //{
                    //    ResPicAccessor.Instance.Delete(user.PicId);
                    //}

                    int picid = ResPicAccessor.Instance.Insert(pic);

                    result.Data = pic;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        public JsonResult DeleteBBPic(int picId)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    ResPicAccessor.Instance.Delete(picId);
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

    }
}
