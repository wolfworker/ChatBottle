using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using Co.ChatBottle.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
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
                userEntity = userBiz.Add(userInfo);
            }catch(Exception ex)
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
                try
                {
                    var imgBase64 = request.FileBase64.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "").Replace("data:image/bmp;base64,", "").Replace("data:image/gif;base64,", "");
                    var imgBtyes = Convert.FromBase64String(imgBase64);
                    var stream = new MemoryStream(imgBtyes);
                    var image = Bitmap.FromStream(stream, true);
                    var filePath = System.Configuration.ConfigurationManager.AppSettings.Get("PhysicsHeaderPath");
                    var webImageUrl = System.Configuration.ConfigurationManager.AppSettings.Get("WebHeaderUrl");
                    var fileFullPath = filePath + "\\" + request.ID+".bmp";

                    image.Save(fileFullPath, System.Drawing.Imaging.ImageFormat.Bmp);
                    userEntity.HeaderImgUrl = webImageUrl + request.ID + ".bmp";
                }
                catch (Exception ex)
                {

                }
            }

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
