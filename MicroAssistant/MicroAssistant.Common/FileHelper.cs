using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Drawing.Imaging;
using MicroAssistant.DataStructure;
using System.Resources;
using System.Reflection;

namespace MicroAssistant.Common
{
    public class FileHelper
    {
        #region 上传图片
        /// <summary>
        /// 上传图片
        /// 图片有三类图，原始大图，缩略中图，缩略小图。
        ///  url命名原则：大图是原始url，中图url后缀名之前为_m,中图url后缀名之前为_s
        /// 上传类型：1-宝贝图片；2-投票图片，4- 用户头像 0-其他
        /// </summary>
        /// <param name="objid">图片所属对象ID</param>
        /// <param name="FileByte">文件</param>
        /// <param name="FileExtention">文件后缀名（扩展名）</param>
        /// <param name="type">上传类型：1-宝贝图片；2-投票图片，4- 用户头像 0-其他</param>
        /// <returns></returns>
        public static string UploadFile(int objid, byte[] fileByte, string fileExtention, PicType type)
        {
            DateTime time = DateTime.Now;
            string fileName = time.Ticks + "." + fileExtention;
            string fileSaveUrl = string.Format("/UploadPicture/{0}/{1}/{2}/{3}", type.ToString(), time.ToString("yyyyMM"), objid, fileName);

            try
            {
                string dir = Path.GetDirectoryName(HttpContext.Current.Server.MapPath(fileSaveUrl));
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                Image image = null;

                // 保存原图
                using (MemoryStream stream = new MemoryStream(fileByte, false))
                {
                    image = Image.FromStream(stream);
                    image.Save(HttpContext.Current.Server.MapPath(fileSaveUrl), ImageFormat.Jpeg);
                }

                Dictionary<string, int> sizeList = new Dictionary<string, int>();
                sizeList.Add("m", 320);
                sizeList.Add("s", 160);

                foreach (KeyValuePair<string, int> size in sizeList)
                {
                    float bl = Math.Min((float)size.Value / image.Width, (float)size.Value / image.Height);

                    Image img = image.GetThumbnailImage((int)(image.Width * bl), (int)(image.Height * bl), null, new IntPtr());
                    img.Save(Path.Combine(dir, size.Key + fileName), ImageFormat.Jpeg);
                }


                return fileSaveUrl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion 上传图片

       
    }
}
