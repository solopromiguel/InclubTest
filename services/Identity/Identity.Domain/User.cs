using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain
{
   public class User
    {
        public int Id { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
