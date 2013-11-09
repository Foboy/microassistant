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
    public class ImageSaveInfo
    {
        public int IsClipping { get; set; }
        public string SavePrefix { get; set; }
        public int StartLeft { get; set; }
        public int StartTop { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int SaveWidth { get; set; }
        public int SaveHeight { get; set; }
    }

    public class PhotoCutInfo
    {
        public PhotoCutInfo()
        {
        }

        public string DocMuid { get; set; }
        public string DocName { get; set; }
        public string SaveName { get; set; }
        public int Saved { get; set; }
        public string Msg { get; set; }
    }

    public class FileHelper
    {

        private static string GetFileSavePath(int objid, PicType type)
        {
            DateTime time = DateTime.Now;
            return string.Format("/UploadPicture/{0}/{1}/{2}/", type.ToString(), time.ToString("yyyyMM"), objid);
        }

        private static string GetFileSaveName(string fileExtention)
        {
            DateTime time = DateTime.Now;
            return time.Ticks + "." + fileExtention;
        
        
        }

        public static string SaveFile()
        {
            return null;
        }
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
            string fileName = GetFileSaveName(fileExtention);
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

        public PhotoCutInfo CutPhotoFromSource(int objid, string url, int startX, int startY, int width, int height, int swidth, int sheight, PicType type)
        {
            PhotoCutInfo cutresult = new PhotoCutInfo();
            DateTime time = DateTime.Now;

            string fileSaveUrl = string.Format("/UploadPicture/{0}/{1}/{2}", type.ToString(), time.ToString("yyyyMM"), objid);

            string filepath = HttpContext.Current.Server.MapPath(url);
            string sspath = HttpContext.Current.Server.MapPath(fileSaveUrl);

            cutresult.DocName = time.Ticks.ToString();

            string saveName = cutresult.DocName + ".jpg";

            if (!File.Exists(filepath) || !filepath.Contains(sspath))
            {
                cutresult.Msg = "文件不存在！";
                cutresult.Saved = 0;
            }
            else
            {
                int level = 100;
                ImageCodecInfo ici = ImageCodecInfo.GetImageEncoders().SingleOrDefault(c => c.MimeType == "image/jpeg");
                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, level);

                string _savepath = HttpContext.Current.Server.MapPath(fileSaveUrl);
                if (!Directory.Exists(_savepath))
                {
                    //如果目录不存在
                    //创建目录
                    Directory.CreateDirectory(_savepath);
                }

                System.Drawing.Image bitmap = null;
                System.Drawing.Image source = null;
                try
                {

                    bitmap = new System.Drawing.Bitmap(swidth, sheight);
                    source = System.Drawing.Image.FromFile(filepath);
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                    //设定缩略图的生成质量
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.Clear(System.Drawing.Color.Transparent);

                    g.DrawImage(source, new System.Drawing.Rectangle(0, 0, swidth, sheight), new System.Drawing.Rectangle(startX, startY, width, height), System.Drawing.GraphicsUnit.Pixel);

                    bitmap.Save(_savepath + saveName, ici, ep);

                    cutresult.SaveName = fileSaveUrl + saveName;
                    cutresult.Saved = 1;

                }
                catch (Exception ex)
                {
                    //保存缩略图出错处理
                    cutresult.Msg = ex.Message;
                    cutresult.Saved = 0;
                }
                finally
                {
                    if (source != null)
                    {
                        source.Dispose();
                    }
                    if (bitmap != null)
                    {
                        bitmap.Dispose();
                    }
                }
            }
            return cutresult;
        }

    }
}
