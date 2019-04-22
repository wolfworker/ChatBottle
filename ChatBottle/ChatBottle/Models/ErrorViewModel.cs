using System;

namespace ChatBottle.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Customer
    {
        public int ID { get; set; }
        public string Nickname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string QQ { get; set; }
        public byte Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Remark { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

    }
}