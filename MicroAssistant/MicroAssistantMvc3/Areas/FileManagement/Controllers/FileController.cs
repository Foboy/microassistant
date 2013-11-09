using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.FileManagement.Controllers
{
    public class FileController : MicControllerBase
    {
        //
        // GET: /FileManagement/File

        public JsonResult SourceFileClipOrThumb(string sourcePath, PicType fileType, List<FileSaveInfo> saveInfo)
        {
            var Res = new JsonResult();
            AdvancedResult<List<FileSaveResult>> result = new AdvancedResult<List<FileSaveResult>>();
            result.Data = new List<FileSaveResult>();
            sourcePath = Server.MapPath(sourcePath);
            if (!System.IO.File.Exists(sourcePath))
            {
                result.Error = AppError.ERROR_PERSON_NOT_FOUND;
                result.ExMessage = result.ErrorMessage = "源文件不存在";
            }
            else if (saveInfo != null)
            {
                int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                if (userid > 0)
                {
                    
                    string itempath = FileHelper.GetFileSavePath(userid, fileType, null);
                    string itemfullpath = Server.MapPath(itempath);
                    string saveName = FileHelper.GetFileSaveName(".png");
                    foreach (FileSaveInfo item in saveInfo)
                    {
                        item.SavePrefix = item.SavePrefix != null ? item.SavePrefix : string.Empty;
                        if (item.IsClipping == 1)
                        {
                            FileHelper.ClipAndSaveFile(sourcePath, itemfullpath, item.SavePrefix + saveName, item);
                        }
                        else
                        {
                            FileHelper.ThumbAndSaveFile(sourcePath, itemfullpath, item.SavePrefix + saveName, item);
                        }
                        result.Data.Add(new FileSaveResult()
                        {
                            FileName = item.SavePrefix + saveName,
                            FilePath = itempath,
                            FileUrl = FileHelper.GetFileSavePath(userid, fileType, item.SavePrefix + saveName)
                        });
                    }
                }
                else
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                    result.ExMessage = result.ErrorMessage = "用户未登陆，不允许进行该操作！";
                }
            }
            else
            {
                result.Error = AppError.ERROR_SUCCESS;
            }
            Res.Data = result;
            return Res;
        }

        public JsonResult UploadFile(int saveSource,PicType fileType)
        {
            var Res = new JsonResult();
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase file = Request.Files[0];

                int seat = file.FileName.LastIndexOf('.');

                //返回位于String对象中指定位置的子字符串并转换为小写.  
                string extension = file.FileName.Substring(seat).ToLower();
                string fileSaveName = FileHelper.GetFileSaveName(extension);

                string path;
                if (saveSource == 1)
                {
                    path = FileHelper.GetFileSavePath(0, fileType, null);
                }
                else
                {
                    path = FileHelper.GetFileSavePath(0, PicType.Ignore, null);
                }

                string savepath = FileHelper.CreatePath(Server.MapPath(path));
                string fullpath = Path.Combine(savepath, fileSaveName);
                file.SaveAs(fullpath);




                ResPic pic = new ResPic();
                if (saveSource == 1)
                {
                    pic.PicUrl = FileHelper.GetFileSavePath(0, fileType, fileSaveName);
                }
                else
                {
                    pic.PicUrl = FileHelper.GetFileSavePath(0, PicType.Ignore, fileSaveName);
                }
                result.Data = pic;
                result.Error = AppError.ERROR_SUCCESS;
            }
            else
            {
                result.Error = AppError.ERROR_FAILED;
                result.ErrorMessage = result.ExMessage = "请选择上传的文件！";
            }

            Res.Data = result;
            return Res;
        }


        public JsonResult SetUserHeadPic(string sourcePath)
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

                    ResPic pic = new ResPic();
                    pic.ObjId = 0;
                    pic.ObjType = PicType.Ignore;
                    pic.PicUrl = sourcePath;
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
                    fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.BBPicture);

                    ResPic pic = new ResPic();
                    pic.ObjId = 0;
                    pic.ObjType = PicType.BBPicture;
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
