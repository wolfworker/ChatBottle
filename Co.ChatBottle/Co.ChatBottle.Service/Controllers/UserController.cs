using Co.ChatBottle.Business;
using Co.ChatBottle.Model;
using Co.ChatBottle.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Co.ChatBottle.Service.Controllers
{
    public class UserController : BaseController
    {
        public UserBiz userBiz { get; set; } = new UserBiz();

        [HttpPost]
        public HttpResponseMessage Register(UserRequest request)
        {
            var response = new BaseResponse<UserResponse>();

            if (request == null)
            {
                return null;
            }
            ConvertBaseRequest(request);

            var userInfo = new ACT_User
            {
                UserName = request.UserName,
                Gender = request.Gender
            };

            var userEntity = userBiz.Add(userInfo);
            var result = new UserResponse
            {
                ID = userEntity.ID,
                UserName = userEntity.UserName,
                Phone = userEntity.Phone,
                Mail = userEntity.Mail,
                QQ = userEntity.QQ,
                Birthday = userEntity.Birthday,
                Gender = userEntity.Gender,
                Status = userEntity.Status,
                Remark = userEntity.Remark
            };

            if (userEntity != null)
            {
                response.ErrorCode = 0;
                response.Result = result;
            }
            return ResponseToJson(response, request.callback);
        }

        [HttpPost]
        public HttpResponseMessage Update(UserRequest request)
        {
            var response = new BaseResponse<UserResponse>();

            if (request == null)
            {
                return null;
            }
            ConvertBaseRequest(request);

            var userEntity = userBiz.Query<ACT_User>(request.ID);

            userEntity.UserName = request.UserName;
            userEntity.Phone = request.Phone;
            userEntity.QQ = request.QQ;
            userEntity.Mail = request.Mail;
            userEntity.Gender = request.Gender;
            if (request.Birthday != null && request.Birthday != DateTime.MinValue)
            {
                userEntity.Birthday = request.Birthday;
            }
            userEntity.Remark = request.Remark;
            userEntity.UpdateUser = "1";
            userEntity.UpdateTime = DateTime.Now;

            if (userBiz.Update(userEntity))
            {
                response.ErrorCode = 0;
                response.Result = null;
            }
            return ResponseToJson(response, request.callback);
        }


        [HttpGet]
        public HttpResponseMessage QueryAll()
        {
            var result = userBiz.QueryAll<ACT_User>();
            return ResponseToJson(result);
        }

        [HttpGet]
        // GET: api/UserApi/5
        public HttpResponseMessage QueryById(int id)
        {
            var result = userBiz.Query<ACT_User>(id);
            return ResponseToJson(result);
        }
    }
}
