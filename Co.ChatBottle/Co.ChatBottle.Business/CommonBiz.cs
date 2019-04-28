using Co.ChatBottle.DataAccess;
using Co.ChatBottle.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Business
{
    public class CommonBiz
    {
        public CommonDal commonDal { get; set; } = new CommonDal();


        public T Add<T>(T t) where T : class
        {
            try
            {
                return commonDal.Add(t);
            }
            catch (Exception ex)
            {
                var message = $"Biz Add 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：{JsonConvert.SerializeObject(t)}";
                WriteLog(message);
                return null;
            }
        }

        public bool Update<T>(T t) where T : class
        {
            try
            {
                return commonDal.Update(t);
            }
            catch (Exception ex)
            {
                var message = $"Biz Update 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：{JsonConvert.SerializeObject(t)}";
                WriteLog(message);
                return false;
            }
        }

        public bool Delete<T>(int id) where T : class
        {
            try
            {
                return commonDal.Delete<T>(id);
            }
            catch (Exception ex)
            {
                var message = $"Biz Delete 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：{id}";
                WriteLog(message);
                return false;
            }

        }

        public List<T> QueryAll<T>() where T : class
        {
            try
            {
                return commonDal.QueryAll<T>();
            }
            catch (Exception ex)
            {
                var message = $"Biz QueryAll 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}";
                WriteLog(message);
                return null;
            }
        }

        public T Query<T>(long id) where T : class
        {
            try
            {
                return commonDal.Query<T>(id);
            }
            catch (Exception ex)
            {
                var message = $"Biz Query 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：{id}";
                WriteLog(message);
                return null;
            }
        }

        public List<T> QueryCustom<T>(string sql, params object[] args) where T : class
        {
            try
            {
                return commonDal.QueryCustom<T>(sql, args);
            }
            catch (Exception ex)
            {
                var message = $"Biz QueryCustom 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：sql -> {sql},args -> {JsonConvert.SerializeObject(args)}";
                WriteLog(message);
                return null;
            }

        }

        public int ExcuteSql(string sql, params object[] args)
        {
            try
            {
                return commonDal.ExcuteSql(sql, args);
            }
            catch (Exception ex)
            {
                var message = $"Biz ExcuteSql 出错 -> 错误信息：{ex.Message}, 堆栈信息：{ex.StackTrace}, 请求参数：sql -> {sql},args -> {JsonConvert.SerializeObject(args)}";
                WriteLog(message);
                return 0;
            }
        }

        public void WriteLog(string logContent, byte level = 1)
        {
            try
            {
                var debugLog = new SYS_DebugLog
                {
                    ID = Guid.NewGuid().ToString(),
                    LogLevel = level,
                    LogContent = logContent
                };
                commonDal.Add(debugLog);
            }
            catch (Exception ex)
            {
                //log error
            }
        }
    }
}
