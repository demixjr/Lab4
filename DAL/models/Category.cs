using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class Category
    {
        public Category() { }
        public Category(string name, Heading heading)
        {
            Name = name;
            Heading = heading;
            Announcements = new List<Announcement>();
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public int HeadingId { get; set; }
        public virtual Heading Heading { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}