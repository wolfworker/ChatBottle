using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Co.ChatBottle.Utility
{
    public class ImageUtil
    {
        //将图片按百分比压缩，flag取值1到100，越小压缩比越大
        public static bool CompressSave(Image iSource, string directPath, string fileName, int flag)
        {
            try
            {
                ImageFormat tFormat = iSource.RawFormat;
                EncoderParameters ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = flag;
                EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageDecoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (!Directory.Exists(directPath))
                {
                    Directory.CreateDirectory(directPath);
                }
                var fullpath = directPath + fileName;
                if (jpegICIinfo != null)
                {
                    iSource.Save(fullpath, jpegICIinfo, ep);
                }
                else
                {
                    iSource.Save(fullpath, tFormat);
                }
                return true;
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }

        public static bool SaveImageToLocal(string base64Str, string directPath,string fileName)
        {
            try
            {
                var imgBase64 = base64Str.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "").Replace("data:image/bmp;base64,", "").Replace("data:image/gif;base64,", "");
                var imgBtyes = Convert.FromBase64String(imgBase64);
                var stream = new MemoryStream(imgBtyes);
                var image = Image.FromStream(stream, true);
                return CompressSave(image, directPath, fileName, 10);
            }
            catch (Exception ex)
            {
                //log
                throw ex;
            }
        }

        public static string GetImgUrlWithTag(string imgurl)
        {
            if (!string.IsNullOrEmpty(imgurl))
            {
                if (imgurl.Contains("?"))
                {
                    imgurl += "&tag="+DateTime.Now.Ticks;
                }
                else
                {
                    imgurl += "?tag=" + DateTime.Now.Ticks;
                }
            }
            return imgurl;
        }
    }
}
