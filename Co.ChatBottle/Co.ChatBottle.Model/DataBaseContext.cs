using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("DataBaseContext")
        {

        }
        #region 数据库 表实体
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<ACT_User> Users { get; set; }

        /// <summary>
        /// 调试日志表
        /// </summary>
        public DbSet<SYS_DebugLog> DebugLogs { get; set; }
        
        /// <summary>
        /// 业务日志表
        /// </summary>
        public DbSet<SYS_RequestLog> RequestLogs { get; set; }

        /// <summary>
        /// 业务日志表
        /// </summary>
        public DbSet<ACT_User_Position> Positions { get; set; }

        #endregion
    }
}
