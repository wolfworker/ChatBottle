using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co.ChatBottle.Model
{
    public class CommonEntity
    {
        public string CreatedUser { get; set; } = "1";
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string UpdateUser { get; set; } = "1";
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
