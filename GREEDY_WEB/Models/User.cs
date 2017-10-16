using System;

namespace GREEDY.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string fullname { get; set; }
        public DateTime birthday { get; set; }
        public DateTime SessionId { get; set; }
    }
}

