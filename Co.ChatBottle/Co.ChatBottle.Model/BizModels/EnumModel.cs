using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class EnumModel
    {
        public enum LogLevel
        {
            [Description("错误")]
            Error = 1,

            [Description("警告")]
            Worning = 2,

            [Description("正常")]
            Normal = 3
        }

        public enum LogType
        {
            #region 系统设置模块 10..
            [Description("打开设置页面")]
            Sys_OpenSetting = 1001,

            [Description("退出账号")]
            Sys_Quit = 1002,
            #endregion

            #region 注册登录相关 11..
            [Description("请求登录")]
            Login_Request = 1101,

            [Description("请求注册")]
            Login_Register = 1102,

            #endregion

            #region 瓶子模块 12..
            [Description("打开我的瓶子页面")]
            Bottle_My = 1201,

            [Description("打开扔瓶子页面")]
            Bottle_ThrowPage = 1202,

            [Description("点击捞瓶子")]
            Bottle_PickUp = 1203,

            [Description("扔出瓶子")]
            Bottle_Throw = 1204,

            [Description("打开瓶子")]
            Bottle_Open = 1206,
            #endregion

            #region 个人信息模块 13..
            [Description("打开个人页面")]
            Profile_SelfPage = 1301,

            [Description("更新个人信息")]
            Profile_Update = 1302,

            [Description("查看朋友信息")]
            Profile_FriendPage = 1303,
            #endregion
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDesc(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }
}
