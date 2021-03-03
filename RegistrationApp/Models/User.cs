using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public string PhotoURL { get; set; } 
    }
}
