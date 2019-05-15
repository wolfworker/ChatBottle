using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Utility
{
    public class AppConst
    {
        /// <summary>
        /// 服务回发消息时，用于分隔消息体和用户信息的字符串
        /// </summary>
        public const string EncryptStr = "#$%*()876543";
        public const string ImgExtion = ".jpg";
    }

    public class AppConfig
    {
        public static string ImgRootPath
        {
            get
            {
                var imgRootPath = System.Configuration.ConfigurationManager.AppSettings.Get("ImgRootPath");
                if (string.IsNullOrEmpty(imgRootPath))
                {
                    return string.Empty;
                }
                return imgRootPath;
            }
            set { ImgRootPath = value; }
        }

        public static string ImgRootUrl
        {
            get
            {
                var imgRootUrl = System.Configuration.ConfigurationManager.AppSettings.Get("ImgRootUrl");
                if (string.IsNullOrEmpty(imgRootUrl))
                {
                    return string.Empty;
                }
                return imgRootUrl;
            }
            set { ImgRootUrl = value; }
        }

        public static string ImgHeaderFolder
        {
            get
            {
                var imgHeaderFolder = System.Configuration.ConfigurationManager.AppSettings.Get("ImgHeaderFolder");
                if (string.IsNullOrEmpty(imgHeaderFolder))
                {
                    return string.Empty;
                }
                return imgHeaderFolder;
            }
            set { ImgHeaderFolder = value; }
        }

        public static string ImgChatFolder
        {
            get
            {
                var imgChatFolder = System.Configuration.ConfigurationManager.AppSettings.Get("ImgChatFolder");
                if (string.IsNullOrEmpty(imgChatFolder))
                {
                    return string.Empty;
                }
                return imgChatFolder;
            }
            set { ImgChatFolder = value; }
        }

        public static string ImgDefaultUrl
        {
            get
            {
                var imgDefaultUrl = ImgRootUrl + ImgHeaderFolder + "default" + AppConst.ImgExtion;
                return imgDefaultUrl;
            }
            set { ImgDefaultUrl = value; }
        }
    }
}