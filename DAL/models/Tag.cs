using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class Tag
    {
        public Tag() { }
        public Tag (string name)
        { 
            this.Name = name;
        }

        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}
