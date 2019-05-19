using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using Co.ChatBottle.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Co.ChatBottle.Service.Controllers
{
    public class UserController : BaseController
    {
        public UserBiz userBiz { get; set; } = new UserBiz();

        [HttpPost]
        public HttpResponseMessage Login(UserRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserName)
                || string.IsNullOrEmpty(request.UserName.Trim()))
            {
                return ErrorToJson("昵称需要填的哦，小可爱");
            }
            ConvertBaseRequest(request);

            var userName = request.UserName.Trim();

            //先用账号密码 验证登陆
            var sql = $"SELECT * FROM dbo.ACT_User WHERE UserName = '{userName}' AND Status = 0 ORDER BY UpdateTime DESC ";
            var exsitUser = userBiz.QueryCustom<ACT_User>(sql);

            if (exsitUser != null && exsitUser.Any())
            {
                if (exsitUser.FirstOrDefault().PassChar == request.PassChar
                    && !string.IsNullOrEmpty(exsitUser.FirstOrDefault().PassChar))
                {
                    //登陆成功
                    return EntityToJson(exsitUser);
                }
                return ErrorToJson("这个昵称已经有人用了，换一个吧！");
            }

            var userInfo = new ACT_User
            {
                UserName = userName,
                Gender = request.Gender,
                PassChar = request.PassChar,
            };

            ACT_User userEntity = null;
            try
            {
                //没有头像用默认头像
                if (string.IsNullOrEmpty(userInfo.HeaderImgUrl))
                {
                    userInfo.HeaderImgUrl = AppConfig.ImgDefaultUrl;
                }
                userInfo.HeaderImgUrl = ImageUtil.GetImgUrlWithTag(userInfo.HeaderImgUrl);
                userEntity = userBiz.Add(userInfo);
            }
            catch (Exception ex)
            {
                return ErrorToJson(ex.Message);
            }
            return EntityToJson(userEntity);
        }

        [HttpPost]
        public HttpResponseMessage Update(UserRequest request)
        {
            var response = new BaseResponse<ACT_User>();

            ConvertBaseRequest(request);

            if (request == null)
            {
                return ErrorToJson("请求为空");
            }

            //判断是否已存在用户名
            var userName = request.UserName.Trim();
            var sql = $"SELECT * FROM dbo.ACT_User WHERE UserName = '{userName}' AND ID != {request.ID} AND Status = 0";
            var exsitUser = userBiz.QueryCustom<ACT_User>(sql);
            if (exsitUser != null && exsitUser.Any())
            {
                return ErrorToJson("这个昵称已经有人用了，换一个吧！");
            }

            var userEntity = userBiz.Query<ACT_User>(request.ID);

            userEntity.UserName = userName;
            userEntity.Phone = request.Phone;
            userEntity.QQ = request.QQ;
            userEntity.Mail = request.Mail;
            userEntity.Gender = request.Gender;
            if (request.Birthday != null && request.Birthday != DateTime.MinValue)
            {
                userEntity.Birthday = request.Birthday;
            }
            userEntity.Remark = request.Remark;
            userEntity.UpdateUserID = 1;
            userEntity.UpdateTime = DateTime.Now;

            //保存头像
            if (!string.IsNullOrEmpty(request.FileBase64))
            {
                var fileFullPath = "";
                try
                {
                    var fileName = request.ID + AppConst.ImgExtion;
                    var directFolder = AppConfig.ImgRootPath + AppConfig.ImgHeaderFolder;
                    fileFullPath = directFolder + fileName;
                   
                    var success = ImageUtil.SaveImageToLocal(request.FileBase64, directFolder, fileName);
                    if (success)
                    {
                        userEntity.HeaderImgUrl = AppConfig.ImgRootUrl + AppConfig.ImgHeaderFolder + fileName;
                    }
                }
                catch (Exception ex)
                {
                    var message = $"更新头像 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 物理地址：{fileFullPath}";
                    WriteLog(message);
                }
            }
            //没有头像用默认头像
            if (string.IsNullOrEmpty(userEntity.HeaderImgUrl))
            {
                userEntity.HeaderImgUrl = AppConfig.ImgDefaultUrl;
            }
            userEntity.HeaderImgUrl = ImageUtil.GetImgUrlWithTag(userEntity.HeaderImgUrl);
            if (userBiz.Update(userEntity))
            {
                response.ErrorCode = 0;
                response.Result = userEntity;
                return ResponseToJson(response);
            }
            else
            {
                return ErrorToJson("更新失败");
            }
            
        }

        ////将图片按百分比压缩，flag取值1到100，越小压缩比越大
        //public void CompressSave(Image iSource, string outPath, int flag)
        //{
        //    ImageFormat tFormat = iSource.RawFormat;
        //    EncoderParameters ep = new EncoderParameters();
        //    long[] qy = new long[1];
        //    qy[0] = flag;
        //    EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
        //    ep.Param[0] = eParam;
        //    ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageDecoders();
        //    ImageCodecInfo jpegICIinfo = null;
        //    for (int x = 0; x < arrayICI.Length; x++)
        //    {
        //        if (arrayICI[x].FormatDescription.Equals("JPEG"))
        //        {
        //            jpegICIinfo = arrayICI[x];
        //            break;
        //        }
        //    }
        //    if (jpegICIinfo != null)
        //    {
        //        iSource.Save(outPath, jpegICIinfo, ep);
        //    }
        //    else
        //    {
        //        iSource.Save(outPath, tFormat);
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage QueryAll()
        {
            var result = userBiz.QueryAll<ACT_User>();
            return EntityToJson(result);
        }

        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage QueryById(long id)
        {
            var result = userBiz.Query<ACT_User>(id);
            return EntityToJson(result);
        }
    }
}
