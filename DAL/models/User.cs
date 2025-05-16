using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class User
    {
        public User() { }
        public User(string username, string password) 
        {
            Username = username;
            Password = password;
            Announcements = new List<Announcement>();
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}
