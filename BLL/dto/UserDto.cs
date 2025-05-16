using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get;set;}

        public virtual ICollection<Announcement> Announcements { get; set;}

    }
}
