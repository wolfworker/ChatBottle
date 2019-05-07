using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class CommonEntity
    {
        public long CreatedUserID { get; set; } = 1;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public long UpdateUserID { get; set; } = 1;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
