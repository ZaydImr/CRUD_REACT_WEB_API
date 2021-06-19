using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string Fullname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public User()
        {

        }
        public User(string username,string password,string Fullname,string email,string phoneNumber)
        {
            this.username = username;
            this.password = password;
            this.Fullname = Fullname;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }
    }
}
