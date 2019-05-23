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
            [Description("打开主页面")]
            Sys_OpenIndex = 1001,

            [Description("打开设置页面")]
            Sys_OpenSetting = 1002,

            [Description("退出账号")]
            Sys_Quit = 1003,
            #endregion

            #region 欢迎页面相关 11..
            [Description("打开欢迎页")]
            Welcome_OpenPage = 1101,

            [Description("请求登录")]
            Welcome_Login = 1102,

            [Description("请求注册")]
            Welcome_Register = 1103,

            #endregion

            #region 瓶子模块 12..
            [Description("打开我的瓶子页面")]
            Bottle_My = 1201,

            [Description("打开扔瓶子页面")]
            Bottle_OpenThrowPage = 1202,

            [Description("打开捞瓶子页面")]
            Bottle_OpenPickPage = 1203,

            [Description("操作扔瓶子")]
            Bottle_Throw = 1204,

            [Description("操作捞瓶子")]
            Bottle_Pick = 1205,

            [Description("打开瓶子")]
            Bottle_OpenDetail = 1206,
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
