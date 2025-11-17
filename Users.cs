using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public Users(int id, string username, string password, bool isAdmin)
        {
            Id = id;
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
        }

    }
}
